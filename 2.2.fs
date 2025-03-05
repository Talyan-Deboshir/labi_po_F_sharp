open System

let rec read () =
    printf "Введите десятичные цифры через пробел "
    let input = Console.ReadLine().Replace(" ", "").Replace("-", "")

    if input.Length = 0 || input |> Seq.exists (fun c -> not (Char.IsDigit c)) then
        printfn "Введите только десятичные числа"
        read ()
    else
        input
        |> Seq.toList                   
        |> List.map (fun c -> int64 (int c - int '0'))


let create_number digits =
    let (result, founded) = 
        List.fold 
            (fun (acc, found) current -> 
                if current % 2L = 0L then (acc * 10L + current, true) 
                else (acc, found)) 
            (0L, false) digits //начальные значения

    if founded then Some result else None 


[<EntryPoint>]
let main argv =
    let digits = read ()          
    match create_number digits with
    | Some result -> printfn "Полученное число: %s" (result.ToString())
    | None -> printfn "В списке нет чётных цифр"
    0
