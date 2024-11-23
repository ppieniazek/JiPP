import json
from pathlib import Path


class Authentication:
    def __init__(self):
        self.admin_credentials = {
            "username": "admin",
            "password": "admin"
        }

    def login(self):
        print("\n=== System Logowania ===")
        attempts = 3
        while attempts > 0:
            username = input("Login: ")
            password = input("Hasło: ")

            if (username == self.admin_credentials["username"] and
                    password == self.admin_credentials["password"]):
                print("Zalogowano pomyślnie!")
                return True

            attempts -= 1
            print(f"Nieprawidłowe dane logowania. Pozostało prób: {attempts}")

        print("Przekroczono limit prób logowania.")
        return False


class Employee:
    def __init__(self, name, age, salary):
        self.name = name
        self.age = age
        self.salary = salary

    def __str__(self):
        return f"Imię i Nazwisko: {self.name}, Wiek: {self.age}, Wynagrodzenie: {self.salary} zł"

    def to_dict(self):
        return {
            "name": self.name,
            "age": self.age,
            "salary": self.salary
        }

    @classmethod
    def from_dict(cls, data):
        return cls(data["name"], data["age"], data["salary"])


class EmployeesManager:
    def __init__(self):
        self.employees = []
        self.data_file = Path("employees_data.json")
        self.load_data()

    def load_data(self):
        try:
            if self.data_file.exists():
                with open(self.data_file, 'r', encoding='utf-8') as file:
                    data = json.load(file)
                    self.employees = [Employee.from_dict(emp) for emp in data]
            else:
                self.save_data()  # Create empty file if doesn't exist
        except json.JSONDecodeError:
            print("Błąd odczytu pliku. Tworzenie nowego pliku danych.")
            self.save_data()

    def save_data(self):
        data = [emp.to_dict() for emp in self.employees]
        with open(self.data_file, 'w', encoding='utf-8') as file:
            json.dump(data, file, ensure_ascii=False, indent=4)

    def add_employee(self, name, age, salary):
        if not self._validate_employee_data(name, age, salary):
            return "Nieprawidłowe dane pracownika"

        employee = Employee(name, age, salary)
        self.employees.append(employee)
        self.save_data()
        return "Pracownik został pomyślnie dodany"

    def _validate_employee_data(self, name, age, salary):
        if not name.strip():
            print("Imię i nazwisko nie może być puste")
            return False
        if not isinstance(age, int) or age < 18 or age > 100:
            print("Wiek musi być liczbą całkowitą między 18 a 100")
            return False
        if not isinstance(salary, (int, float)) or salary < 0:
            print("Wynagrodzenie musi być liczbą dodatnią")
            return False
        return True

    def display_employees(self):
        if not self.employees:
            return "Brak pracowników w systemie"

        result = []
        for employee in self.employees:
            result.append(str(employee))
        return "\n".join(result)

    def remove_employees_by_age_range(self, min_age, max_age):
        if not self._validate_age_range(min_age, max_age):
            return "Nieprawidłowy zakres wieku"

        initial_count = len(self.employees)
        self.employees = [emp for emp in self.employees
                          if emp.age < min_age or emp.age > max_age]
        removed_count = initial_count - len(self.employees)
        self.save_data()
        return f"Usunięto {removed_count} pracownika(ów) w przedziale wieku {min_age}-{max_age}"

    def _validate_age_range(self, min_age, max_age):
        if min_age < 18 or max_age > 100 or min_age > max_age:
            print("Nieprawidłowy zakres wieku (min: 18, max: 100)")
            return False
        return True

    def find_employee_by_name(self, name):
        if not name.strip():
            print("Imię i nazwisko nie może być puste")
            return None

        for employee in self.employees:
            if employee.name.lower() == name.lower():
                return employee
        return None

    def update_salary_by_name(self, name, new_salary):
        if not isinstance(new_salary, (int, float)) or new_salary < 0:
            return "Nieprawidłowa wartość wynagrodzenia"

        employee = self.find_employee_by_name(name)
        if employee:
            employee.salary = new_salary
            self.save_data()
            return f"Zaktualizowano wynagrodzenie dla pracownika {name}"
        return f"Nie znaleziono pracownika o nazwisku {name}"


class FrontendManager:
    def __init__(self):
        self.auth = Authentication()
        self.manager = EmployeesManager()

    def run(self):
        if not self.auth.login():
            return

        while True:
            self.display_menu()
            choice = input("Wybierz opcję (1-6): ")

            match choice:
                case "1":
                    self.add_employee()
                case "2":
                    print("\nLista Pracowników:")
                    print(self.manager.display_employees())
                case "3":
                    self.remove_employees_by_age()
                case "4":
                    self.update_salary()
                case "5":
                    self.find_employee()
                case "6":
                    print("Zamykanie systemu...")
                    break
                case _:
                    print("Nieprawidłowy wybór. Spróbuj ponownie.")

            print("\n" + "=" * 50 + "\n")

    def display_menu(self):
        print("\nSystem Zarządzania Pracownikami - Menu:")
        print("1. Dodaj Pracownika")
        print("2. Wyświetl Wszystkich Pracowników")
        print("3. Usuń Pracowników według Wieku")
        print("4. Aktualizuj Wynagrodzenie Pracownika")
        print("5. Znajdź Pracownika")
        print("6. Wyjście")

    def add_employee(self):
        print("\nDodawanie Nowego Pracownika:")
        name = input("Podaj imię i nazwisko pracownika: ")
        while True:
            try:
                age = int(input("Podaj wiek pracownika: "))
                salary = float(input("Podaj wynagrodzenie pracownika: "))
                break
            except ValueError:
                print("Proszę podać prawidłowe wartości liczbowe dla wieku i wynagrodzenia.")

        result = self.manager.add_employee(name, age, salary)
        print(result)

    def remove_employees_by_age(self):
        print("\nUsuwanie Pracowników według Przedziału Wieku:")
        try:
            min_age = int(input("Podaj minimalny wiek: "))
            max_age = int(input("Podaj maksymalny wiek: "))
            result = self.manager.remove_employees_by_age_range(min_age, max_age)
            print(result)
        except ValueError:
            print("Proszę podać prawidłowe wartości liczbowe dla przedziału wieku.")

    def update_salary(self):
        print("\nAktualizacja Wynagrodzenia Pracownika:")
        name = input("Podaj imię i nazwisko pracownika: ")
        try:
            new_salary = float(input("Podaj nowe wynagrodzenie: "))
            result = self.manager.update_salary_by_name(name, new_salary)
            print(result)
        except ValueError:
            print("Proszę podać prawidłową wartość liczbową dla wynagrodzenia.")

    def find_employee(self):
        print("\nWyszukiwanie Pracownika:")
        name = input("Podaj imię i nazwisko pracownika: ")
        employee = self.manager.find_employee_by_name(name)
        if employee:
            print(f"\nZnaleziono pracownika:\n{employee}")
        else:
            print(f"Nie znaleziono pracownika o nazwisku {name}")


if __name__ == "__main__":
    frontend = FrontendManager()
    frontend.run()