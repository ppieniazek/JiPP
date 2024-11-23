def analyze_data(data_list):
    numbers = list(filter(lambda x: isinstance(x, (int, float)), data_list))
    max_number = max(numbers) if numbers else None

    strings = list(filter(lambda x: isinstance(x, str), data_list))
    longest_string = max(strings, key=len) if strings else None

    tuples = list(filter(lambda x: isinstance(x, tuple), data_list))
    largest_tuple = max(tuples, key=len) if tuples else None

    return max_number, longest_string, largest_tuple

def test():
    test_data = [
        42,
        "drzewo",
        (1, 2, 3),
        [4, 5, 6],
        {"klucz": "wartosc"},
        3.14,
        "łabędź",
        (1, 2, 3, 4, 5),
        "marchewka",
        100,
        (1,),
        672.55
    ]

    max_num, max_str, max_tup = analyze_data(test_data)
    print(f"Największa liczba: {max_num}")
    print(f"Najdłuższy napis: {max_str}")
    print(f"Największa krotka: {max_tup}")


if __name__ == "__main__":
    test()