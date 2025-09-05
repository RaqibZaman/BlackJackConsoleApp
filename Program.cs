// Imports or something
using System.Collections.Generic;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World! ♣️ ♦️ ♥️ ♠️");

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

// Well, we want to start with a deck
List<string> deck = new List<string>();

// Big O inefficient but I prefer generation :D
// generate 4 suits of Aces, 2-10, Jake, Queen, King.
var suits = new List<string> { "♦️", "♣️", "♥️", "♠️" };
for (int i = 0; i < 4; i++)
{
    Console.WriteLine(suits[i]);
}