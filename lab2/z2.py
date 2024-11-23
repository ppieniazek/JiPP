import numpy as np

def validate_matrix(matrix_str):
    try:
        matrix = eval(matrix_str)
        if not isinstance(matrix, np.ndarray) or matrix.ndim != 2:
            raise ValueError("Niepoprawna macierz")
    except (SyntaxError, NameError, TypeError):
        raise ValueError("Niepoprawna macierz")
    return matrix

def validate_matrices(matrix1_str, matrix2_str):
    matrix1 = validate_matrix(matrix1_str)
    matrix2 = validate_matrix(matrix2_str)
    if matrix1.shape != matrix2.shape:
        raise ValueError("Niezgodne wymiary macierzy")
    return matrix1, matrix2

def validate_matrix_operation(operation_str, matrix1_str, matrix2_str=None):
    if operation_str not in ["dodawanie", "mnożenie", "transponowanie"]:
        raise ValueError("Niepoprawna operacja")
    if operation_str == "transponowanie" and matrix2_str is not None:
        raise ValueError("Transponowanie wymaga tylko jednej macierzy")
    if operation_str != "transponowanie" and matrix2_str is None:
        raise ValueError(f"{operation_str.capitalize()} wymaga dwóch macierzy")

def perform_matrix_operation(operation_str, matrix1_str, matrix2_str=None):
    validate_matrix_operation(operation_str, matrix1_str, matrix2_str)
    if operation_str == "transponowanie":
        matrix = validate_matrix(matrix1_str)
        result = matrix.T
    else:
        matrix1, matrix2 = validate_matrices(matrix1_str, matrix2_str)
        if operation_str == "dodawanie":
            result = matrix1 + matrix2
        elif operation_str == "mnożenie":
            result = np.dot(matrix1, matrix2)
    return result

# Przykłady użycia
matrix1 = "np.array([[1, 2], [3, 4]])"
matrix2 = "np.array([[5, 6], [7, 8]])"

try:
    result = perform_matrix_operation("dodawanie", matrix1, matrix2)
    print("Wynik dodawania:")
    print(result)
except ValueError as e:
    print(f"Błąd: {str(e)}")

try:
    result = perform_matrix_operation("mnożenie", matrix1, matrix2)
    print("Wynik mnożenia:")
    print(result)
except ValueError as e:
    print(f"Błąd: {str(e)}")

try:
    result = perform_matrix_operation("transponowanie", matrix1)
    print("Wynik transponowania:")
    print(result)
except ValueError as e:
    print(f"Błąd: {str(e)}")