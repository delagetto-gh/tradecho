using System;
using System.Linq;
using Framework.Ddd;

namespace Domain.Word;

public class Word : AggregateRoot<string>
{
    private Word() { }

    private Word(string text) : base(text) { }

    public static Word FromText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException(nameof(text));

        if (text.Any(o => Char.IsWhiteSpace(o)))
            throw new ArgumentException(nameof(text));

        return new Word(text);
    }

    public void AddTranslation(string translation, string translationLanguage)
    {
        // validation + state change logic goes here

        // ensure translation sint empty or translationlaanguage snt mepty

        //...raise event (something happened)
        AddEvent(new WordTranslated(Id, translation, translationLanguage));
    }
}
