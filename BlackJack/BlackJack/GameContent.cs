using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;

namespace BlackJack
{
    public static class GameConstants
    {
        public const int CARDS_IN_DECK = 52;
        public const int STARTING_CARDS = 4;
        public const int CARD_DRAW_DELAY = 250;
        public const int POPUP_DELAY = 2500;
        public const int TIMEOUT_DELAY = 350;
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
        
    }
        public partial class Card
        {
            private int cardVal;
            private string imageSource;

            public int CardVal { get => cardVal; set => cardVal = value; }
            public string ImageSource { get => imageSource; set => imageSource = value; }
        }
        public partial class Deck
        {
            private Card[] cards;
            private int totalCards;
            
            public Deck(int deckCount, int startAmount)
            {
                cards = new Card[deckCount * GameConstants.CARDS_IN_DECK];
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
        public static int losses = 0;
        public static int wins = 0;
        public static int pushes = 0;
        public static int busts = 0;
        private int deckCount;
            private int totCardCount;
            private Deck playDeck;
            private Deck usedDeck;
            private theHand dealHand;
            private theHand playerHand;
            private Random random = new Random();
            

            public GameOperations(int deckCount)
            {
                totCardCount = deckCount * GameConstants.CARDS_IN_DECK;
                Setup();
            }

            private void Setup()
            {
                setDecks();

                dealHand = new theHand();
                playerHand = new theHand();
            }

            public async Task Stand(FlexLayout[] views, Label[] totals, StackLayout[] notifications, Button[] buttons)
            {
                buttons[GameConstants.HIT].IsEnabled = false;
                buttons[GameConstants.STAND].IsEnabled = false;
                await Task.Delay(GameConstants.TIMEOUT_DELAY);
                ShowCard(views[GameConstants.DEALER], totals[GameConstants.DEALER]);


                while (dealHand.HandVal < 17)
                {
                    await Task.Delay(GameConstants.CARD_DRAW_DELAY);
                    if (playDeck.TotalCards == 0)
                    {
                        RecombineDeck();
                    }
                        DrawCard(views[GameConstants.DEALER],  dealHand,  false);
                        totals[GameConstants.DEALER].Text = dealHand.HandVal.ToString();
                    
                    
                }
                await Task.Delay(GameConstants.TIMEOUT_DELAY);

                if (dealHand.HandVal > 21)
                {
                    await ShowNotif(notifications, GameConstants.WIN_NOT);
                    wins++;
                    totals[GameConstants.WIN].Text = wins.ToString();
                } else if (dealHand.HandVal == playerHand.HandVal)
                {
                    await ShowNotif(notifications, GameConstants.PUSH_NOT);
                    pushes++;
                    totals[GameConstants.PUSH].Text = pushes.ToString();
                } else if (dealHand.HandVal < playerHand.HandVal)
                {
                    await ShowNotif(notifications, GameConstants.WIN_NOT);
                    wins++;
                    totals[GameConstants.WIN].Text = wins.ToString();
                } else 
                {
                    await ShowNotif(notifications, GameConstants.LOSE_NOT);
                    losses++;
                    totals[GameConstants.LOSE].Text = losses.ToString();

                }
                PlayAgain(buttons);
            }

            public async Task Hit(FlexLayout[] views, Label[] totals, StackLayout[] notifications, Button[] buttons)
            {
                if (playDeck.TotalCards == 0)
                {
                    RecombineDeck();
                }

                DrawCard(views[GameConstants.PLAYER], playerHand,  false);
                totals[GameConstants.PLAYER].Text = playerHand.HandVal.ToString();

                if (playerHand.HandVal > 21)
                {
                    buttons[GameConstants.HIT].IsEnabled = false;
                    buttons[GameConstants.STAND].IsEnabled = false;
                    await ShowNotif(notifications, GameConstants.BUST_NOT);
                    losses++;
                    busts++;
                    totals[GameConstants.LOSE].Text = losses.ToString();

                    ShowCard(views[GameConstants.PLAYER], totals[GameConstants.DEALER]);

                    PlayAgain(buttons);
                }

            }

            private void setDecks()
            {
                playDeck = new Deck(deckCount, totCardCount);
                for (int i = 0; i < totCardCount; i++)
                {
                    playDeck.Cards[i] = new Card();
                    playDeck.Cards[i].CardVal = GameConstants.CARD_VALS[i % 13];
                    playDeck.Cards[i].ImageSource = "/Cards/card" + ((i % 52) + 1) + ".png";
                }
                usedDeck = new Deck(deckCount, 0);
                Shuffle(playDeck);
            }

            

            private void CalcHandVal(theHand hand)
            {
                var newHand = new theHand();

                int numOfAces = 0;
                int val = 0;

                foreach (Card card in newHand.Cards)
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

           

            private void Shuffle(Deck deck)
            {
                Card[] cards = deck.Cards;
                int n = deck.Cards.Length;

                for (int i =0; i < (n - 1); i++)
                {
                    int j = i + random.Next(n - i);
                    Card temp = cards[j];
                    cards[j] = cards[i];
                    cards[i] = temp;
                }
            }

            private void RecombineDeck()
            {
                while (usedDeck.TotalCards > 0)
                {
                    playDeck.Cards[playDeck.TotalCards] = usedDeck.Cards[usedDeck.TotalCards - 1];
                    usedDeck.Cards[usedDeck.TotalCards - 1] = null;
                    usedDeck.TotalCards--;
                    playDeck.TotalCards++;
                }
                Shuffle(playDeck);
            }

            private void DrawCard(FlexLayout views, theHand hand, Boolean hide)
            {

                hand.Cards.AddLast(playDeck.Cards[playDeck.TotalCards - 1]);
                playDeck.Cards[playDeck.TotalCards - 1] = null;
                playDeck.TotalCards--;

                CalcHandVal(hand);

                 Image img = new Image();

                if (hide)
                {
                    img.Source = "/Cards/Back Side.jpg";
                } else
                {
                    img.Source = hand.Cards.Last.Value.ImageSource;
                }
                views.Children.Add(img);

            }

            public async Task Play(FlexLayout[] views, Label[] totals, StackLayout[] notifications, Button[] buttons)
            {
                if (playerHand.Cards.Count > 0 || dealHand.Cards.Count > 0)
                {
                    CleanHands();
                    totals[GameConstants.DEALER].Text = "0";
                    totals[GameConstants.PLAYER].Text = "0";
                    views[GameConstants.PLAYER].Children.RemoveAt(0);
                    views[GameConstants.DEALER].Children.RemoveAt(0);


                }

                for (int i = 0; i < GameConstants.STARTING_CARDS; i++)
                {
                    await Task.Delay(GameConstants.CARD_DRAW_DELAY);
                    if (playDeck.TotalCards == 0)
                    {
                        RecombineDeck();
                    }

                    if (i == GameConstants.STARTING_CARDS - 1)
                    {
                        DrawCard(views[GameConstants.DEALER], dealHand, true);
                        continue;
                    }

                    if (i % 2 == 0)
                    {
                        DrawCard(views[GameConstants.PLAYER], playerHand, false);
                        totals[GameConstants.PLAYER].Text = playerHand.HandVal.ToString();
                    } else
                    {
                        DrawCard(views[GameConstants.DEALER], dealHand, false);
                        totals[GameConstants.DEALER].Text = dealHand.HandVal.ToString();
                    }
                }

                //blackjack logic
                if (dealHand.HandVal == 21 || playerHand.HandVal == 21)
                {
                    if (dealHand.HandVal == 21 && playerHand.HandVal == 21)
                    {
                        await Task.Delay(GameConstants.TIMEOUT_DELAY);
                        ShowCard(views[GameConstants.DEALER], totals[GameConstants.DEALER]);
                        await ShowNotif(notifications, GameConstants.DEALER_BLACKJACK_NOT);
                        totals[GameConstants.PUSH].Text = pushes.ToString();
                        pushes++;
                        
                    }

                    else if (dealHand.HandVal == 21 && playerHand.HandVal != 21)
                    {
                        await Task.Delay(GameConstants.TIMEOUT_DELAY);
                        ShowCard(views[GameConstants.DEALER], totals[GameConstants.DEALER]);
                        await ShowNotif(notifications, GameConstants.DEALER_BLACKJACK_NOT);
                        losses++;
                        totals[GameConstants.LOSE].Text = losses.ToString();
                    } else if (dealHand.HandVal != 21 && playerHand.HandVal == 21)
                    {
                        await ShowNotif(notifications, GameConstants.BLACKJACK_NOT);
                        ShowCard(views[GameConstants.DEALER], totals[GameConstants.DEALER]);
                        wins++;
                        totals[GameConstants.WIN].Text = wins.ToString();
                    }
                    PlayAgain(buttons);
                }
                else
                {
                    buttons[GameConstants.HIT].IsEnabled = true;
                    buttons[GameConstants.STAND].IsEnabled = true;
                }
            }

            private void CleanHands()
            {
                while (playerHand.Cards.Count > 0)
                {
                    usedDeck.Cards[usedDeck.TotalCards] = playerHand.Cards.Last.Value;
                    usedDeck.TotalCards++;
                    playerHand.Cards.RemoveLast();
                }

                while (dealHand.Cards.Count > 0)
                {
                    usedDeck.Cards[usedDeck.TotalCards] = dealHand.Cards.Last.Value;
                    usedDeck.TotalCards++;
                    dealHand.Cards.RemoveLast();
                }
            }

            private void ShowCard(FlexLayout views, Label dealerTot)
            {
                views.Children.RemoveAt(views.Children.Count - 1);
                Image img = new Image();
                img.Source = dealHand.Cards.Last.Value.ImageSource;
                img.Margin = new Thickness(5);
                views.Children.Add(img);
                dealerTot.Text = dealHand.HandVal.ToString();
            }

            private async Task ShowNotif(StackLayout[] notifications, int n)
            {
                notifications[n].IsVisible = true;
                await Task.Delay(GameConstants.POPUP_DELAY);
                notifications[n].IsVisible = false;
            }

            private void PlayAgain(Button[] buttons)
            {
                buttons[GameConstants.PLAY].Text = "Play again?";
                buttons[GameConstants.PLAY].IsEnabled = true;
            }

        }
    }

