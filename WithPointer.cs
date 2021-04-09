using System;
using System.Collections.Generic;
using System.Text;

namespace PointerUno
{
    public struct Player // is the player struct and stores players deck
    /* TO DO
     * store playes name here
     * make when plays name is wanted to access it here
     */
    {
        List<string[]> deck;
        int deckLength;
        public void getValues(List<string[]> deck)
        {
            this.deck = deck;
            this.deckLength = deck.Count;
        }
        public List<string[]> Deck()
        {
            return deck;
        }
    }
    unsafe class Run
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NOTE: ONLY MISS AND REVERSE POWERCARDS WORK NOW");
            int playerCount = PlayerCount();
            int* playerCountPtr = &playerCount;
            string[] names = PlayerNames(playerCountPtr);
            Console.Clear();
            Deck deck = new Deck();
            string[] pile = new string[1];
            pile = deck.GenerateCard();

            Player player = new Player();
            player.getValues(deck.GenerateStartupDeck());

            Player[] players = new Player[*playerCountPtr];
            for (int i = 0; i < *playerCountPtr; i++)
            {
                players[i] = player;
                player.getValues(deck.GenerateStartupDeck());
            }
            Console.Clear();
            int? winnerPos = IsWinner(players);
            int?* winnerPosPtr = &winnerPos;
            while (*winnerPosPtr == null) //where the game loop starts
            {
                for (int i = 0; i < *playerCountPtr; i++)
                {
                    int* iPtr = &i;
                    if (*winnerPosPtr == null)
                    {
                        Console.WriteLine($"Press any key when {names[*iPtr]} is ready");
                        Console.ReadKey(); Console.Clear();
                        string[] cardPlayed = { "", "" };
                        ShowDeck(players[*iPtr].Deck(), names[*iPtr], pile);
                        Console.Write($"Enter The index of the card you want to play (leave blank to pickup){Environment.NewLine}>>> ");
                        string reqIndex = Console.ReadLine();
                        int intReqIndex = -1;
                        if (reqIndex == "")
                            cardPlayed = pile;

                        else if (int.TryParse(reqIndex, out intReqIndex) && intReqIndex - 1 >= 0 && intReqIndex - 1 < players[*iPtr].Deck().Count)
                            cardPlayed = players[*iPtr].Deck()[intReqIndex - 1];
                        //cardPlayed[0] = players[i].Deck()[intReqIndex - 1][0];
                        //cardPlayed[1] = players[i].Deck()[intReqIndex - 1][1];

                        while (cardPlayed[0] != pile[0] && cardPlayed[1] != pile[1])
                        {
                            Console.Write($"The card chosen is not playable, enter The index of the card you want to play (leave blank to pickup){Environment.NewLine}>>> ");
                            reqIndex = Console.ReadLine();
                            if (reqIndex == "")
                                cardPlayed = pile;
                            else if (int.TryParse(reqIndex, out intReqIndex) && intReqIndex - 1 >= 0 && intReqIndex - 1 < players[*iPtr].Deck().Count)
                                cardPlayed = players[*iPtr].Deck()[intReqIndex - 1];
                        }
                        pile = cardPlayed;
                        //if (pile[0] == "reverse") { Array.Reverse(players); Array.Reverse(names); }
                        //else if (pile[0] == "miss") i++;
                        /*else if (pile[0] == "+2" || pile[0] == "+4")
                        {
                            int tmpTransition = pile[0] == "+2" ? 2 : 4;
                            if (Store.inUseActionCards[0] == 0)
                            {
                                Store.inUseActionCards[0] = tmpTransition;
                                Store.inUseActionCards[1]++;
                            }
                            else if (Store.inUseActionCards[0] == tmpTransition) Store.inUseActionCards[1]++;
                            else
                            {
                                intReqIndex = -1;
                                for (int j = 0; j < (Store.inUseActionCards[0] * Store.inUseActionCards[1]) - 1; j++)
                                {
                                    players[i].Deck().Add(deck.GenerateCard());
                                }
                            }
                        }*/
                        if (intReqIndex > 0)
                            players[*iPtr].Deck().RemoveAt(intReqIndex - 1); //BOTH REVERSE AND MISS A GO ARE SEVERY BUGGED
                        else
                            players[*iPtr].Deck().Add(deck.GenerateCard());
                        Console.Clear();
                        *winnerPosPtr = IsWinner(players);
                    }
                }
            }
            Console.WriteLine($"{names[winnerPos.Value]} is the winner as they lost all of their cards first");
            Console.Read();
        }


        private static int PlayerCount()
        {
            Console.Write($"Welcome to dos!{Environment.NewLine}Please enter the number of players (2-10) {Environment.NewLine}>>> ");
            string strCount = Console.ReadLine();
            while (!int.TryParse(strCount, out int a) || a < 2 || a > 10)
            {
                Console.Write($"Please enter the number of players (2-10) {Environment.NewLine}>>> ");
                strCount = Console.ReadLine();
            }
            return int.Parse(strCount);
        }

        private static string[] PlayerNames(int* playerCountPtr)
        {
            string[] names = new string[*playerCountPtr];
            for (int i = 0; i < *playerCountPtr; i++)
            {
                int* iPtr = &i;
                Console.Write($"Enter the name for player {*iPtr + 1}:{Environment.NewLine}>>> ");
                names[*iPtr] = Console.ReadLine();
            }
            return names;
        }

        private static int? IsWinner(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].Deck().Count == 0)
                    return i;
            }
            return null;
        }

        public static void ShowDeck(List<string[]> deck, string name, string[] pile) //Func that Prints the players deck to the console
        {
            Console.WriteLine($"{name}'s turn:{Environment.NewLine}Cards below:");
            string output;
            for (int i = 0; i < deck.Count; i++)
            {
                int* iPtr = &i;
                Console.Write($"{*iPtr + 1}. ");
                if (deck[*iPtr][1] == "Yellow")
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (deck[*iPtr][1] == "Blue")
                    Console.ForegroundColor = ConsoleColor.Blue;
                if (deck[*iPtr][1] == "Red")
                    Console.ForegroundColor = ConsoleColor.Red;
                if (deck[*iPtr][1] == "Green")
                    Console.ForegroundColor = ConsoleColor.Green;
                output = $"{deck[*iPtr][0]}, {deck[*iPtr][1]}\n";
                Console.Write(output);
                Console.ResetColor();
            }
            Console.Write($"Card on the top of the pile is ");
            if (pile[1] == "Yellow")
                Console.ForegroundColor = ConsoleColor.DarkYellow;
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
    }



    unsafe public class Deck
    {
        //local string[]s that stor colours and possible values
        private string[] colours = { "Red", "Green", "Blue", "Yellow" };
        private string[] value = { "0", "1", "2", "3", "4", "5"/*, "6", "7", "8", "9", "10", "+2", "+4", "reverse", "miss"*/}; //commented cuz games take a while so NICK re-enable ones u need (nothing will break)
        public string[] GenerateCard() //generates a card with: [string val, string colour]
        {
            Random rand = new Random();
            string val = value[rand.Next(0, value.Length)];
            string colour = colours[rand.Next(0, colours.Length)];
            string[] card = { val, colour };
            return card;
        }
        public List<string[]> GenerateStartupDeck() //generates a list of string[]s that can be accesed as a deck
        {
            List<string[]> deck = new List<string[]>();
            for (int i = 0; i < 7; i++)
            {
                deck.Add(GenerateCard());
            }
            return deck;
        }
    }

    public class Store
    {
        public static int[] inUseActionCards = { 0, 0 };
        public static int NumForPickup = 20000;
        public (List<string[]>, List<string[]>) SwitchDeck(List<string[]> deck1, List<string[]> deck2) //Dosnt need a func but simplifys
        {
            return (deck2, deck1);
        }
    }
}
