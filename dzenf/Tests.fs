namespace Tests.Lib.Base

module ``Base Tests Cases`` =
  open NUnit.Framework
  open FsUnit

  [<Test>]
  let ``Readable test cae name`` () = 
    true |> should be (equal true)