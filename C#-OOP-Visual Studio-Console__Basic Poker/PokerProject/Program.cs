using CardLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            CardSet myDeck = new CardSet(); //make the deck of cards

            const int handSize = 5;
            int balance = 10;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("Welcome to Poker! \nYou start with $10, and each bet is $1. \nPress any key when ready...");
            Console.ReadLine();

            while (balance != 0)
            {
                Console.Clear();

                SuperCard[] computerHand = myDeck.GetCards(handSize);
                SuperCard[] playerHand = myDeck.GetCards(handSize);
                Array.Sort(computerHand);
                Array.Sort(playerHand);

                DisplayHands(computerHand, playerHand);
                if (!Flush(computerHand))   //if no flush, then change a card. otherwise, keep hand if there is
                {
                    ComputerDrawsOne(computerHand, myDeck);
                }
                PlayerDrawsOne(playerHand, myDeck);
                DisplayHands(computerHand, playerHand);

                int cScore;
                int pScore;
                bool won = CompareHands(computerHand, playerHand, out cScore, out pScore);
                Console.WriteLine("\nYour score was {0}. \nComputer score was {1}.\n", pScore, cScore);
                if (won)
                {
                    balance++;
                    Console.WriteLine("You won! Your new balance is ${0}. Press Enter to play again.", balance);
                    Console.ReadLine();
                }
                else
                {
                    balance--;
                    Console.WriteLine("You lost. Your new balance is ${0}. Press Enter to play again.", balance);
                    Console.ReadLine();
                }
                CardSet.ResetUsage();
            }
        }

        //--Looks at both hands, and decides who wins
        private static bool CompareHands(SuperCard[] computer, SuperCard[] player, out int cTotalRank, out int pTotalRank)
        {
            cTotalRank = 0;
            pTotalRank = 0;
            foreach (SuperCard item in computer)
            {
                cTotalRank += (int)item.CardRank; //+ cTotalRank;
            }
            foreach (SuperCard item in player)
            {
                pTotalRank += (int)item.CardRank; //+ pTotalRank;
            }
            if (Flush(computer))    //this will hit first, even if player has a flush too, causing the computer to always win with a flush
            {
                Console.WriteLine("Computer has a flush!");
                return false;
            }
            else if (Flush(player))
            {
                Console.WriteLine("You got a flush!");
                return true;
            }
            else if (pTotalRank > cTotalRank)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //--Display hands
        private static void DisplayHands(SuperCard[] computerHand, SuperCard[] playerHand)
        {
            Console.WriteLine("Computer's hand:");
            foreach (SuperCard item in computerHand)
            {
                item.Display();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine("Player's hand:");
            foreach (SuperCard item in playerHand)
            {
                item.Display();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        //--Let's the computer swap a card. If a card has a rank lower than 7, it gets replaced
        private static void ComputerDrawsOne(SuperCard[] computerHand, CardSet myDeck)
        {
            //get a card
            //compare rest of ranks to that card
            //replace that card
            SuperCard card = computerHand[0];
            for (int i = 0; i < computerHand.Length; i++)
            {
                if (card.CardRank > computerHand[i].CardRank)
                {
                    card = computerHand[i];
                }
                if ((int)card.CardRank < 7)
                {
                    computerHand[i] = CardSet.GetOneCard();
                    break;
                }
            }
        }

        //--Let's player swap a card
        private static void PlayerDrawsOne(SuperCard[] playerHand, CardSet myDeck)
        {
            Console.WriteLine("Would you like to replace a card?");
            int i = 1;
            foreach (SuperCard item in playerHand)
            {
                Console.WriteLine(i + " for:");
                item.Display();
                i++;
            }
            Console.WriteLine("0 for no card replacement");

            //--validation
            int userParse;
            bool b_userParse = false;
            while (!b_userParse)
            {
                string user = Console.ReadLine();
                b_userParse = int.TryParse(user, out userParse);
                if (((userParse != 0) & ((userParse < 0) || (userParse > playerHand.Length))) || !b_userParse)
                    //if the number isn't 0, is under 0, or is over the hand limit              //or it just didn't parse
                {
                    Console.WriteLine("Invalid Input");
                    b_userParse = false;    //to catch instances where the number did parse, but is out of range
                }
                else if (userParse != 0)
                    //if everything clears, but input wasn't 0
                    //0 is reserved to let the user "fall through" the loop, so no card is replaced and they may keep their hand
                {
                    playerHand[(userParse - 1)] = CardSet.GetOneCard();
                }
            }
        }

        //--Flush check
        private static bool Flush(SuperCard[] hand)
        {
                if (Array.TrueForAll<SuperCard>(hand, hand[0].Equals))
                {
                    return true;
                }
            return false;
        }
    }
}
