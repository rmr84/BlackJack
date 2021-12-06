using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public static class GameContent
    {
        public const int CARDS_IN_DECK = 52;
        public const int STARTING_CARDS = 4;
        public const int CARD_DRAW_DELAY = 250;
        public const int POPUP_DELAY = 2500;
        public const int DEALER = 0;
        public const int PLAYER = 1;
        public const int WIN = 2;
        public const int LOSE = 3;
        public const int PUSH = 4;
        public const int PLAY = 0;
        public const int BACK = 1;
        public const int HIT = 2;
        public const int STAND = 3;
        public const int DEALERS_TURN = 0;
        public const int PUSH_NOT = 1;
        public const int BUST_NOT = 2;
        public const int LOSE_NOT = 3;
        public const int DEALER_BLACKJACK_NOT = 4;
        public const int WIN_NOT = 5;
        public const int BLACKJACK_NOT = 6;
        public static readonly int[] CARD_VALS = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };

        public partial class Card
        {
            private int cardVal;
            private string imageSource;

            public int CardVal { get => cardVal; set => cardVal = value; }
            public string ImgSource { get => imageSource; set => imageSource = value; }
        }
        public partial class Deck
        {
            private Card[] cards;
            private int totalCards;
            
            public Deck(int deckCount, int startAmount)
            {
                cards = new Card[deckCount * GameContent.CARDS_IN_DECK];
                totalCards = startAmount;
            }
            public Card[] Cards { get => cards; set => cards = value; }
            public int TotalCards { get => totalCards; set => totalCards = value; }

        }

        public partial class theHand
        {
            private LinkedList<Card> cards = new LinkedList<Card>();
            private int handVal = 0;

            public LinkedList<Card> Cards { get => cards; set => cards = value; }
            public int HandVal { get => handVal; set => handVal = value; }
        }

        public partial class GameOperations
        {
            private int deckCount;
            private int totCardCount;
            private Deck playDeck;
            private Deck usedDeck;
            private theHand dealHand;
            private theHand playerHand;
            private Random random = new Random();

            public GameOperations(int deckCount)
            {
                this.deckCount = deckCount;
                totCardCount = deckCount * GameContent.CARDS_IN_DECK;
                Setup();
            }

            private void Setup()
            {
                setDecks();

                dealHand = new theHand();
                playerHand = new theHand();
            }

            private void setDecks()
            {
                playDeck = new Deck(deckCount, totCardCount);
                for (int i = 0; i < totCardCount; i++)
                {
                    playDeck.Cards[i] = new Card();
                    playDeck.Cards[i].CardVal = GameContent.CARD_VALS[i % 13];
                  //  playDeck.Cards[i].ImageSource = need to add card pngs here
                }
                usedDeck = new Deck(deckCount, 0);
                Shuffle(playDeck);
            }

            private void CalcHandVal(theHand hand)
            {
                int numOfAces = 0;
                int val = 0;

                foreach (Card card in theHand.Cards)
                {
                    if (card.CardVal == 11)
                    {
                        numOfAces++;

                    }
                    else
                    {
                        val += card.CardVal;
                    }


                    if (val > 21 && numOfAces > 0)
                    {
                        numOfAces--;
                        val -= 10;
                    }
                }
                hand.HandVal = val;
            }

            private void CreateDecks()
            {
                playDeck = new Deck(deckCount, totCardCount);
                for (int i = 0; i < totCardCount; i++)
                {
                    playDeck.Cards[i] = new Card();
                    playDeck.Cards[i].CardVal = GameContent.CARD_VALS[i % 13];
                    // card images go here

                }
                usedDeck = new Deck(deckCount, 0);
                Shuffle(playDeck);

            }


            private void Shuffle(Deck deck)
            {
                Card[] cards = deck.Cards;
                int n = deck.Cards.Length;

                for (int i =0; i < (n - 1); i++)
                {
                    int r = i + random.Next(n - i);
                    Card temp = cards[r];
                    cards[r] = cards[i];
                    cards[i] = temp;
                }
            }

        }
    }
}
