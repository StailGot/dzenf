open UI.Window
open System
open System.Windows.Controls
open System.Windows

[<STAThread>]
[<EntryPoint>]
let main argv = 
    let window = Window( Visibility = Visibility.Visible )
    System.Windows.Application( MainWindow = window ).Run()
