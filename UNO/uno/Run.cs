using System;
using System.Linq;
using System.Collections.Generic;

namespace uno
{
    class Run
    {
        static void Main(string[] args)
        {
            //creating the class objects
            Deck deck = new Deck();
            Logic logic = new Logic();
            Store store = new Store();

            //gets the number of players
            int numPlayers = NumPlayers();
            Console.WriteLine(numPlayers);

            //gives each player a name
            string[] names = logic.CollectNames(numPlayers);

            //just to let them hand over the device to start players 0's turn
            System.Threading.Thread.Sleep(2000);
            Console.Clear();

            //creates the starting played pile
            string[] pile = new string[1];
            pile = deck.GenerateCard();


            //generates player objects and their randomly generated cards
            Player player1 = new Player();
            player1.getValues(deck.GenerateStartupDeck());
            Player player2 = new Player();
            player2.getValues(deck.GenerateStartupDeck());
            Player player3 = new Player();
            player3.getValues(deck.GenerateStartupDeck());
            Player player4 = new Player();
            player4.getValues(deck.GenerateStartupDeck());
            Player player5 = new Player();
            player5.getValues(deck.GenerateStartupDeck());
            Player player6 = new Player();
            player6.getValues(deck.GenerateStartupDeck());
            Player player7 = new Player();
            player7.getValues(deck.GenerateStartupDeck());

            //puts all of the players into a list
            List<Player> players = new List<Player>();

            //the players are added to the players here (could have used an array but List is easier to modify)
            for (int i = 0; i < numPlayers; i++)
            {
                if (i == 0)
                {
                    players.Add(player1);
                    players.Add(player2);
                }
                if (i == 2)
                    players.Add(player3);
                if (i == 3)
                    players.Add(player4);
                if (i == 4)
                    players.Add(player5);
                if (i == 5)
                    players.Add(player6);
                if (i == 6)
                    players.Add(player7);

            }
            //where the game played
            /* To Fix/Add
             * what happens when a player reaches 0 cards
             * any card can be chosen
             * preformance fixes/optimisation
             */
            while (true)
            {
                for (int i = 0; i < numPlayers; i++)
                {
                    logic.ShowDeck(players[i].Deck(), names[i], pile);
                    List<string[]> tmp = new List<string[]>();
                    (pile, tmp) = logic.Play(pile, players[i].Deck());
                    players[i].getValues(tmp);
                    Console.Clear();
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }
        public static int NumPlayers() //Func that gets number of players
        {
            //generate variables to be used
            int numPlayers;
            string strNumPlayers;
            //gets input
            Console.Write("Enter the number of players (2-7)\n>>> ");
            strNumPlayers = Console.ReadLine();
            //checks if it is numeric AND in the range of 2 to 7
            while (strNumPlayers.Any(c => c < '2' || c > '7'))
            {
                Console.Write("Please enter a number between 2 and 7\n>>> ");
                strNumPlayers = Console.ReadLine();
                Console.Clear();
            }
            //converts to int and returns
            numPlayers = Convert.ToInt32(strNumPlayers);
            return numPlayers;
        }
    }
}