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



let rec read () =
    printf "Введите римские цифры (I - IX) через пробел: "
    let input = Console.ReadLine()

    let rims_nums = input.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) |> List.ofArray
    let des_from_rims = rims_nums |> List.map rims_to_des

    
    if List.exists Option.isNone des_from_rims then //проверка на корректность ввода
        printfn "Введены некорректные римские цифры"
        read () 
    else
        des_from_rims |> List.choose id // преобразование option<int> в list<int>


[<EntryPoint>]
let main argv =
    let des_nums = read () 
    printfn "Десятичные цифры: %A" des_nums
    0
