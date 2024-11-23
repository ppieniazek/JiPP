from functools import reduce

# Proceduralne podejście
def procedural_task_optimization(tasks):
    tasks = tasks[:]

    n = len(tasks)
    for i in range(n):
        for j in range(0, n - i - 1):
            if (tasks[j][1] / tasks[j][0]) < (tasks[j + 1][1] / tasks[j + 1][0]):
                tasks[j], tasks[j + 1] = tasks[j + 1], tasks[j]

    total_waiting_time = 0
    current_time = 0
    optimal_order = []

    for task in tasks:
        optimal_order.append(task)
        total_waiting_time += current_time
        current_time += task[0]

    return optimal_order, total_waiting_time

# Funkcyjne podejście
def functional_task_optimization(tasks):
    sorted_tasks = sorted(tasks, key=lambda x: x[1] / x[0], reverse=True)

    current_time = 0

    def calculate_waiting_time(acc, task):
        nonlocal current_time
        total_waiting_time = acc + current_time
        current_time += task[0]
        return total_waiting_time

    total_waiting_time = reduce(calculate_waiting_time, sorted_tasks, 0)

    return sorted_tasks, total_waiting_time

def main():
    tasks = [(3, 50), (1, 30), (2, 40), (5, 90), (2, 60), (3, 70)]

    print("Zadania wejściowe:", tasks)

    order_proc, waiting_time_proc = procedural_task_optimization(tasks)
    print("\n=== Proceduralne podejście ===")
    print("Optymalna kolejność:", order_proc)
    print("Całkowity czas oczekiwania:", waiting_time_proc)

    order_func, waiting_time_func = functional_task_optimization(tasks)
    print("\n=== Funkcyjne podejście ===")
    print("Optymalna kolejność:", order_func)
    print("Całkowity czas oczekiwania:", waiting_time_func)

if __name__ == "__main__":
    main()
