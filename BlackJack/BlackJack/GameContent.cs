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


    }
}
