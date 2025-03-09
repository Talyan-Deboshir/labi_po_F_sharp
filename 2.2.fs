open System

let rec string_list () =
    printfn "Введите числа "
    let rec read acc =
        let input = Console.ReadLine()
        match input with
        | null | "" -> List.rev acc
        | _ ->
            match Int64.TryParse(input) with
            | (true, _) -> read (input :: acc)
            | _ ->
                printfn "Введите только числа"
                read acc
    read []


let create_number digits =
    let rec number_to_digits (n: int64) =
        if n = 0L then [0L]
        else 
            let rec razdelenie acc n =
                if n = 0L then acc
                else razdelenie ((n % 10L) :: acc) (n / 10L)
            razdelenie [] n

    if digits = [] then None

    else
        let digits_list = digits |> List.collect number_to_digits 

        let (result, founded) = 
            List.fold 
                (fun (acc, found) digit -> 
                    if digit % 2L = 0L then (acc * 10L + digit, true)
                    else (acc, found)) 
                (0L, false) digits_list

        if founded then Some result else None



[<EntryPoint>]
let main argv =
    let digits = string_list () 
    let int64_list = digits |> List.map Int64.Parse

    match create_number int64_list with
    | Some result -> printfn "Полученное число: %s" (result.ToString())
    | None -> printfn "В списке нет чётных цифр"
    0
