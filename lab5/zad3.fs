let rec permutacje lista =
    let rec usunElement x = function
        | [] -> []
        | h::t -> if h = x then t else h :: usunElement x t
    
    match lista with
    | [] -> [ [] ]
    | _ -> [ for element in lista do
                for reszta in permutacje (usunElement element lista) do
                    element :: reszta ]

// Przykłady użycia
printfn "Permutacje [1;2;3]: %A" (permutacje [1;2;3])
printfn "Permutacje [2;5]: %A" (permutacje [2;5])
printfn "Permutacje pustej listy: %A" (permutacje [])