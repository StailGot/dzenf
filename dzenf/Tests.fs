namespace Tests.Lib.Base

module ``Base Tests Cases`` =
  open NUnit.Framework
  open FsUnit

  let [<Literal>] s1 = "q"
  let [<Literal>] s2 = "s"
  let [<Literal>] s3 = "v"

  [<Test>]
  let ``Readable test case name``
    ([<Values(s1, s2, s3)>] i) = 
    true |> should be (equal true)