open System

//Функция для безопасного ввода целого числа
let rec readInt prompt =
    printf "%s" prompt // %s — форматный спецификатор для строк; prompt — выводится вместо %s
    match Int32.TryParse(Console.ReadLine()) with // match ... with проверяет результат (сопоставление с образцом) (как switch)
    //Int32.TryParse — проверяет, можно ли эту строку преобразовать в число
    | true, value -> value                          // Если ввод корректный — возвращаем число
    | false, _ ->                                  // Если ввод некорректный — выводим ошибку и повторяем ввод
        printfn "Введите целое число"
        readInt prompt

// Функция для ввода списка целых чисел
let rec readIntList () =
    printf "Введите целые числа через пробел: "
    let input = Console.ReadLine()
    try //Проверка на ошибки
        input.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) // Делим строку по пробелам
        |> Array.toList                                              // Преобразуем массив в список
        |> List.map int                                              // Преобразуем строки в числа
    with
    | :? System.FormatException ->                                  // Если встречается нечисловое значение
        printfn "Введите только целые числа"
        readIntList ()                                              // Повторный ввод

// Добавление элемента в конец списка
let addElement element list =
    list @ [element]  // Оператор @ сцепляет список с новым элементом, создавая новый список

// Удаление элемента из списка
let removeElement element list =
    list |> List.filter (fun x -> x <> element) // Оставляем только те элементы, которые не равны заданному
    //List.filter пропускает только те элементы, для которых функция (fun x -> x <> element) возвращает true

// Поиск элемента в списке
let findElement element list =
    list |> List.exists (fun x -> x = element)  // Возвращает true, если элемент найден, иначе false

// Сцепка двух списков
let concatLists list1 list2 =
    list1 @ list2  // Оператор @ объединяет два списка

// Получение элемента по индексу
let getElementByIndex index list =
    if index < 0 || index >= List.length list then
        None  // Если индекс вне диапазона — возвращаем None
    else
        Some (List.item index list)  //функция, которая возвращает элемент списка по индексу
        //Some используется для представления значений, которые могут либо существовать, либо отсутствовать

// Главное меню программы
let rec menu myList =
    printfn "\nТекущий список: %A" myList
    printfn "Выберите операцию:"
    printfn "1. Добавить элемент"
    printfn "2. Удалить элемент"
    printfn "3. Поиск элемента"
    printfn "4. Сцепить с другим списком"
    printfn "5. Получить элемент по индексу"
    printfn "6. Выйти"

    match readInt "Ваш выбор: " with
    | 1 ->  // Добавление элемента
        let element = readInt "Введите элемент для добавления: "
        let updatedList = addElement element myList
        printfn "Список после добавления: %A" updatedList
        menu updatedList  // Повторный вызов меню с обновлённым списком

    | 2 ->  // Удаление элемента
        let element = readInt "Введите элемент для удаления: "
        let updatedList = removeElement element myList
        printfn "Список после удаления: %A" updatedList
        menu updatedList

    | 3 ->  // Поиск элемента
        let element = readInt "Введите элемент для поиска: "
        let found = findElement element myList
        printfn "Элемент %d %s в списке" element (if found then "найден" else "не найден")
        menu myList  // Список не меняется при поиске

    | 4 ->  // Сцепка списков
        printfn "Введите второй список для сцепки:"
        let anotherList = readIntList ()
        let updatedList = concatLists myList anotherList
        printfn "Результат сцепки: %A" updatedList
        menu updatedList

    | 5 ->  // Получение элемента по индексу
        let index = readInt "Введите индекс элемента: "
        match getElementByIndex index myList with
        | Some value -> printfn "Элемент по индексу %d: %d" index value
        | None -> printfn "Индекс вне границ списка"
        menu myList

    | 6 ->  // Выход из программы
        printfn "Выход из программы"
        ()

    | _ ->  // Обработка некорректного выбора
        printfn "Выберите корректный номер операции"
        menu myList

[<EntryPoint>]
let main argv =
    printfn "=== Работа со списками ==="
    printfn "Введите исходный список:"
    let initialList = readIntList ()  // Ввод начального списка
    menu initialList                  // Запуск меню с этим списком
    0                                 
