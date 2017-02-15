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
    let data = [1..1000] |> Seq.map (fun e -> {Name = Convert.ToString e; Count = e}) |> Collections.ObjectModel.ObservableCollection
    window.dataGrid.ItemsSource <- data
    
    async{ Seq.initInfinite (fun e -> 
           printfn "asd"
           System.Threading.Thread.Sleep 300;
           window.dataGrid.Dispatcher.Invoke( fun () -> data.Add {Name = Convert.ToString e; Count = data.Count }; 
                                                        window.dataGrid.SelectedIndex <- data.Count - 1
                                                        window.dataGrid.ScrollIntoView ( window.dataGrid.SelectedItem )
                                                        )) |> Seq.iter ignore
          } |> Async.Start
    app.Run()
