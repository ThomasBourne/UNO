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
            Console.Write($"Card on the top of the pile is ");
            if (pile[1] == "Yellow")
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (pile[1] == "Blue")
                Console.ForegroundColor = ConsoleColor.Blue;
            if (pile[1] == "Red")
                Console.ForegroundColor = ConsoleColor.Red;
            if (pile[1] == "Green")
                Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{pile[0]}, {pile[1]}");
            Console.ResetColor();
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
        public (string[], List<string[]>) Play(string[] oldPile, List<string[]> playersDeck) //The highest level of card logic
        /* BUGS
         * cant pickup cards
         */
        {
            //creates object of Deck class
            Deck deck = new Deck();

            //valid indexs that of the deck that match the pile card
            List<int> validIndexes = GetValidIndexes(oldPile, playersDeck);
            //the input value
            int index = GetRangeInput(validIndexes);
            if (index == Store.NumForPickup)
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
            Console.Write("Enter the index of the card you want to play (leave blank to pickup)\n>>> ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out int asd))
            {
                if (input == "" || input == "\n") { return Store.NumForPickup; }
                Console.Write("Value not valid\nEnter the index of the card you want to play (leave blank to pickup)\n>>> ");
                input = Console.ReadLine();
            }
            int numIndex = int.Parse(input);
            return numIndex;
        }
        private int GetRangeInput(List<int> validIndexes)
        {
            
            int index = GetNumericInput();
            while (true)
            {
                for (int i = 0; i < validIndexes.Count; i++)
                {
                    if (index -1 == validIndexes[i] || index == Store.NumForPickup)
                    {
                        if (index != Store.NumForPickup) { return index - 1; }
                        return Store.NumForPickup;
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
