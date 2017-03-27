open System
open System.Windows.Controls
open System.Windows
open UI.Window

open SharpSvn

open MahApps.Metro
open MahApps.Metro.Controls

type DataItem = { Name:string; Count:int }

[<STAThread>]
[<EntryPoint>]
let main argv =
    let app = UI.Application()
    let window = Window( Visibility = Visibility.Visible )

    app.MainWindow <- window
    let data = 
      {1..1_000_000} 
      |> Seq.map (fun e -> {Name = Convert.ToString e; Count = e}) 
      |> Collections.ObjectModel.ObservableCollection

    window.ctrlDataGrid.ItemsSource <- data
    window.ctrlListBox .ItemsSource <- ThemeManager.Accents |> Seq.map ( fun v -> v.Name )
    window.ctrlListBox2.ItemsSource <- ThemeManager.AppThemes |> Seq.map ( fun v -> v.Name )
    window.ctrlListBox .SelectedIndex <- 0
    window.ctrlListBox2.SelectedIndex <- 0

    let update_theme () = 
      let args = Application.Current
               , ThemeManager.GetAccent( Convert.ToString window.ctrlListBox.SelectedItem )
               , ThemeManager.GetAppTheme (Convert.ToString window.ctrlListBox2.SelectedItem)
      args |> ThemeManager.ChangeAppStyle 

    ignore >> update_theme |> window.ctrlListBox.SelectionChanged.Add
    ignore >> update_theme |> window.ctrlListBox2.SelectionChanged.Add
    update_theme()


    let add_some_rows () = 
      let add_row_safe () = 
        printfn "%A" DateTime.Now
        let dataGrid = window.ctrlDataGrid
        System.Threading.Thread.Sleep 300
        let add_row () = data.Add {Name = Convert.ToString data.Count; Count = data.Count };
        dataGrid.Dispatcher.Invoke( add_row )
      Seq.initInfinite (ignore >> add_row_safe) |> Seq.iter ignore

    let async_add_data () =
      async{ add_some_rows() } |> Async.Start

    //window.ctrlSettings.Click.Add ( fun e -> MessageBox.Show("132") |> ignore )
    ignore >> async_add_data |> window.ctrlSettings.Click.Add
    ignore >> async_add_data |> window.ctrlButton.Click.Add

    app.Run()
