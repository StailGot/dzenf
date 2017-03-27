open System.Collections
open System.ComponentModel.Composition
open System.ComponentModel.Composition.Hosting

type IDo =
  abstract member Do : unit -> unit

type ILogger =
  abstract member Log : 'T -> unit

[<Export(typeof<ILogger>)>]
type Logger() =
  interface ILogger with
    member this.Log e = printfn "Log: %A" e

[<Export(typeof<IDo>)>]
type DoItOne() =
  interface IDo with
    member this.Do() = printfn "%A" "DoItOne"

[<Export(typeof<IDo>)>]
type DoItTwo [<ImportingConstructor>] (logger:ILogger) =
  let _logger = logger
  interface IDo with
    member this.Do() = printfn "%A" "DoItTwo"; logger.Log "DoItTwo"

type DoItDo() =
  [<ImportMany(typeof<IDo>)>]
  let (actions: Generic.IEnumerable<System.Lazy<IDo>>) = null
  member this.Do() = actions |> Seq.iter ( fun action -> action.Value.Do() )

type App() =
  let doer = DoItDo()
  let catalogs () =
    let catalogs' = new AggregateCatalog()
    
    let src:list<Primitives.ComposablePartCatalog> =
     System.IO.Directory.CreateDirectory >> ignore <| "./plugins/"
     [ new ApplicationCatalog()
       new DirectoryCatalog "."
       new DirectoryCatalog "./plugins/"]
    src |> Seq.iter catalogs'.Catalogs.Add
    catalogs'
  let Init () =
      let container = new CompositionContainer( catalogs() )
      container.ComposeParts(doer)
  do Init()
  member this.Do () = doer.Do()
    
[<EntryPoint>]
let main argv = 
  let app = App()
  app.Do()
  0