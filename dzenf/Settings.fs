module Settings

open System.IO
open System.Xml.Serialization

let inline save (record:'T) =
    let serializer = XmlSerializer(typeof<'T[]>, XmlRootAttribute "Root" )
    let writer = new StringWriter()
    let ns = new XmlSerializerNamespaces()
    ns.Add("","")
    serializer.Serialize( writer, [|record|], ns )
    writer |> sprintf "%A"

let inline read<'T> str =
  try 
    let Serializer = XmlSerializer(typeof<'T[]>, XmlRootAttribute "Root" )
    Serializer.Deserialize (new StringReader(str) ) :?> 'T[] |> Seq.tryItem 0
  with
  | e -> e.Message |> stderr.WriteLine; None


[<XmlType>]
[<CLIMutable>]
type Settings = {
     [<XmlAttribute>] A: string
     [<XmlAttribute>] B: string
     [<XmlAttribute>] C: string
}
