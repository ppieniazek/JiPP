# Zadanie 2 - BFS
from collections import deque
def z2(graph, start, end):
    queue = deque([[start]])
    visited = set()

    while queue:
        path = queue.popleft()
        node = path[-1]

        if node == end:
            return path

        if node not in visited:
            for neighbor in graph.get(node, []):
                new_path = list(path)
                new_path.append(neighbor)
                queue.append(new_path)
            visited.add(node)

    return None


graf = {
    '1': ['2', '3', '5'],
    '2': ['1', '3', '4', '5'],
    '3': ['1', '2', '4'],
    '4': ['2', '3', '5'],
    '5': ['1', '2', '4']
}

print(z2(graf, '1', '5'))