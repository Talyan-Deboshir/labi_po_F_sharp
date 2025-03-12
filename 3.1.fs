open System

let rims_to_des roman =
    match roman with
    | "I" -> Some 1
    | "II" -> Some 2
    | "III" -> Some 3
    | "IV" -> Some 4
    | "V" -> Some 5
    | "VI" -> Some 6
    | "VII" -> Some 7
    | "VIII" -> Some 8
    | "IX" -> Some 9
    | _ -> None

let rec string_seq () =
    printfn "Введите римские цифры (I - IX) через Enter: "
    
    let rec read_seq () =
        seq {
            let input = Console.ReadLine()
            if not (input = null || input = "") then
                yield input
                yield! read_seq ()
        }
    
    let des_from_rims = read_seq () |> Seq.map rims_to_des |> Seq.cache
    
    if Seq.exists Option.isNone des_from_rims then
        printfn "Введены некорректные римские цифры"
        string_seq ()
    else
        des_from_rims |> Seq.choose id

[<EntryPoint>]
let main argv =
    let des_nums = string_seq ()
    printf "Десятичные цифры: "; Seq.iter (printf "%d ") des_nums; printfn ""
    0
