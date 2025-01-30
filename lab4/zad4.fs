open System

// Definicja rekordu reprezentującego konto
type Account = {
    Number: string
    Balance: decimal
}

// Stan aplikacji jako mapa kont
type Bank = Map<string, Account>

// Funkcje pomocnicze do operacji bankowych
let createAccount number (bank: Bank) =
    bank.Add(number, { Number = number; Balance = 0m })

let deposit accountNumber amount (bank: Bank) =
    match bank.TryFind accountNumber with
    | Some account ->
        bank.Add(accountNumber, { account with Balance = account.Balance + amount })
    | None -> bank

let withdraw accountNumber amount (bank: Bank) =
    match bank.TryFind accountNumber with
    | Some account when account.Balance >= amount ->
        bank.Add(accountNumber, { account with Balance = account.Balance - amount })
    | _ -> bank

let getBalance accountNumber (bank: Bank) =
    match bank.TryFind accountNumber with
    | Some account -> Some account.Balance
    | None -> None

// Menu i interakcja z użytkownikiem
let printMenu() =
    printfn "\nWybierz operację:"
    printfn "1. Utwórz nowe konto"
    printfn "2. Wpłać środki"
    printfn "3. Wypłać środki"
    printfn "4. Sprawdź saldo"
    printfn "5. Wyjście"

[<EntryPoint>]
let main argv =
    let mutable bank = Map.empty<string, Account>
    let mutable running = true
    
    while running do
        printMenu()
        match Console.ReadLine() with
        | "1" ->
            printfn "Podaj numer konta:"
            let number = Console.ReadLine()
            bank <- createAccount number bank
            printfn "Utworzono konto o numerze %s" number
            
        | "2" ->
            printfn "Podaj numer konta:"
            let number = Console.ReadLine()
            printfn "Podaj kwotę do wpłaty:"
            match Decimal.TryParse(Console.ReadLine()) with
            | true, amount ->
                bank <- deposit number amount bank
                printfn "Wpłacono %M na konto %s" amount number
            | _ -> printfn "Nieprawidłowa kwota"
            
        | "3" ->
            printfn "Podaj numer konta:"
            let number = Console.ReadLine()
            printfn "Podaj kwotę do wypłaty:"
            match Decimal.TryParse(Console.ReadLine()) with
            | true, amount ->
                let oldBank = bank
                bank <- withdraw number amount bank
                if oldBank = bank then
                    printfn "Brak wystarczających środków lub konto nie istnieje"
                else
                    printfn "Wypłacono %M z konta %s" amount number
            | _ -> printfn "Nieprawidłowa kwota"
            
        | "4" ->
            printfn "Podaj numer konta:"
            let number = Console.ReadLine()
            match getBalance number bank with
            | Some balance -> printfn "Saldo konta %s: %M" number balance
            | None -> printfn "Konto nie istnieje"
            
        | "5" ->
            running <- false
            
        | _ ->
            printfn "Nieprawidłowa opcja"
            
    0