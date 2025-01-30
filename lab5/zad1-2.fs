// Zadanie 1 i 2
type Drzewo =
    | Puste
    | Wezel of wartosc:int * lewe:Drzewo * prawe:Drzewo

let rec fibonacci n =
    match n with
    | 0 -> 0
    | 1 -> 1
    | _ -> fibonacci (n - 1) + fibonacci (n - 2)

let fibonacciTail n =
    let rec pomocnicza a b m =
        if m = n then a
        else pomocnicza b (a + b) (m + 1)
    pomocnicza 0 1 0

let rec szukajRekurencyjnie drzewo szukany =
    match drzewo with
    | Puste -> false
    | Wezel(w, l, p) ->
        w = szukany || szukajRekurencyjnie l szukany || szukajRekurencyjnie p szukany

let szukajIteracyjnie drzewo szukany =
    let rec przeszukaj stos =
        match stos with
        | [] -> false
        | Puste :: reszta -> przeszukaj reszta
        | Wezel(w, l, p) :: reszta ->
            if w = szukany then true
            else przeszukaj (l :: p :: reszta)
    przeszukaj [drzewo]

// Przykłady użycia
let testoweDrzewo = 
    Wezel(5,
        Wezel(3,
            Wezel(1, Puste, Puste),
            Wezel(4, Puste, Puste)),
        Wezel(8,
            Wezel(6, Puste, Puste),
            Puste))

printfn "Fib(8) = %d | FibTail(8) = %d" (fibonacci 8) (fibonacciTail 8)
printfn "Drzewo zawiera 4: %b" (szukajRekurencyjnie testoweDrzewo 4)
printfn "Drzewo zawiera 7: %b" (szukajIteracyjnie testoweDrzewo 7)