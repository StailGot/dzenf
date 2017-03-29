module Settings

open System.IO
open System.Xml.Serialization

[<XmlType>]
[<CLIMutable>]
type Settings = {
     [<XmlAttribute>] A: string
     [<XmlAttribute>] B: string
     [<XmlAttribute>] C: string
} 
with
  static member Serializer = XmlSerializer(typeof<Settings[]>, XmlRootAttribute "Root" )

  member __.Save() =
    let writer = new StringWriter()
    let ns = new XmlSerializerNamespaces()
    ns.Add("","")
    Settings.Serializer.Serialize( writer, [|__|], ns ) 
    writer |> sprintf "%A"

  static member Read str =
    try 
      Settings.Serializer.Deserialize (new StringReader(str) ) :?> Settings[] |> Seq.tryItem 0
    with
    | e -> e.Message |> stderr.WriteLine; None

