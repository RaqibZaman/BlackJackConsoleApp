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