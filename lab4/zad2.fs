open System
//Zadanie 2 waluty

let kursyWalut = Map [
    ("EUR", 1.0)
    ("USD", 1.09)
    ("GBP", 0.85)
    ("PLN", 4.37)
]

let przeliczWalute kwota zrodlowa docelowa =
    if Map.containsKey zrodlowa kursyWalut && Map.containsKey docelowa kursyWalut then
        let przelicznik0 = kursyWalut[zrodlowa]
        let przelicznik1 = kursyWalut[docelowa]
        // najpierw na EUR potem na docelową
        (kwota / przelicznik0) * przelicznik1
    else
        failwith "Nieobsługiwana waluta"

let main() =
    printf "Dostępne waluty: PLN, EUR, USD, GBP\nPodaj kwotę: "
    let kwota = Console.ReadLine() |> float
    printf "Podaj walutę źródłową: "
    let waluta0 = Console.ReadLine().ToUpper()
    printf "Podaj walutę docelową: "
    let waluta1 = Console.ReadLine().ToUpper()
    
    try
        let wynik = przeliczWalute kwota waluta0 waluta1
        printfn "\nWynik: %.2f %s = %.2f %s" kwota waluta0 wynik waluta1
    with
        | ex -> printfn "Błąd: %s" ex.Message

main()