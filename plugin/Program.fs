module Plug

open Plug.Interop

open System.ComponentModel.Composition

[<Export(typeof<IDo>)>]
type DoItOne() =
  interface IDo with
    member this.Do() = printfn "%A" "DoItOne www"

[<Export(typeof<IDo>)>]
type DoItTwo [<ImportingConstructor>] (logger:ILogger) =
  let _logger = logger
  interface IDo with
    member this.Do() = _logger.Log "DoItTwo"

[<Export(typeof<IDo>)>]
type DoItTwo' [<ImportingConstructor>] (logger:ILogger) =
  let _logger = logger
  interface IDo with
    member this.Do() = _logger.Log "DoItTwo DOO Itt''"

