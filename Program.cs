// Imports or something
using System.Collections.Generic;

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World! ♣️ ♦️ ♥️ ♠️");

/* To Do
----------
!!! I want to get a basic game setup before I start pushing to git and cleaning up the comments/files.
* This is a card game, so i guess I need 52 cards?
* Something about 21
* House is dealer
* Let's assume 52 card pack instead of 6 decks (312) for simplicity
* Objective: beat dealer by getting count close to 21 without going over 21
*** Deck Considerations ***
* 4 suits: Diamond ♦️, Clubs ♣️, Hearts ♥️, Spades ♠️
* Ace (A) can be worth 1 or 11 according to player 
* two to 10 numbered cards
* Face cards (jack 🎃, queen 👠, king 👑) (J,Q,K)
* Jokers 🤹🎲 (JR) (for now XD) are excluded
*** 
* Before deal, player places bets from $2 to $500 dollars
* After bet is placed, dealer gives 1 card faced up to each player. Just 1 for now. And 1 card faced up to themselves
* Another round of cards is dealt faced up to each player, but dealer takes the 2nd card faced down
* 
* 
*/



// Big O inefficient but I prefer generation :D
// generate 4 suits of Aces, 2-10, Jake, Queen, King.
// 4 suits

// Well, we want to start with a deck
var deck = new List<string>();
var suits = new List<string> { "♦️", "♣️", "♥️", "♠️" };
var ranks = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" }; // no jocker!

for (int i = 0; i < suits.Count; i++)
{
    for (int j = 0; j < ranks.Count; j++)
    {
        // Console.Write(ranks[j] + suits[i]);
        // Console.Write("   "); // emojis need space
        deck.Add(ranks[j] + suits[i]);
    }
}

// test deck by printing list
foreach (var card in deck)
{
    Console.Write(card);
    Console.Write("   ");
}

// I suppose next step is randomizing the deck

// then maybe betting?

// then maybe dealing?