open System

let rec string_seq () =
    printfn "Введите числа "
    let rec readSeq () =
        seq {
            let input = Console.ReadLine()
            match Int64.TryParse(input) with
            | (true, value) ->
                yield value
                yield! readSeq ()
            | _ when input = null || input = "" -> ()
            | _ ->
                printfn "Введите только числа"
                yield! readSeq ()
        }
    readSeq ()

let create_number digits =
    let number_to_digits (n: int64) =
        seq {
            if n = 0L then yield 0L
            else
                let rec razdelenie n =
                    seq {
                        if n <> 0L then
                            yield! razdelenie (n / 10L)
                            yield (n % 10L)
                    }
                yield! razdelenie n
        }

    let digits_seq = digits |> Seq.collect number_to_digits

    let (result, found) =
        Seq.fold 
            (fun (acc, found) digit ->
                if digit % 2L = 0L then (acc * 10L + digit, true)
                else (acc, found))
            (0L, false) digits_seq

    if found then Some result else None

[<EntryPoint>]
let main argv =
    let digits = string_seq ()
    match create_number digits with
    | Some result -> printfn "Полученное число: %s" (result.ToString())
    | None -> printfn "В последовательности нет чётных цифр"
    0
