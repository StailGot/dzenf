module Plug.Interop

type IDo =
  abstract member Do : unit -> unit

type ILogger =
  abstract member Log : 'T -> unit