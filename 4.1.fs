open System

type Tree =
    | Empty
    | Node of int * Tree * Tree



let rec build_tree values =
    match values with
    | [] -> Empty
    | head :: others ->
        let left = others |> List.filter (fun v -> v < head) |> build_tree
        let right = others |> List.filter (fun v -> v >= head) |> build_tree
        Node(head, left, right)

let dec_num n =
    let dec_digit d =
        if d = 0 then 1
        else d - 1
    
    let rec to_digits num acc =
        if num = 0 then acc
        else to_digits (num / 10) ((num % 10) :: acc)
    
    let rec from_digits digits =
        List.fold (fun acc d -> acc * 10 + d) 0 digits
    
    if n = 0 then 1
    else from_digits (List.map dec_digit (to_digits n []))

let rec mapTree f tree =
    match tree with
    | Empty -> Empty
    | Node(v, left, right) -> Node(f v, mapTree f left, mapTree f right)

let print_tree tree =
    let rec print_sub_tree tree prefix is_left =
        match tree with
        | Empty -> ()
        | Node(v, left, right) ->
            let new_prefix = prefix + (if is_left then "│   " else "    ")
            if right <> Empty then print_sub_tree right new_prefix false
            printfn "%s%s%d" prefix (if is_left then "└── " else "┌── ") v
            if left <> Empty then print_sub_tree left new_prefix true
    print_sub_tree tree "" false


let rec read_tree () =
    printfn "Введите строки (только числа, пустая строка для завершения ввода): "
    let rec read_lines acc =
        let input = Console.ReadLine()
        match input with
        | null | "" -> List.rev acc
        | _ -> 
            match Int32.TryParse(input) with
            | true, num -> read_lines (num :: acc)
            | _ -> 
                printfn "Введите целое число"
                read_lines acc 
    read_lines []


let numbers = read_tree()


let input_tree = build_tree numbers
let output_tree = mapTree dec_num input_tree

printfn "Исходное дерево:" 
print_tree input_tree
printfn "\nПреобразованное дерево: " 
print_tree output_tree
