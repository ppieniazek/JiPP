type KontoBankowe(numerKonta: string) =
    let mutable saldo = 0.0
    
    member this.NumerKonta = numerKonta
    
    member this.Saldo 
        with get() = saldo
    
    member this.Wplac(kwota: float) =
        if kwota > 0.0 then
            saldo <- saldo + kwota
            printfn "Wpłacono %.2f zł na konto %s. Aktualne saldo: %.2f zł" kwota numerKonta saldo
            true
        else
            printfn "Błąd: Kwota wpłaty musi być większa od 0"
            false
            
    member this.Wyplac(kwota: float) =
        if kwota > 0.0 && kwota <= saldo then
            saldo <- saldo - kwota
            printfn "Wypłacono %.2f zł z konta %s. Aktualne saldo: %.2f zł" kwota numerKonta saldo
            true
        else
            printfn "Błąd: Nieprawidłowa kwota wypłaty lub niewystarczające środki"
            false

type Bank() =
    let mutable konta = Map.empty<string, KontoBankowe>
    
    member this.UtworzKonto(numerKonta: string) =
        match konta.TryFind numerKonta with
        | None ->
            let noweKonto = KontoBankowe(numerKonta)
            konta <- konta.Add(numerKonta, noweKonto)
            printfn "Utworzono nowe konto o numerze: %s" numerKonta
            true
        | Some _ ->
            printfn "Błąd: Konto o numerze %s już istnieje" numerKonta
            false
    
    member this.PobierzKonto(numerKonta: string) =
        match konta.TryFind numerKonta with
        | Some konto -> 
            printfn "Znaleziono konto %s. Saldo: %.2f zł" numerKonta konto.Saldo
            Some konto
        | None ->
            printfn "Błąd: Nie znaleziono konta o numerze %s" numerKonta
            None
    
    member this.AktualizujSaldo(numerKonta: string, noweOperacje: KontoBankowe -> bool) =
        match konta.TryFind numerKonta with
        | Some konto ->
            noweOperacje konto
        | None ->
            printfn "Błąd: Nie znaleziono konta o numerze %s" numerKonta
            false
    
    member this.UsunKonto(numerKonta: string) =
        match konta.TryFind numerKonta with
        | Some _ ->
            konta <- konta.Remove(numerKonta)
            printfn "Usunięto konto o numerze: %s" numerKonta
            true
        | None ->
            printfn "Błąd: Nie można usunąć konta, które nie istnieje"
            false

[<EntryPoint>]
let main argv =
    let bank = Bank()
    
    printfn "\n--- Tworzenie kont ---"
    bank.UtworzKonto("001") |> ignore
    bank.UtworzKonto("002") |> ignore
    bank.UtworzKonto("001") |> ignore // próba utworzenia duplikatu
    
    printfn "\n--- Wpłaty na konta ---"
    bank.AktualizujSaldo("001", fun konto -> konto.Wplac(1000.0)) |> ignore
    bank.AktualizujSaldo("002", fun konto -> konto.Wplac(500.0)) |> ignore
    
    printfn "\n--- Odczyt stanu kont ---"
    bank.PobierzKonto("001") |> ignore
    bank.PobierzKonto("002") |> ignore
    bank.PobierzKonto("003") |> ignore // próba odczytu nieistniejącego konta
    
    printfn "\n--- Wypłaty z kont ---"
    bank.AktualizujSaldo("001", fun konto -> konto.Wyplac(200.0)) |> ignore
    bank.AktualizujSaldo("002", fun konto -> konto.Wyplac(600.0)) |> ignore // próba wypłaty kwoty większej niż saldo
    
    printfn "\n--- Usuwanie kont ---"
    bank.UsunKonto("001") |> ignore
    bank.PobierzKonto("001") |> ignore // próba odczytu usuniętego konta
    
    0