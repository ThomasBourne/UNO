using System;
using System.Collections.Generic;
using System.Text;

namespace uno
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
    public class Store
    {
        public static int NumForPickup = 20000;
        public (List<string[]>, List<string[]>) SwitchDeck(List<string[]> deck1, List<string[]> deck2) //Dosnt need a func but simplifys
        {
            return (deck2, deck1);
        }
    }
    public class Deck
    {
        //local string[]s that stor colours and possible values
        private string[] colours = { "Red", "Green", "Blue", "Yellow" };
        private string[] value = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "+2", "+4", "reverse", "miss" };
        public string[] GenerateCard() //generates a card with: [string val, string colour]
        {
            Random rand = new Random();
            string val = value[rand.Next(0, value.Length)];
            string colour = colours[rand.Next(0, colours.Length)];
            string[] card = { val, colour };
            return card;
        }
        public List<string[]> GenerateStartupDeck() //generates a list of string[]s that can be acces ad a deck
        {
            List<string[]> deck = new List<string[]>();
            for (int i = 0; i < 7; i++)
            {
                deck.Add(GenerateCard());
            }
            return deck;
        }
    }
}
