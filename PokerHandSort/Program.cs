using System;
using System.IO;
using System.Text.RegularExpressions;
using PokerHandSort.Models;
using System.Linq;

namespace PokerHandSort
{
    class Program
    {
        public const string TIE = "Nobody Wins. THis is a Tie!.";
        static void Main(string[] args)
        {
            int winsPlayer1 = 0;
            int winsPlayer2 = 0;
            Console.WriteLine(@"Please enter file name with full path (eg: C:\Users\PokerHandSort\poker-hands.txt)");
            string Filename = Console.ReadLine();
            string textFile = Filename;
            if (File.Exists(textFile))
            {
                Console.WriteLine("Has Document");
            try
            {   // Open the text file using a stream reader.
                using (StreamReader br = new StreamReader(Filename))
                {
                    while (true)
                    {
                        string input = br.ReadLine();
                        if(string.ReferenceEquals(input,null))
                        {
                            break;
                        }
                        String check = "(?:[2-9TJQKA][SCHD] ){9}[2-9TJQKA][SCHD]";
                        Match match = Regex.Match(input, check);
                        if (match.Success == false)
                        {
                            Console.WriteLine("Input Format is Wrong.");
                            break;
                        }
                        String[] cards = input.Split(" ");

                        //Splitting action
                        String[] handOneStr = cards.Take(5).ToArray();
                        String[] handTwoStr = cards.Skip(5).ToArray();

                        //Sorting
                        Hand.Sorting(handOneStr);
                        Hand.Sorting(handTwoStr);
                                                
                        Hand handOne = new Hand(handOneStr);
                        Hand handTwo = new Hand(handTwoStr);

                        handOne.Evaluate();
                        handTwo.Evaluate();

                        //Checking who is a winner returns 1 is Player 1 wins

                        int res = Hand.Winner(handOne, handTwo);
                        if (res == 1)
                        {
                            winsPlayer1++;
                        }
                        else if (res == 2)
                        {
                            winsPlayer2++;
                        }
                        else
                        {
                            Console.WriteLine(TIE);
                        }

                    }
                    Console.WriteLine("Player 1: " + winsPlayer1 + " hands");
                    Console.WriteLine("Player 2: " + winsPlayer2 + " hands");
                    Console.WriteLine("\nPress any key to Exit...");
                    Console.ReadKey();                
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            }
            else
            {
                Console.WriteLine("The Text file that you input dosen't exist");
            }
               
        }

    }
}
