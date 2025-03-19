open System

type Tree =
    | Empty
    | Node of string * Tree * Tree

let rec build_tree (values: string list) : Tree =
    match values with
    | [] -> Empty
    | head :: others ->
        let left = others |> List.filter (fun v -> v < head) |> build_tree
        let right = others |> List.filter (fun v -> v >= head) |> build_tree
        Node(head, left, right)

let rec count_of_letter letter acc tree =
    match tree with
    | Empty -> acc
    | Node(value, left, right) ->
        let new_acc = if value.EndsWith(letter) then acc + 1 else acc
        count_of_letter letter (count_of_letter letter new_acc left) right


let print_tree (tree: Tree) =
    let rec print_sub_tree tree prefix is_left =
        match tree with
        | Empty -> ()
        | Node(v, left, right) ->
            let new_prefix = prefix + (if is_left then "│   " else "    ")
            if right <> Empty then print_sub_tree right new_prefix false
            printfn "%s%s%s" prefix (if is_left then "└── " else "┌── ") v
            if left <> Empty then print_sub_tree left new_prefix true
    print_sub_tree tree "" false

let rec read_tree () : string list =
    printfn "Введите строки (пустая строка для завершения ввода): "
    let rec read_lines acc =
        let input = Console.ReadLine()
        match input with
        | null | "" -> List.rev acc
        | _ -> read_lines (input :: acc)
    read_lines []

let values = read_tree()
let input_tree = build_tree values

printf "Введите символ: "
let search_letter = Console.ReadLine()

let count = count_of_letter search_letter 0 input_tree 

printfn "Исходное дерево:" 
print_tree input_tree
printfn "\nКоличество узлов, заканчивающихся на '%s': %d" search_letter count
