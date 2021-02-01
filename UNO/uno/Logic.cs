using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace uno
{
    class Logic
    {
        public void ShowDeck(List<string[]> deck, string name, string[] pile) //Func that Prints the players deck to the console
        {
            Console.WriteLine($"{name}'s turn:\nCards below:");
            string output;
            for (int i = 0; i < deck.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                if (deck[i][1] == "Yellow")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                if (deck[i][1] == "Blue")
                    Console.ForegroundColor = ConsoleColor.Blue;
                if (deck[i][1] == "Red")
                    Console.ForegroundColor = ConsoleColor.Red;
                if (deck[i][1] == "Green")
                    Console.ForegroundColor = ConsoleColor.Green;
                output = $"{deck[i][0]}, {deck[i][1]}\n";
                Console.Write(output);
                Console.ResetColor();
            }
            Console.WriteLine($"Card on the top of the pile is {pile[0]}, {pile[1]}");
            Console.WriteLine("");
        }
        public List<string[]> ChangeCard(List<string[]> deck, List<string[]> card, int pos) //Func may not be implemented or fully built
        {
            //deck[pos] = card;
            return deck;
        }
        public string[] CollectNames(int playerCount) //func that collects players names
        {
            string[] names = new string[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                Console.Write($"Enter Name for player {i + 1}\n>>> ");
                string name = Console.ReadLine();
                names[i] = name;
            }
            //Done here just to clean up Run.cs
            Console.WriteLine($"Push Enter to start {names[0]}'s turn");
            Console.ReadLine();
            return names;
        }
        public List<string[]> Plus(List<string[]> deck, string card) //adds cards to a players deck
        {
            Deck deckGen = new Deck();
            int repeat;
            if(card == "+2")
            {
                repeat = 2;
            }
            else
            {
                repeat = 4;
            }
            for (int i = 0; i < repeat; i++)
            {
                deck.Add(deckGen.GenerateCard());
            }
            return deck;
        }
        public (string[], List<string[]>) Play(string[] oldPile, List<string[]> playersDeck)
        /* Broken atm
         * Make them have to choose a card with the same number or colour
         * optimise
         * clean up and simplify code
         */
        {
            /*
            Deck deck = new Deck();
            //add the pickup card with just enter
            Console.Write("What Card do you want to play (Enter to pickup card)\n>>> ");
            string whatCard = Console.ReadLine();
            if (whatCard == "" || whatCard == "\n")
            {
                playersDeck.Add(deck.GenerateCard());
                Console.Clear();
                return (oldPile, playersDeck);
            }
            //at the moment any card can be chosen
            int pos = 0;
            //while (!whatCard.Any(c => c < '1' || c > Convert.ToChar(playersDeck.Count))){
            while (!int.TryParse(whatCard, out pos) || (pos < 1 && pos > playersDeck.Count + 1)) {
                Console.Write("Please enter a valid value\n>>> ");
                whatCard = Console.ReadLine();
            }
            pos = Convert.ToInt32(whatCard);
            if (oldPile[0] == playersDeck[pos - 1][0] || oldPile[1] == playersDeck[pos - 1][1])
            {
                string[] newPile = new string[2];
                newPile[0] = playersDeck[pos - 1][0];
                newPile[1] = playersDeck[pos - 1][1];
                Console.Clear();
                playersDeck.RemoveAt(pos - 1);
                return (newPile, playersDeck);
            }
            while (oldPile[0] == playersDeck[pos - 1][0] || oldPile[1] == playersDeck[pos - 1][1])
            {
                Console.Write("Please enter a valid value\n>>> ");
                whatCard = Console.ReadLine();
            }
            string[] newPilee = new string[2];
            newPilee[0] = playersDeck[pos - 1][0];
            newPilee[1] = playersDeck[pos - 1][1];
            playersDeck.RemoveAt(pos - 1);
            Console.Clear();
            return (newPilee, playersDeck);
            */ //This is old code deos not work
            Deck deck = new Deck();
            List<int> validIndexes = GetValidIndexes(oldPile, playersDeck);
            int index = GetRangeInput(validIndexes);
            if (index == 1000)
            {
                playersDeck.Add(deck.GenerateCard());
                return (oldPile, playersDeck);
            }

            string[] newPile = playersDeck[index];
            playersDeck.RemoveAt(index);
            return (newPile, playersDeck);
        }
        private int GetNumericInput()
        {
            Console.Write("Enter the index of the card you want to play\n>>> ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out int asd))
            {
                if (input == "" || input == "\n")
                    return 1000;
                Console.Write("Value not valid\nEnter the index of the card you want to play\n>>> ");
                input = Console.ReadLine();
            }
            int numIndex = int.Parse(input);
            return numIndex;
        }
        private int GetRangeInput(List<int> validIndexes)
        {
            int index = GetNumericInput();
            //bool valid = false;
            while (true)
            {
                for (int i = 0; i < validIndexes.Count; i++)
                {
                    if (index -1 == validIndexes[i] || index == 1000)
                    {
                        //valid = !valid;
                        if (index < 999)
                            return index - 1;
                        return index;
                    }
                }
                index = GetNumericInput();
            }
        }
        private List<int> GetValidIndexes(string[] pile, List<string[]> deck)
        {
            List<int> validIndexes = new List<int>();
            for (int i = 0; i < deck.Count; i++)
            {
                if (pile[0] == deck[i][0] || pile[1] == deck[i][1])
                {
                    validIndexes.Add(i);
                }
            }
            return validIndexes;
        }
    }
}
