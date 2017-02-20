namespace Tests.Lib.Base

module ``Base Tests Cases`` =
  open NUnit.Framework
  open FsUnit

  open FParsec

  let get_revision text =
    let maxCount = System.Int32.MaxValue
    let skipManyCharsTillString s = skipCharsTillString s true maxCount
    let revision = pint64
    let keyWord = "some"
    let parser = skipManyCharsTillString keyWord .>> spaces >>. revision
    let result = run parser text 
    match result with
    | Success( result, _, _) -> Some result
    | _ -> None
  let [<Literal>] s1 = @"some 42"
  let [<Literal>] s2 = @"dsad 
  dsa^%$% &#^ &SA 231sd23a4s0 dssa@!#$$4fa duas jjn v^$^$^%&V gjhg hascjbsma c some       
  
  23  "

  [<TestCase( s1, 42L )>]
  [<TestCase( s2, 23L )>]
  let ``Parse int in text``
    (text, expected) = get_revision text |> should equal (Some expected)