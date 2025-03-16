open System
open System.IO


let rec read_files (dir: string) : Lazy<seq<string>> =
    lazy seq {
        yield! Directory.EnumerateFiles(dir)
        for subdir in Directory.EnumerateDirectories(dir) do
            yield! (read_files subdir).Value
    }


let get_extension (dir: string) : Lazy<option<string * int>> =
    lazy (
        read_files dir
        |> fun lazyFiles -> lazyFiles.Value
        |> Seq.map Path.GetExtension
        |> Seq.filter (fun ext -> ext <> "" && ext |> Seq.exists (fun c -> c <> ' '))
        |> Seq.countBy id
        |> Seq.sortBy snd
        |> Seq.tryHead
    )

[<EntryPoint>]
let main argv =
    printf "Введите путь к каталогу: "
    let dir = Console.ReadLine()

    let min_extension = get_extension dir

    match min_extension.Value with
    | Some (ext, count) -> printfn "Наименее частое расширение: %s (встречается %d раз)" ext count
    | None -> printfn "В указанной директории нет файлов с расширениями"

    0
