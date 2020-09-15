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
        List<string> pastGuesses = new List<string>();

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to Hangman!\n" +
                "Press any key to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();

            word = PickWord();
            currentGuess = new char[word.Length];
            Console.Clear();
            Console.WriteLine($"I'm thinking of a word that has {word.Length} letters.");

            while (playing)
            {
                Console.WriteLine("Please pick a single letter.");
                string guess = Console.ReadLine().ToUpper();

                if (guess == "" || guess.Length > 1)
                {
                    Console.WriteLine("That is an invalid guess.");
                }
                else
                {
                    EvaluateGuess(guess, word);
                }
            }
        }

        public string PickWord()
        {
            string[] Hangmanwords = {
            "HANGMAN", "APPLE", "TOWER", "SMARTPHONE", "PROGRAMMING", "AWKWARD", "BANJO",
            "DWARVES", "FISHHOOK", "JAZZY", "JUKEBOX", "MEMENTO", "MYSTIFY", "OXYGEN", "PIXEL",
            "ZOMBIE", "NUMBSKULL", "BAGPIPES", "COMPUTER", "EASTER", "CHRISTMAS", "COFFEE",
            };

            var random = new Random();
            return Hangmanwords[random.Next(Hangmanwords.Length)];
        }

        public void EvaluateGuess(string guess, string word)
        {
            // ============== ALREADY GUESSED ==============
            if (pastGuesses.Contains(guess))
            {
                Console.WriteLine($"You already guessed {guess}, you dingus.");
            }
            // ============== CORRECT GUESS ==============
            else if (word.Contains(guess))
            {
                Console.Clear();
                Console.WriteLine("Correct!");
                char guessChar = Convert.ToChar(guess);
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == guessChar)
                    {
                        currentGuess[i] = guessChar;
                    }
                    else if (!Char.IsLetter(currentGuess[i]))
                    {
                        currentGuess[i] = '_';
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Join("", currentGuess));
                Console.ForegroundColor = ConsoleColor.White;

                if (string.Join("", currentGuess) == word)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You won!\n" +
                        "Press any key to exit.");
                    Console.ReadKey();
                    playing = false;
                }
            }
            // ============== INCORRECT GUESS ==============
            else
            {
                Console.Clear();
                Console.WriteLine($"Wrong!\n" +
                    $"You have {maxErrorCount - errorCount} guess(es) left.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{string.Join("", currentGuess)}");
                Console.ForegroundColor = ConsoleColor.White;

                errorCount++;
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
            pastGuesses.Add(guess);
        }
    }
}