using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Android.App;

namespace EmailClient.Droid
{
    public sealed class EmailServer
    {
        public IList<EmailItem> Email {get; private set;}
        readonly string DefaultUserName = "Johnny Appleseed";

        public EmailServer (int number)
        {
            Email = new List<EmailItem>(number);
            Generate (number);
        }

        void Generate (int count)
        {
            DataGenerator dg = new DataGenerator();
            for (int index = 0; index < count; index++) {
                Email.Add (CreateOneEmail (dg));
            }
        }

        EmailItem CreateOneEmail (DataGenerator dg)
        {
            return new EmailItem {
                To = DefaultUserName,
                From = dg.Name,
                Subject = dg.GenerateSentence(dg.RNG.Next(4,8)),
                Body = dg.GenerateParagraphs(dg.RNG.Next(1,5), 1, 5, 1, 20)
            };
        }
    }

    public class DataGenerator
    {
        readonly StringBuilder sentenceBuilder = new StringBuilder(1024);
        readonly StringBuilder paragraphBuilder = new StringBuilder(4096);
        static readonly string[] _words;

        static string[] _firstName = {
            "Alice", "Bob", "Charlie", "David", "Elliot", "Francis", "George",
            "Harry", "Isabelle", "Jackie", "Karen", "Lenny", "Michael", "Nancy",
            "Otto", "Peter", "Ron", "Steve", "Trevor", "Uma", "Violet", "Yvonne", "Zoe"
        };

        static string[] _lastName = {
            "Ackard", "Baker", "Candy", "Duvall", "Ennis", "Finch", "Griswold", "Heck",
            "Jackson", "Kardashian", "Lewis", "Miller", "Nevell", "Octavius", "Parker",
            "Rivest", "Smith", "Taylor", "Stewart"
        };

        public Random RNG = new Random();

        static DataGenerator ()
        {
            List<string> words = new List<string>();
            using (var fs = new StreamReader(Application.Context.Assets.Open("words.txt")))
            {
                while (!fs.EndOfStream)
                {
                    string line = fs.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                        words.Add(line);
                }
            }
            _words = words.ToArray();
        }

        public string Name
        {
            get { return _firstName[RNG.Next(_firstName.Length-1)] + " " + _lastName[RNG.Next(_lastName.Length-1)]; }
        }

        public string GenerateParagraphs(int numberParagraphs, int minSentences,
                                         int maxSentences, int minWords, int maxWords)
        {
            paragraphBuilder.Clear ();

            for (int i = 0; i < numberParagraphs; i++)
            {
                AddParagraph(RNG.Next(minSentences, maxSentences + 1), minWords, maxWords);
                paragraphBuilder.Append("\n\n");
            }

            return paragraphBuilder.ToString ();
        }

        void AddParagraph(int numberSentences, int minWords, int maxWords)
        {
            for (int i = 0; i < numberSentences; i++)
            {
                int count = RNG.Next(minWords, maxWords + 1);
                GenerateSentence(paragraphBuilder, count);
            }
        }

        public string GenerateSentence(int numberOfWords)
        {
            sentenceBuilder.Clear();
            GenerateSentence(sentenceBuilder, numberOfWords);
            return sentenceBuilder.ToString();
        }

        public void GenerateSentence(StringBuilder sb, int numberWords)
        {
            for (int i = 0; i < numberWords; i++)
            {
                if (i > 0)
                    sb.Append(" ");
                
                string word = _words[RNG.Next(_words.Length)];
                if (i == 0)
                    word = char.ToUpper(word[0]) + word.Substring(1);
                sb.Append(word);
            }

            sb.Append(".");
        }
    }

    public sealed class EmailItem
    {
        public string From {get;set;}
        public string To {get;set;}
        public string Subject {get;set;}
        public string Body {get;set;}
        public DateTime Date {get;set;}
        public bool IsRead { get; set; }

        public EmailItem ()
        {
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return From + ": " + Subject;
        }
    }
}

