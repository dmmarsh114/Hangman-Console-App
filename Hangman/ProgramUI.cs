using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class ProgramUI
    {
        bool playing = true;
        int errorCount = 0;
        int maxErrorCount = 5;
        string word;
        char[] currentGuess;
        List<char> pastGuesses = new List<char>();

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to Hangman!\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Please select a Category:\n" +
                "1. Colors\n" +
                "2. Food\n" +
                "3. The Office Characters (First Name Only)\n" +
                "4. Ron Swanson\n");
            string input = Console.ReadLine();
            word = PickWord(input);
            currentGuess = new char[word.Length];

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine($"I'm thinking of a word that has {word.Length} letters.");
            PlayGame();
        }

        private void PlayGame()
        {
            while (playing)
            {
                Console.WriteLine("Please pick a single letter.");
                char guess;

                try
                {
                    guess = Convert.ToChar(Console.ReadLine().ToUpper());
                    if (guess == ' ' || !Char.IsLetter(guess))
                    {
                        Console.Clear();
                        Console.WriteLine("That is an invalid guess.");
                    }
                    else
                    {
                        EvaluateGuess(guess, word);
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("That is an invalid guess.");
                }
            }
        }

        private string PickWord(string input)
        {
            string[] ColorWords = { "RED", "WHITE", "BLUE", "PURPLE", "CHARTREUSE", "CORNFLOWERBLUE", "WHITESMOKE", "FUCHSIA" };
            string[] FoodWords = { "YAM", "BANANA", "ORANGE", "COFFEE", "STEAK", "CURRY", "SHRIMP", "PIE", "RAVIOLI" };
            string[] OfficeCharacters = { "JIM", "DWIGHT", "PAM", "KELLY", "PHYLLIS", "MICHAEL", "TOBY", "KEVIN" };
            string[] RonSwanson = { "BACON", "EGGS", "BREAKFAST", "WHISKEY", "DUKESILVER", "TAMMY" };

            string category;
            string wordToPick;
            var random = new Random();
            switch (input)
            {
                case "1":
                    category = "Colors";
                    wordToPick = ColorWords[random.Next(ColorWords.Length)];
                    break;
                case "2":
                    category = "Food";
                    wordToPick = FoodWords[random.Next(FoodWords.Length)];
                    break;
                case "3":
                    category = "Office Characters";
                    wordToPick = OfficeCharacters[random.Next(OfficeCharacters.Length)];
                    break;
                case "4":
                    category = "Ron Swanson";
                    wordToPick = RonSwanson[random.Next(RonSwanson.Length)];
                    break;
                default:
                    category = "Office Characters";
                    wordToPick = OfficeCharacters[random.Next(OfficeCharacters.Length)];
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You have chosen the {category} category.");
            Console.ForegroundColor = ConsoleColor.White;
            return wordToPick;
        }

        private void EvaluateGuess(char guess, string word)
        {
            if (pastGuesses.Contains(guess))
            {
                Console.WriteLine($"You already guessed {guess}, you dingus.");
            }
            else if (word.Contains(guess))
            {
                CorrectGuess(guess, word);
            }
            else
            {
                IncorrectGuess(word);
            }
            pastGuesses.Add(guess);
        }

        private void IncorrectGuess(string word)
        {
            Console.Clear();
            Console.WriteLine($"Wrong!\n" +
                $"You have {maxErrorCount - errorCount} guess(es) left.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{string.Join("", currentGuess)}");
            Console.ForegroundColor = ConsoleColor.White;

            errorCount++;
            // Check for player defeat
            if (errorCount > maxErrorCount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You lose!\n" +
                    $"The word was \"{word}\".\n" +
                    "Press any key to exit in shame.");
                Console.ReadKey();
                playing = false;
            }
        }

        private void CorrectGuess(char guess, string word)
        {
            Console.Clear();
            Console.WriteLine("Correct!");

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == guess)
                {
                    currentGuess[i] = guess;
                }
                else if (!Char.IsLetter(currentGuess[i]))
                {
                    currentGuess[i] = '_';
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Join("", currentGuess));
            Console.ForegroundColor = ConsoleColor.White;

            // Check for victory
            if (string.Join("", currentGuess) == word)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You won!\n" +
                    "Press any key to exit.");
                Console.ReadKey();
                playing = false;
            }
        }
    }
}