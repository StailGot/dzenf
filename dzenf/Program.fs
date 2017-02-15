open System
open System.Windows.Controls
open System.Windows
open UI.Window

type DataItem = { Name:string; Count:int }

[<STAThread>]
[<EntryPoint>]
let main argv =
    let app = UI.Application()
    let window = Window( Visibility = Visibility.Visible )

    app.MainWindow <- window
    //let window = Window( Visibility = Visibility.Visible )
    let data = {1..1000000} |> Seq.map (fun e -> {Name = Convert.ToString e; Count = e}) |> Collections.ObjectModel.ObservableCollection

    window.ctrlDataGrid.ItemsSource <- data

    
    let async_add_data () =
      async{ Seq.initInfinite (fun e -> 
             printfn "%A" DateTime.Now
             let dataGrid = window.ctrlDataGrid
             System.Threading.Thread.Sleep 300;
             dataGrid.Dispatcher.Invoke(
              fun () -> data.Add {Name = Convert.ToString data.Count; Count = data.Count }; )) |> Seq.iter ignore
            } |> Async.Start

    //window.ctrlSettings.Click.Add ( fun e -> MessageBox.Show("132") |> ignore )
    ignore >> async_add_data |> window.ctrlSettings.Click.Add
    ignore >> async_add_data |> window.ctrlButton.Click.Add

    app.Run()
