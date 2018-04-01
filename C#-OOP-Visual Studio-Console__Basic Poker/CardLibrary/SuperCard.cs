using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public enum Suit
    {
        Club = 1,
        Diamond,
        Heart,
        Spade
    }

    public enum Rank
    {
        Deuce = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

    public abstract class SuperCard : IComparable<SuperCard>, IEquatable<SuperCard>
    {
        public Rank CardRank { get; set; }

        public abstract Suit CardSuit { get; }

        public bool InPlay { get; set; }

        public abstract void Display();

        public int CompareTo(SuperCard other)
        {
            if (this.CardSuit == other.CardSuit)
            {
                return this.CardRank.CompareTo(other.CardRank);
            }
            return other.CardSuit.CompareTo(this.CardSuit);
        }

        public bool Equals(SuperCard other)
        {
            if (this.CardSuit == other.CardSuit)
            {
                return true;
            }
            return false;
        }
    }

     public class CardClub : SuperCard
     {
        //---properties--\\
        private Suit _CardSuit = Suit.Club;
        public override Suit CardSuit
        {
            get { return _CardSuit; }
        }

        //---constructor---\\
        public CardClub (Rank rank)
        {
            CardRank = rank;
        }

        public override void Display()
        {
            // Code to Display a club card...    
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(CardRank + " of " + CardSuit + "s ♣");
            Console.ResetColor();
        }

     }

    public class CardDiamond : SuperCard
    {
        //---properties---\\
        private Suit _CardSuit = Suit.Diamond;
        public override Suit CardSuit
        {
            get{ return _CardSuit; }
        }

        //---constructor---\\
        public CardDiamond (Rank rank)
        {
            CardRank = rank;
        }

        public override void Display()
        {
            // Code to Display a diamond card...    
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(CardRank + " of " + CardSuit + "s ♦");
            Console.ResetColor();}
        }

    public class CardHeart : SuperCard
    {
        //---properties---\\
        private Suit _CardSuit = Suit.Heart;
        public override Suit CardSuit
        {
            get { return _CardSuit; }
        }

        //---constructor---\\
        public CardHeart (Rank rank)
        {
            CardRank = rank;
        }
        public override void Display()
        {
            // Code to Display a heart card...    
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CardRank + " of " + CardSuit + "s ♥");
            Console.ResetColor();}
        }

    public class CardSpade : SuperCard
    {
        //---prop---\\
        private Suit _CardSuit = Suit.Spade;
        public override Suit CardSuit
        {
            get { return _CardSuit; }
        }

        //---constr---\\
        public CardSpade(Rank rank)
        {
            CardRank = rank;
        }
        public override void Display()
        {
            // Code to Display a Spade card...    
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(CardRank + " of " + CardSuit + "s ♠");
            Console.ResetColor();
        }

    }

    public class CardSet
    {
        //---properties---\\
        static private SuperCard[] cardArray = new SuperCard[52];
        static Random myRandom = new Random();


        //creates deck
        public CardSet()   //creates deck of cards
        {
            int i = 0; //scan through cardArray[]
            int j = 2; //rank casting surrogate
            while (i < cardArray.Length)
            {
                Rank tmp = (Rank)j; //casting Rank enum

                //clubs
                if (i < 13)
                {
                    cardArray[i] = new CardClub(tmp);
                }

                //diamonds
                if (12 < i && i < 26)
                {
                    cardArray[i] = new CardDiamond(tmp);
                }

                //hearts
                if (25 < i && i < 39)
                {
                    cardArray[i] = new CardHeart(tmp);
                }

                //spades
                if (38 < i && i < cardArray.Length)
                {
                    cardArray[i] = new CardSpade(tmp);
                }

                i++;

                //checks our "rank" enum surrogate to make sure its not out of bounds with underlying ints
                if (j == 14) //if j has reached ace
                {
                    j = 2; //back to deuce
                }
                else
                {
                    j++;
                }
            }

        }


        //creates a hand
        public SuperCard[] GetCards(int pHandSize)
        {
            //make an array of handsize
            //populate array with random cards
            //return array

            SuperCard[] tmp = new SuperCard[pHandSize];
            //List<SuperCard> tmp = new List<SuperCard>();
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = cardArray[myRandom.Next(1, cardArray.Length)]; //put a card
                if (!tmp[i].InPlay)
                {
                    tmp[i].InPlay = true;   //marks the card in play
                }
                else
                {
                    i--;    //decreases i, to find a new card to overwrite saved card in tmp[i], or rather "taking it out".
                }
            }
            return tmp;
        }

        public static SuperCard GetOneCard()
        {
            SuperCard card = cardArray[myRandom.Next(1, cardArray.Length)];
            if (!card.InPlay)
            {
                card.InPlay = true;
            }
            else
            {
                GetOneCard();
            }

            return card;
        }

        //resets cards to be "reshuffled"
        static public void ResetUsage()
        {
            foreach (SuperCard item in cardArray)
            {
                item.InPlay = false;
            }

            //foreach (SuperCard item in hand2)
            //{
            //    item.InPlay = false;
            //}
        }
    }   
}
