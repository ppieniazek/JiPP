type Ksiazka(tytul: string, autor: string, liczbaStron: int) =
    member this.Tytul = tytul
    member this.Autor = autor
    member this.LiczbaStron = liczbaStron
    
    member this.PobierzInfo() =
        sprintf "Książka: %s, Autor: %s, Liczba stron: %d" tytul autor liczbaStron

type Uzytkownik(imie: string) =
    let mutable wypozyczoneKsiazki = List.empty<Ksiazka>
    
    member this.Imie = imie
    member this.WypozyczoneKsiazki = wypozyczoneKsiazki
    
    member this.WypozyczKsiazke(ksiazka: Ksiazka) =
        wypozyczoneKsiazki <- ksiazka :: wypozyczoneKsiazki
        printfn "Użytkownik %s wypożyczył książkę: %s" imie (ksiazka.PobierzInfo())
        
    member this.ZwrocKsiazke(ksiazka: Ksiazka) =
        wypozyczoneKsiazki <- wypozyczoneKsiazki |> List.filter (fun k -> k <> ksiazka)
        printfn "Użytkownik %s zwrócił książkę: %s" imie (ksiazka.PobierzInfo())

type Biblioteka() =
    let mutable ksiazki = List.empty<Ksiazka>
    
    member this.DodajKsiazke(ksiazka: Ksiazka) =
        ksiazki <- ksiazka :: ksiazki
        printfn "Dodano książkę: %s" (ksiazka.PobierzInfo())
        
    member this.UsunKsiazke(ksiazka: Ksiazka) =
        ksiazki <- ksiazki |> List.filter (fun k -> k <> ksiazka)
        printfn "Usunięto książkę: %s" (ksiazka.PobierzInfo())
        
    member this.WyswietlKsiazki() =
        printfn "\nLista książek w bibliotece:"
        ksiazki |> List.iter (fun k -> printfn "%s" (k.PobierzInfo()))

[<EntryPoint>]
let main argv =
    let biblioteka = Biblioteka()
    let uzytkownik = Uzytkownik("Jan")
    
    let ksiazka1 = Ksiazka("Pan Tadeusz", "Adam Mickiewicz", 376)
    let ksiazka2 = Ksiazka("Lalka", "Bolesław Prus", 890)
    
    biblioteka.DodajKsiazke(ksiazka1)
    biblioteka.DodajKsiazke(ksiazka2)
    
    biblioteka.WyswietlKsiazki()
    
    uzytkownik.WypozyczKsiazke(ksiazka1)
    biblioteka.UsunKsiazke(ksiazka1)
    biblioteka.WyswietlKsiazki()
    
    uzytkownik.ZwrocKsiazke(ksiazka1)
    biblioteka.DodajKsiazke(ksiazka1)
    biblioteka.WyswietlKsiazki()
    
    0