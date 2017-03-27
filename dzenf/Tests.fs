namespace Tests.Lib.Base

module ``Base Tests Cases`` =
  open NUnit.Framework
  open FsUnit

  open FParsec

  let revision text =
    let maxCount = System.Int32.MaxValue
    let skipToString s = skipCharsTillString s true maxCount
    let revision = pint64
    let keyWord = "some"
    let parser = skipToString keyWord .>> spaces >>. revision
    let result = run parser text 
    match result with
    | Success( result, _, _) -> Some result
    | _ -> None
  let [<Literal>] s1 = @"some 42"
  let [<Literal>] s2 
    = @"dsad++++++++++[>+++++++>++++++++++>+++<<<-]
       >++.>+.+++++++..+++.>++.<<+++++++++++++++.> 
       .+++.------.--------.>+.some                
                                                   
      23                                          "
  let [<Literal>] s3 = @"sooome 99"

  [<TestCase( s1, 42L )>]
  [<TestCase( s2, 23L )>]
  let ``Parse int in text: expected some result``
    (text, expected) = revision text |> should equal (Some expected)

  [<TestCase( s3 )>]
  let ``Parse int in text: expected empty result``
    (text) = revision text |> should equal None