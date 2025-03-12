open System
open System.IO

let rec get_all_files dir =
    seq {
        yield! Directory.EnumerateFiles(dir) // добавляем файлы из текущей директории
        for subdir in Directory.EnumerateDirectories(dir) do
            yield! get_all_files subdir //рекурсивно обрабатываем подкаталоги
    }

let get_least_common_extension dir =
    get_all_files dir
    |> Seq.map Path.GetExtension //расширения
    |> Seq.filter (fun ext -> ext <> "" && ext |> Seq.exists (fun c -> c <> ' ')) //без расширения
    |> Seq.countBy id //подсчёт
    |> Seq.sortBy snd //сортировка по кол-ву
    |> Seq.tryHead

[<EntryPoint>]
let main argv =
    printf "Введите путь к каталогу: "
    let dir = Console.ReadLine()
    match get_least_common_extension dir with
    | Some (ext, count) -> printfn "Наименее частое расширение: %s (встречается %d раз)" ext count
    | None -> printfn "В указанной директории нет файлов с расширениями"
    0