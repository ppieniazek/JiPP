# employee.py
class Employee:
    def __init__(self, name, age, salary):
        self.name = name
        self.age = age
        self.salary = salary

    def __str__(self):
        return f"{self.name}, {self.age} lat, {self.salary} PLN"


# employees_manager.py
class EmployeesManager:
    def __init__(self):
        self.employees = []

    def add_employee(self, name, age, salary):
        employee = Employee(name, age, salary)
        self.employees.append(employee)
        return "Pracownik został pomyślnie dodany"

    def display_employees(self):
        if not self.employees:
            return "Brak pracowników w systemie"

        result = []
        for employee in self.employees:
            result.append(str(employee))
        return "\n".join(result)

    def remove_employees_by_age_range(self, min_age, max_age):
        initial_count = len(self.employees)
        self.employees = [emp for emp in self.employees
                          if emp.age < min_age or emp.age > max_age]
        removed_count = initial_count - len(self.employees)
        return f"Usunięto {removed_count} pracownika(ów) w przedziale wieku {min_age}-{max_age}"

    def find_employee_by_name(self, name):
        for employee in self.employees:
            if employee.name.lower() == name.lower():
                return employee
        return None

    def update_salary_by_name(self, name, new_salary):
        employee = self.find_employee_by_name(name)
        if employee:
            employee.salary = new_salary
            return f"Zaktualizowano wynagrodzenie dla pracownika - {name}"
        return f"Nie znaleziono pracownika o nazwisku - {name}"


# frontend_manager.py
class FrontendManager:
    def __init__(self):
        self.manager = EmployeesManager()

    def run(self):
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


# main.py
if __name__ == "__main__":
    frontend = FrontendManager()
    frontend.run()