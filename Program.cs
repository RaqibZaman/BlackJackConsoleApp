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
    var rand = new Random();    // seed is based on system clock
    // var rand = new Random(1);     // fixed randomization for testing
    deck = deck.OrderBy(x => rand.Next()).ToList();
    return deck;
}
// need to make sure change in function propagates outside func. scope. Force by using "ref" or "out" if needed. Will have to test I suppose.
int PlaceBet(ref int bank)
{
    Console.WriteLine($"Available Cash: ${bank}");
    Console.WriteLine("Enter your bet between $10-$500 as plain integer");

    // if the user is an idot and puts in letters, empty space, value outside 10-500
    int parsedBet = 0;
    while (true)
    {
        string? bet = Console.ReadLine();
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
    //bank -= parsedBet;
    //Console.WriteLine($"Available Cash: ${bank}");
    return parsedBet;
}

// draw a card from the top of deck
string DrawCard(ref List<string> deck)
{
    var card = deck[0];
    deck.RemoveAt(0);
    return card;
}

// initial drawing- 2 for player, 2 for dealer
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

// value of each card rank
var cardValDict = new Dictionary<string, int>
{
    {"A", 11},   // subtract 10 if total is over 21
    {"2", 2},
    {"3", 3},
    {"4", 4},
    {"5", 5},
    {"6", 6},
    {"7", 7},
    {"8", 8},
    {"9", 9},
    {"1", 10},  // if I just read the value of first char, then "10" => '1' => int 10
    { "J", 10},
    {"Q", 10},
    {"K", 10}
};

// return value of total hand
int calcTotalHandVal(List<string> hand, Dictionary<string, int> cardValDict)
{
    // keep track of the number of aces and the total. ace is +10 depending on the total to 21
    int numberOfAces = 0;
    int total = 0;
    foreach (var card in hand)
    {
        if (card[0].ToString() == "A")
        {
            numberOfAces++;
        }
        total += cardValDict[card[0].ToString()];   // 1st char of card string
        //Console.WriteLine(cardValDict[card[0].ToString()]);
    }
    //Console.WriteLine($"Total: {total}");
    // if total is more than 21, check for aces. If ace exists, subtract. And so on according to the number of aces and total
    if (total > 21 && numberOfAces > 0)
    {
        for (int i = 0; i < numberOfAces; i++)
        {
            total -= 10;
            if (total <= 21)
            {
                break;
            }
        }
    }
    //Console.WriteLine($"Adjusted Total: {total}");
    return total;
}

// Hit: draw card after initial drawing
void hit(ref List<string> hand, ref List<string> deck, ref int handVal)
{
    Console.WriteLine("You drawed a card!");
    // player takes a card, update hand
    hand.Add(DrawCard(ref deck));
    PrintCards(hand);
    // calculate hand total
    handVal = calcTotalHandVal(hand, cardValDict);
    Console.WriteLine($"Total: {handVal}");
    // getRecked if bust
    //isBust(handVal);
}

// if you lose
bool isBust(int handVal)
{
    if (handVal > 21)
    {
        Console.WriteLine("Your busted!");
        return true;
    }
    return false;
}

// Double Down: Double bet, draw card, end turn
// Surrender: forfeit, dealer wins, lose half of bet
// [x] Stand: end turn
// [x] Hit: draw a card
// Split: ??? (eeh, skip for now)
bool playerTurn(ref List<string> playerHand, ref List<string> deck, ref int playerHandVal)
{
    Console.WriteLine("Enter option: [h] Hit, [s] Stand, [d] Double Down, [e] Surrender");
    while (true)
    {
        var key = Console.ReadKey(intercept: true).KeyChar;
        switch (key)
        {
            case 'h':
                hit(ref playerHand, ref deck, ref playerHandVal);
                break;
            case 's':
                return false; // exit
        }

        if (isBust(playerHandVal))
        {
            return true;
        }
        // exit player turn
        // if (key == 's')
        // {
        //     break;
        // }
    }
}

void dealerTurn(ref List<string> dealerHand, ref List<string> deck, ref int dealerHandVal)
{
    // check dealer hand value
    // if hand is less than 17, keep hitting
    while (dealerHandVal < 17)
    {
        hit(ref dealerHand, ref deck, ref dealerHandVal);
    }
}


int gameResult(ref int dealerHandVal, ref int playerHandVal, ref int bank, ref int bet)
{
    // compare player and dealers hands. Either lose, win, or push (tie)
    // dealer hand > 21 => bust
    string result = "";
    if (playerHandVal > 21)
    {
        Console.WriteLine("Player loses");
        result = "L";
    }
    else if (dealerHandVal > 21)
    {
        Console.WriteLine("Player wins");
        result = "W";
    }
    // player hand > dealer hand => player wins
    else if (playerHandVal > dealerHandVal)
    {
        Console.WriteLine("Player wins");
        result = "W";
    }
    // player hand == dealer hand => tie
    else if (playerHandVal == dealerHandVal)
    {
        Console.WriteLine("Tie - Push to next round");
        result = "T";
    }
    // player hand < dealer hand => player loses
    else if (playerHandVal < dealerHandVal)
    {
        Console.WriteLine("Player loses");
        result = "L";
    }
    else
    {
        Console.WriteLine("There's a bug");
    }

    // bank account adjustment
    switch (result)
    {
        case "W":
            bank += bet;
            break;
        case "L":
            bank -= bet;
            break;
        default:
            break;
    }
    return bank;
}

// -----------------------------------------------
// Abstract spaghetti away into functions --------
// -----------------------------------------------

int bank = 100;

// 0) Loop game until bankrupt
while (bank > 9)
{
    // 1) Start with a standard 52 deck of cards
    var deck = GenDeck();
    PrintCards(deck);
    Console.WriteLine();

    // 2) Shuffle the deck
    deck = ShuffleDeck(deck);
    PrintCards(deck);
    Console.WriteLine();

    // 3) place the initial bet   
    int bet = PlaceBet(ref bank); 
    //Console.WriteLine($"bank: {bank} bet: {bet}");   // <test> if ref passed change over scope

    // 4) deal the cards
    List<string> dealerHand = new List<string>();
    List<string> playerHand = new List<string>();
    InitialDeal(ref dealerHand, ref playerHand, ref deck);  // pass 4 cards to player and dealer from deck

    // 5) Let's calculate the value of the cards in a simple way 1st
    int dealerHandVal = calcTotalHandVal(dealerHand, cardValDict);
    int playerHandVal = calcTotalHandVal(playerHand, cardValDict);

    Console.WriteLine();
    Console.WriteLine("Player Cards");
    PrintCards(playerHand);
    Console.WriteLine($"Total: {playerHandVal}");

    Console.WriteLine();
    Console.WriteLine("Dealer Cards");

    Console.WriteLine(dealerHand[0]);   // hide dealer's 2nd card
    Console.WriteLine($"partial: {cardValDict[dealerHand[0][0].ToString()]}");  // hide total

    Console.WriteLine();
    // Console.WriteLine("Deck Cards Remaining");
    // PrintCards(deck);

    // 6). Player turns: Choose to Hit, Stand, Double Down, Split (if applicable), or Surrender (if allowed).
    bool playerBusted = false;  // track if player busts
    playerBusted = playerTurn(ref playerHand, ref deck, ref playerHandVal);

    // 7). Dealer's turn
    Console.WriteLine("Dealer's Hand");
    PrintCards(dealerHand); // dealer reveals face-down card
    Console.WriteLine($"Total: {dealerHandVal}");
    if (!playerBusted)  // skip dealer turn if player is already busted
    {
        dealerTurn(ref dealerHand, ref deck, ref dealerHandVal);
    }

    // 8). Decide result of round, update player bank account
    gameResult(ref dealerHandVal, ref playerHandVal, ref bank, ref bet);
}


// 8. Payouts are made (usually 3:2 for Blackjack, 1:1 for normal win, insurance pays 2:1).
// track if player wins/loses/ties. Use a signal variable to track
// win => bank + bet
// lose => bank - bet
// tie => next round

// 9. Loop until player becomes  bankrupt




/*
Cycle of a Blackjack round:
1.[x] Players place bets within table limits.
2.[x] Dealer shuffles (or uses a shoe if multiple decks).
3.[x] Initial deal: each player gets 2 cards; dealer gets 2 cards (1 face up, 1 face down).
4.[x -skip] Check for natural Blackjack (21 with first two cards):
    a. If dealer has it, hand ends (unless player also has it → push).
    b. If players have it and dealer doesn’t, they’re paid immediately.
5. [x -partial] Player turns (starting left of dealer):
    a. [x] Choose to Hit
    b. [x] Stand
    c. [] Double Down
    d. [] Split (if applicable)
    e. [] or Surrender (if allowed).
6. [x] Dealer’s turn: reveal hole card, draw until total is 17 or higher (rules vary for “soft 17”).
7. Compare hands:
    a. [x] If player busts → automatic loss.
    b. [x] If dealer busts → remaining players win.
    c. [x] Otherwise, higher total ≤21 wins; equal totals = push (tie).
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
Console.WriteLine(getRecked);