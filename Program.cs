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
// NOTE TO REVIEWRS: I don't play cards!
// MOTO: keep abstracting until it makes sense.
//// Functions ////

void toStringDeck(List<string> deck)
{
    foreach (var card in deck)
    {
        Console.Write(card);
        Console.Write("   ");
    }
    Console.WriteLine();
    Console.WriteLine();
}

// Big O inefficient but I prefer generation :D
// generate 4 suits of Aces, 2-10, Jake, Queen, King.
// 4 suits
List<string> genDeck()
{
    var deck = new List<string>();
    var suits = new List<string> { "♦️", "♣️", "♥️", "♠️" };
    var ranks = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" }; // no jocker!
    for (int i = 0; i < suits.Count; i++)
    {
        for (int j = 0; j < ranks.Count; j++)
        {
            deck.Add(ranks[j] + suits[i]);
        }
    }
    return deck;
}

List<string> shuffleDeck(List<string> deck)
{
    var rand = new Random();    // seed is based on system clock
    deck = deck.OrderBy(x => rand.Next()).ToList();
    return deck;
}
// need to make sure change in function propagates outside func. scope. Force by using "ref" or "out" if needed. Will have to test I suppose.
void placeBet(ref int bank, ref string? bet, ref int parsedBet)
{
    Console.WriteLine($"Available Cash: ${bank}");
    Console.WriteLine("Enter your bet between $10-$500 as plain integer");

    // if the user is an idot and puts in letters, empty space, value outside 10-500
    while (true)
    {
        bet = Console.ReadLine();
        int.TryParse(bet, out parsedBet);
        if (int.TryParse(bet, out parsedBet) && parsedBet > 9 && parsedBet < 501 && parsedBet <= bank)
        {
            break;
        }
        if (parsedBet > bank)
        {
            Console.WriteLine("You do not have enough money...");
            Console.WriteLine($"Available Cash: ${bank}");
        }
        else
        {
            Console.WriteLine("Dude, bet integer between $10-$500...");
        }
        
    }
    Console.WriteLine($"You placed a ${parsedBet} bet");
    // update bank account
    bank -= parsedBet;
    Console.WriteLine($"Available Cash: ${bank}");
}

// --- ew code! Let's abstract away into functions



var deck = genDeck();   // 1) Start with a standard 52 deck of cards
toStringDeck(deck);     // <test> deck by printing list
deck = shuffleDeck(deck);   // 2) I suppose next step is randomizing the deck
toStringDeck(deck);     // <test> randomization or deck shuffle

// 3) then the initial bet
// bet between $10 to $500
int bank = 100;
string? bet = "";
int parsedBet = 0;
placeBet(ref bank, ref bet, ref parsedBet);
Console.WriteLine($"bank: {bank} bet: {bet} parsedBet: {parsedBet}");   // test if ref passed change over scope


// then maybe dealing the cards?
// deal 1 card to player and then 1 card to dealer

// total higher than 21 is bust

// place bet
// dealer deals 1 card faced up to player and 1 faced up themselves
// dealer deals 2nd card faced up to player and 1 faced down for themselves
//

// need a reprint of total amount available




/*
Cycle of a Blackjack round:
1.[x] Players place bets within table limits.
2.[x] Dealer shuffles (or uses a shoe if multiple decks).
3. Initial deal: each player gets 2 cards; dealer gets 2 cards (1 face up, 1 face down).
4. Check for natural Blackjack (21 with first two cards):
    a. If dealer has it, hand ends (unless player also has it → push).
    b. If players have it and dealer doesn’t, they’re paid immediately.
5. Player turns (starting left of dealer):
    a. Choose to Hit, Stand, Double Down, Split (if applicable), or Surrender (if allowed).
6. Dealer’s turn: reveal hole card, draw until total is 17 or higher (rules vary for “soft 17”).
7. Compare hands:
    a. If player busts → automatic loss.
    b. If dealer busts → remaining players win.
    c. Otherwise, higher total ≤21 wins; equal totals = push (tie).
8. Payouts are made (usually 3:2 for Blackjack, 1:1 for normal win, insurance pays 2:1).

*/


string getRecked = @"⠀⠀⠀⣿⣿⣷⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⢀⣿⣿⣿⣿⣿⣿⣆⡀⠀⠀⠀⠀⣠⣴⣦⡄⢤⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣷⣷⣶⣶⣿⣿⣿⣿⡀⣽⡿⣶⣦⡀⠀⠀⠀⠀⠀
⠀⠀⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡿⣿⣿⣿⣿⣆⠀⠀⠀⠀
⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣿⣿⣿⣿⣿⣦⠀⠀⠀
⠀⠀⢾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣟⣿⣿⣿⣿⣿⡿⢟⣿⣷⡀⠀
⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣭⣿⣿⣽⣿⣽⣾⣿⣿⣿⠛⠉⠉⠀⢈⣿⣿⡇⠀
⠀⠀⠀⢻⣿⣿⠛⠉⠛⠻⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⠡⠤⠄⠁⠀⠀⢻⣿⡇⠀
⠀⠀⠀⠘⣿⣿⠄⠀⠀⠀⠀⠀⣉⠙⠋⢿⣿⣯⠀⠀⠀⠀⠀⠀⣰⣿⣿⡿⡃⠀
⠀⠀⠀⠀⢹⣿⣇⣀⠀⠈⠉⠉⠁⠀⣤⢠⣿⣿⣧⡆⣤⣤⡀⣾⣿⣿⣿⢠⡇⠀
⠀⠀⠀⠀⠀⣿⣿⣿⣷⣤⠄⣀⣴⣧⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢸⠇⠀
⠀⠀⠀⠀⠀⠸⣿⣯⠉⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⡯⠁⡌⠀⠀
⠀⠀⠀⠀⠀⠀⠙⢿⡄⢿⣿⣿⣿⣿⣿⣎⠙⠻⠛⣁⣼⣿⣿⡿⠛⠁⡸⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠈⢿⡄⠉⣿⡿⣿⣿⣿⣿⣷⣬⣿⡿⠟⠋⢀⣴⡞⠁⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠈⢳⠀⠀⠀⠀⠉⠉⠋⠉⠉⠁⠀⢀⣴⣿⡿⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣿⣿⣿⠿⢃⣴⣿⣿⣿⠃⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣿⣿⣿⣿⣿⣿⠟⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠛⠛⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀";
// Console.WriteLine(getRecked);