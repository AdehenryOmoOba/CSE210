using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] arg)
    {
        string reference;
        ScriptureReference parsedReference = new ScriptureReference("John 3:16");
        if (parsedReference.EndVerse == null)
        {
            reference = $"{parsedReference.Book} {parsedReference.Chapter}:{parsedReference.StartVerse}";
        }
        else
        {
            reference = $"{parsedReference.Book} {parsedReference.Chapter}:{parsedReference.StartVerse}-{parsedReference.EndVerse}";
        }

        Scripture scripture = new Scripture(reference, "For God so loved the world, that he gave his only Son, that whoever believes in him should not perish but have eternal life.");

        DisplayScripture(scripture);

        do
        {

            Console.WriteLine("Press Enter to hide words or type 'quit' to end:");
            string input = Console.ReadLine() ?? "";

            if (input.ToLower() == "quit")
            {
                break;
            }

            string newString = HideRandomWords(scripture);
            Scripture scriptureWithUnderscores = new Scripture(reference, newString);
            DisplayScripture(scriptureWithUnderscores);
        } while (!scripture.AllWordsHidden);
    }

    static void DisplayScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine($"{scripture.Reference}  {scripture.Text}");
    }

    static string HideRandomWords(Scripture scripture)
    {
        Random random = new Random();
        List<string> wordsToHide = scripture.Words.Where(word => !word.IsHidden).Select(word => word.Text).ToList();

        string newString = "";

        if (wordsToHide.Count > 1)
        {
            int randomIndex1 = random.Next(wordsToHide.Count);
            int randomIndex2;

            do
            {
                randomIndex2 = random.Next(wordsToHide.Count);
            } while (randomIndex2 == randomIndex1);

            string wordToHide1 = wordsToHide[randomIndex1];
            string wordToHide2 = wordsToHide[randomIndex2];

            // int wordToHideLength = wordToHide1.Length;


            foreach (Word word in scripture.Words)
            {
                if (word.Text == wordToHide1 || word.Text == wordToHide2)
                {
                    string underscores = ReplaceWithUnderscore(word.Text);
                    word.Text = underscores;
                    word.Hide();
                }

                newString += word.Text + " ";
            }
        }
        else
        {
            scripture.HideAllWords();
        }
        return newString;
    }

    static string ReplaceWithUnderscore(string word)
    {
        // Replace each letter of the word with underscore
        char[] underscores = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            underscores[i] = '_';
        }

        return new string(underscores);
    }
}

class Scripture
{
    public string Reference { get; private set; }
    public string Text { get; private set; }
    public List<Word> Words { get; private set; }

    public bool AllWordsHidden => Words.All(word => word.IsHidden);

    public Scripture(string reference, string text)
    {
        Reference = reference;
        Text = text;
        Words = InitializeWords();
    }

    private List<Word> InitializeWords()
    {
        // Split the text into words
        string[] wordArray = Text.Split(' ');
        return wordArray.Select(wordText => new Word(wordText)).ToList();
    }

    public void HideAllWords()
    {
        foreach (Word word in Words)
        {
            word.Hide();
        }
    }
}

class ScriptureReference
{
    public string Book { get; private set; } = "";
    public int Chapter { get; private set; }
    public int? StartVerse { get; private set; }
    public int? EndVerse { get; private set; }

    public ScriptureReference(string reference)
    {
        ParseReference(reference);
    }

    private void ParseReference(string reference)
    {
        // Split the reference into parts
        string[] parts = reference.Split(' ');

        // Assuming the reference format is "Book Chapter:StartVerse-EndVerse"
        if (parts.Length >= 2)
        {
            Book = parts[0];

            // Parse Chapter
            if (int.TryParse(parts[1].Split(':')[0], out int chapter))
            {
                Chapter = chapter;
            }

            // Parse StartVerse and EndVerse if available
            if (parts[1].Contains(':'))
            {
                string[] verseParts = parts[1].Split(':')[1].Split('-');

                if (int.TryParse(verseParts[0], out int startVerse))
                {
                    StartVerse = startVerse;
                }

                if (verseParts.Length == 2 && int.TryParse(verseParts[1], out int endVerse))
                {
                    EndVerse = endVerse;
                }
            }
        }
    }

}

class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}
