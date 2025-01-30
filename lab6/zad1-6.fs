let liczbaSlowZnakow () =
    printf "Podaj tekst: "
    let tekst = System.Console.ReadLine()
    let slowa = tekst.Split([|' '|], System.StringSplitOptions.RemoveEmptyEntries).Length
    let znaki = tekst.Replace(" ", "").Length
    printfn "Liczba słów: %d" slowa
    printfn "Liczba znaków (bez spacji): %d" znaki

let sprawdzPalindrom () =
    printf "Podaj tekst: "
    let tekst = System.Console.ReadLine().ToLower().Replace(" ", "")
    let odwrocony = tekst |> Seq.rev |> Seq.toArray |> System.String
    let czyPalindrom = tekst = odwrocony
    printfn "%s" (if czyPalindrom then "To jest palindrom" else "To nie jest palindrom")

let usunDuplikaty () =
    printf "Podaj słowa oddzielone spacjami: "
    let tekst = System.Console.ReadLine()
    let unikalne = tekst.Split([|' '|], System.StringSplitOptions.RemoveEmptyEntries) |> Set.ofArray
    printfn "Unikalne słowa: %s" (String.concat " " unikalne)

let zmienFormat () =
    let rec wczytujWpisy () =
        printf "Podaj wpis w formacie 'imię; nazwisko; wiek' (pusty wpis kończy): "
        let wpis = System.Console.ReadLine()
        if wpis = "" then ()
        else
            try
                let dane = wpis.Split([|';'|]) |> Array.map (fun s -> s.Trim())
                printfn "%s, %s (%s lat)" dane.[1] dane.[0] dane.[2]
                wczytujWpisy()
            with _ ->
                printfn "Niepoprawny format"
                wczytujWpisy()
    wczytujWpisy()

let najdluzszeSlowo () =
    printf "Podaj tekst: "
    let tekst = System.Console.ReadLine()
    let slowa = tekst.Split([|' '|], System.StringSplitOptions.RemoveEmptyEntries)
    if slowa.Length > 0 then
        let najdluzsze = Array.maxBy String.length slowa
        printfn "Najdłuższe słowo: %s" najdluzsze
        printfn "Długość: %d" najdluzsze.Length
    else
        printfn "Brak słów w tekście"

let wyszukajZamien () =
    printf "Podaj tekst: "
    let tekst = System.Console.ReadLine()
    printf "Jakie słowo znaleźć: "
    let szukane = System.Console.ReadLine()
    printf "Na jakie słowo zamienić: "
    let zamiana = System.Console.ReadLine()
    printfn "Zmodyfikowany tekst: %s" (tekst.Replace(szukane, zamiana))

let rec menu () =
    printfn "\nMenu:"
    printfn "1. Liczba słów i znaków"
    printfn "2. Sprawdzenie palindromu"
    printfn "3. Usuwanie duplikatów"
    printfn "4. Zmiana formatu tekstu"
    printfn "5. Znajdowanie najdłuższego słowa"
    printfn "6. Wyszukiwanie i zamiana"
    printfn "0. Wyjście"
    
    printf "\nWybierz zadanie (0-6): "
    match System.Console.ReadLine() with
    | "0" -> ()
    | "1" -> liczbaSlowZnakow(); menu()
    | "2" -> sprawdzPalindrom(); menu()
    | "3" -> usunDuplikaty(); menu()
    | "4" -> zmienFormat(); menu()
    | "5" -> najdluzszeSlowo(); menu()
    | "6" -> wyszukajZamien(); menu()
    | _ -> printfn "Niepoprawny wybór"; menu()

[<EntryPoint>]
let main argv =
    menu()
    0