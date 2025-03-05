open System

let rim_to_des rim =
    match rim with
    | "I" -> 1
    | "II" -> 2
    | "III" -> 3
    | "IV" -> 4
    | "V" -> 5
    | "VI" -> 6
    | "VII" -> 7
    | "VIII" -> 8
    | "IX" -> 9


let rims = ["I"; "II"; "III"; "IV"; "V"; "VI"; "VII"; "VIII"; "IX"]

let desNum = List.map rim_to_des rims

printfn "Десятичные: %A" desNum
