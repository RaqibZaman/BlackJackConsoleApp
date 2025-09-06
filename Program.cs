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

void PrintCards(List<string> deck)
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
List<string> GenDeck()
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

List<string> ShuffleDeck(List<string> deck)
{
    //var rand = new Random();    // seed is based on system clock
    var rand = new Random(1);     // fixed randomization for testing
    deck = deck.OrderBy(x => rand.Next()).ToList();
    return deck;
}
// need to make sure change in function propagates outside func. scope. Force by using "ref" or "out" if needed. Will have to test I suppose.
void PlaceBet(ref int bank, ref string? bet, ref int parsedBet)
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



var deck = GenDeck();   // 1) Start with a standard 52 deck of cards
PrintCards(deck);     // <test> deck by printing list
deck = ShuffleDeck(deck);   // 2) I suppose next step is randomizing the deck
PrintCards(deck);     // <test> randomization or deck shuffle

int bank = 100; string? bet = ""; int parsedBet = 0;    
PlaceBet(ref bank, ref bet, ref parsedBet); // 3) place the initial bet
Console.WriteLine($"bank: {bank} bet: {bet} parsedBet: {parsedBet}");   // <test> if ref passed change over scope


// 4) deal the cards
// from deck to dealer and player hands
List<string> dealerHand = new List<string>();
List<string> playerHand = new List<string>();
// the card value calculation depends on the hands. (hand) => {calculation}

// pass card to player and deal and print them out

// remove card from top of deck
string DrawCard(ref List<string> deck)
{
    var card = deck[0];
    deck.RemoveAt(0);
    return card;
}

void InitialDeal(ref List<string> dealerHand, ref List<string> playerHand, ref List<string> deck)
{
    // toggle giving cards to player and dealer
    string recipient = "p";
    for (int i = 0; i < 4; i++) // hand out 4 cards
    {
        if (recipient == "p")
        {
            playerHand.Add(DrawCard(ref deck));
            recipient = "d";    // switch recipient
        }
        else
        {
            dealerHand.Add(DrawCard(ref deck));
            recipient = "p";    // switch recipient
        }   
    }
}

InitialDeal(ref dealerHand, ref playerHand, ref deck);

Console.WriteLine("Player Cards");
PrintCards(playerHand);
Console.WriteLine("Dealer Cards");
PrintCards(dealerHand);
Console.WriteLine("Deck Cards");
PrintCards(deck);

// auto assume value of ace, depending on what wins you the game
// if you go over 21 with ace, then its value drops to 1. So starts at 11, reduces to 1 to avoid bust
// if your hand is already 21 with 2 cards (e.g. ace plus jack/queen/king) then you automatically win the round (I'm wondering about the dealer end)

// round 1
// deal 1 faced-up card to player
// deal 1 faced-up card to dealer
// round 2
// deal 1 faced-up card to player
// deal 1 faced-DOWN card to dealer

// total higher than 21 is bust

// place bet
// dealer deals 1 card faced up to player and 1 faced up themselves
// dealer deals 2nd card faced up to player and 1 faced down for themselves
//

// you can keep asking for another card until you bust (over 21)
// finish round by "stay" to abstain


// need a reprint of total amount available

// "hit" to ask for another card, "stay" to abstain
// Dealer: After the players are done hitting, he flips his faced-down card. If it is 16 or under, he has to pick another card from the deck. If his hand is 17 or higher, the dealer has to stay with his hand of cards. If the dealer busts, then player wins twice the bet.


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