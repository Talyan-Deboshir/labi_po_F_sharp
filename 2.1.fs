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


let rec string_list () =
    printfn "Введите римские цифры (I - IX) через пробел: "
    
    let rec read acc =
        let input = Console.ReadLine()
        match input with
        | null | "" -> List.rev acc
        | _ -> read (input :: acc)
    
    let rims_nums = read []
    let des_from_rims = rims_nums |> List.map rims_to_des

    if List.exists Option.isNone des_from_rims then
        printfn "Введены некорректные римские цифры"
        string_list () 
    else
        des_from_rims |> List.choose id 



[<EntryPoint>]
let main argv =
    let des_nums = string_list () 
    printfn "Десятичные цифры: %A" des_nums
    0
