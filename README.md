# BlackJackConsoleApp
♦️♣️♥️♠️♦️♣️♥️♠️♦️♣️♥️♠️♦️♣️♥️♠️

// auto assume value of ace, depending on what wins you the game
// if you go over 21 with ace, then its value drops to 1. So starts at 11, reduces to 1 to avoid bust

// 6) Check for Blackjack 21 tie
// If both player and dealer has it, then push (i.e. tie)
// If only dealer has it, player loses (but player could hit though)
// if only player has it, player wins (but if dealer is under 17, they draw)
Because the player could draw a card and likewise the dealer, I though I'd just check for the tie on 21.

Note to self. If a variable is declared before a function, you don't have to add it to the function params. However I don't know if in this case it would be a pass by value or pass by reference!

// [rule option] if your hand is already 21 with 2 cards (e.g. ace plus jack/queen/king) then you automatically win the round (I'm wondering about the dealer end)

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