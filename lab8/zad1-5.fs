type ListaLaczona<'T> =
    | Pusta
    | Wezel of 'T * ListaLaczona<'T>

let rec utworzListeLaczona (lista: List<'T>) : ListaLaczona<'T> =
    match lista with
    | [] -> Pusta
    | glowa::ogon -> Wezel(glowa, utworzListeLaczona ogon)

let rec sumaListy (lista: List<int>) : int =
    match lista with
    | [] -> 0
    | glowa::ogon -> glowa + sumaListy ogon

let znajdzMinMax (lista: List<int>) : int * int =
    let rec pomocnicza (lista: List<int>) (aktualnyMin: int) (aktualnyMax: int) : int * int =
        match lista with
        | [] -> (aktualnyMin, aktualnyMax)
        | glowa::ogon ->
            let nowyMin = if glowa < aktualnyMin then glowa else aktualnyMin
            let nowyMax = if glowa > aktualnyMax then glowa else aktualnyMax
            pomocnicza ogon nowyMin nowyMax
    match lista with
    | [] -> failwith "Lista jest pusta"
    | glowa::ogon -> pomocnicza ogon glowa glowa

let rec odwrocListe (lista: List<'T>) : List<'T> =
    match lista with
    | [] -> []
    | glowa::ogon -> odwrocListe ogon @ [glowa]

let rec czyElementJestWLiście (element: 'T) (lista: List<'T>) : bool =
    match lista with
    | [] -> false
    | glowa::ogon -> glowa = element || czyElementJestWLiście element ogon

let przykladowaLista = [1; 2; 3; 4]

// 1. Tworzenie listy łączonej
let listaLaczona = utworzListeLaczona przykladowaLista
printfn "Lista łączona: %A" listaLaczona  // Wezel (1, Wezel (2, Wezel (3, Wezel (4, Pusta))))

// 2. Sumowanie elementów listy
let suma = sumaListy przykladowaLista
printfn "Suma listy: %d" suma  // Suma: 10

// 3. Znajdowanie min i max w liście
let min, max = znajdzMinMax przykladowaLista
printfn "Min: %d, Max: %d" min max  // Min: 1, Max: 4

// 4. Odwracanie listy
let odwroconaLista = odwrocListe przykladowaLista
printfn "Odwrocona lista: %A" odwroconaLista  // [4; 3; 2; 1]

// 5. Sprawdzanie, czy element jest w liście
let czyJest = czyElementJestWLiście 3 przykladowaLista
printfn "Czy 3 jest w liście? %b" czyJest  // Czy 3 jest w liście? true