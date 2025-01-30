// Program analizujący tekst
open System

// Funkcja do liczenia słów
let countWords (text: string) =
    text.Split([|' '; '\n'; '\t'; '\r'|], StringSplitOptions.RemoveEmptyEntries).Length

// Funkcja do liczenia znaków (bez spacji)
let countCharsWithoutSpaces (text: string) =
    text.Replace(" ", "").Length

// Funkcja znajdująca najczęściej występujące słowo
let findMostFrequentWord (text: string) =
    text.Split([|' '; '\n'; '\t'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun word -> word.ToLower())
    |> Array.groupBy id
    |> Array.maxBy (fun (_, group) -> Array.length group)
    |> fst

[<EntryPoint>]
let main argv =
    printfn "Wprowadź tekst do analizy:"
    let text = Console.ReadLine()
    
    let wordCount = countWords text
    let charCount = countCharsWithoutSpaces text
    let mostFrequent = findMostFrequentWord text
    
    printfn "\nAnaliza tekstu:"
    printfn "Liczba słów: %d" wordCount
    printfn "Liczba znaków (bez spacji): %d" charCount
    printfn "Najczęściej występujące słowo: %s" mostFrequent
    
    0