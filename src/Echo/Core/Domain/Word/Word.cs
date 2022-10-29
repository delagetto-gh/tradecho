using System;
using System.Linq;
using Framework.Ddd;

namespace Domain.Word;

public class Word : AggregateRoot<string>
{
    private Word() { }

    private Word(string text) : base(text) => Text = text;

    public string Text { get; private set; }

    public static Word FromText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException(nameof(text));

        if (text.Any(o => Char.IsWhiteSpace(o)))
            throw new ArgumentException(nameof(text));

        return new Word(text);
    }

    public void Echo()
    {
        // validation + state change logic goes here
        // this partiuclar behaviour doesnt require any state change 

        //...raise event (something happened)
        AddEvent(new WordEchoed(Text));
    }
}
