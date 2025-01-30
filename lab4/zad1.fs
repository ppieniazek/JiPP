open System
//Zadanie 1 bmi = kg/m2

type User = {Waga: float; Wzrost: float}

let obliczBMI (waga: float) (wzrost: float) = 
    let wzrostMetry = wzrost / 100.0
    waga / (wzrostMetry * wzrostMetry)

let okreslKategorieBMI bmi =
    if bmi < 18.5 then "niedowaga"
    elif bmi < 25 then "prawidłowa"
    elif bmi < 30 then "nadwaga"
    elif bmi < 35 then "otyłość 1 stopnia"
    else "otyłość 2 stopnia"

let okreslMatchBMI bmi = 
    match bmi with
        | _ when bmi < 18.5 -> "niedowaga"
        | _ when bmi < 25 -> "prawidłowa"
        | _ when bmi < 30 -> "nadwaga"
        | _ when bmi < 35 -> "otyłość 1 stopnia"
        | _ -> "otyłość 2 stopnia"


let main() = 
    printfn "Podaj swoją wagę w kg: "
    let waga = System.Console.ReadLine() |> float
    printfn "Podaj swój wzrost w cm."
    let wzrost = System.Console.ReadLine() |> float

    let user = {Waga = waga; Wzrost = wzrost}

    let bmi = obliczBMI user.Waga user.Wzrost
    let kategoria = okreslKategorieBMI bmi

    printfn "Twoje BMI wynosi: %.2f" bmi
    printfn "Twoja kategoria BMI to: %s" kategoria

main()