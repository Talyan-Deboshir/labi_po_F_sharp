open System

let rims_to_des (roman: string) : Option<int> =
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

let rec lazy_string_seq () : seq<int> =
    printfn "Введите римские цифры (I - IX) через Enter (пустая строка для завершения):"
    
    let rec read_seq () : seq<Lazy<string>> =
        seq {
            let input = lazy (printfn "ввод "; Console.ReadLine())
            if not (input.Value = null || input.Value = "") then
                yield input
                yield! read_seq ()
        }
    
    let des_from_rims: seq<Lazy<Option<int>>> = 
        read_seq () 
        |> Seq.map (fun lz -> lazy (printfn "преобразование "; rims_to_des lz.Value)) 
        |> Seq.cache 

    if Seq.exists (fun (lz: Lazy<Option<int>>) -> Option.isNone lz.Value) des_from_rims then
        printfn "Введены некорректные римские цифры, попробуйте снова."
        lazy_string_seq ()
    else
        des_from_rims |> Seq.choose (fun (lz: Lazy<Option<int>>) -> lz.Value) |> Seq.cache 

[<EntryPoint>]
let main argv =
    let des_nums = lazy_string_seq ()
    printf "Десятичные цифры: "
    des_nums |> Seq.iter (printf "%d ")
    printfn ""
    0
