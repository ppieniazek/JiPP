import re
from collections import Counter


def analyze_text(text):
    paragraphs = len([p for p in text.split('\n') if p.strip()])
    sentences = len(re.split('[.!?]+', text))
    words = len(text.split())

    stop_words = {
        'i', 'w', 'na', 'z', 'do', 'od', 'by', 'że', 'a', 'o', 'dla',
        'przy', 'po', 'pod', 'nad', 'przed', 'za', 'się', 'co', 'jak',
        'to', 'tak', 'lub', 'by', 'między', 'dzięki', 'oraz'
    }

    all_words = re.findall(r'\w+', text.lower())

    filtered_words = list(filter(lambda x: x not in stop_words and
                                         not x.isdigit(),
                                 all_words))

    word_counts = Counter(filtered_words).most_common(5)

    words = text.split()
    transformed_words = [
        word[::-1] if word.lower().startswith('a') else word
        for word in words
    ]
    transformed_text = ' '.join(transformed_words)

    return {
        'paragraphs': paragraphs,
        'sentences': sentences,
        'words': words,
        'most_common_words': word_counts,
        'transformed_text': transformed_text
    }


text = """Wprowadzenie Współczesny świat oferuje ogromne możliwości rozwoju dzięki technologii, globalizacji i dostępności edukacji. Jednocześnie stawia przed nami wyzwanie zarządzania czasem i energią, by osiągnąć cele, zachowując równowagę między pracą a życiem prywatnym.
Wyzwania współczesności Nadmiar informacji z mediów, e-maili i reklam powoduje przeciążenie i stres. Kluczowe staje się rozwijanie umiejętności selekcji treści i praktykowanie uważności, by skupić się na tym, co naprawdę ważne.
Rola technologii Technologia ułatwia życie dzięki aplikacjom do zarządzania czasem i automatyzacji procesów. Ważne jednak, by korzystać z tych narzędzi świadomie, by uniknąć uzależnienia od ciągłej dostępności.
Podsumowanie Świadome zarządzanie czasem pozwala osiągnąć równowagę między pracą a życiem osobistym. Dzięki temu możemy cieszyć się sukcesami, dbając jednocześnie o zdrowie psychiczne i relacje z innymi."""

results = analyze_text(text)

print(f"Liczba akapitów: {results['paragraphs']}")
print(f"Liczba zdań: {results['sentences']}")
print(f"Liczba słów: {len(results['words'])}")
print("\nNajczęściej występujące słowa:")
for word, count in results['most_common_words']:
    print(f"{word}: {count} razy")
print("\nTekst po transformacji słów rozpoczynających się na literę 'a':")
print(results['transformed_text'])