using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;



namespace BlackJack
{
    

    public class Constants
    {
        

        public static readonly int[] CARDS = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        public const int CARD_DECK = 52;
        public const int STARTING_CARDS = 4;
        public const int DRAW_DELAY = 250;
        public const int POPUP_DELAY = 4000;
        public const int TIMEOUT_DELAY = 300;
        public const int DEALER = 0;
        public const int PLAYER = 1;
        
        public const int PLAY_BUTTON = 0;
        public const int BACK_BUTTON = 1;
        public const int HIT_BUTTON = 2;
        public const int STAND_BUTTON = 3;
        public const int PLAYAGAIN_BUTTON = 4;
        
        
        

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

            public Deck(int startingAmount)
            {

                cards = new Card[Constants.CARD_DECK];
                totalCards = startingAmount;
            }

            public Card[] Cards { get => cards; set => cards = value; }
            public int TotalCards { get => totalCards; set => totalCards = value; }
        }

        


        public class Hand
        {
            private LinkedList<Card> cards = new LinkedList<Card>();
            private int handValue = 0;

            public LinkedList<Card> Cards { get => cards; set => cards = value; }
            public int HandValue { get => handValue; set => handValue = value; }

            
        }

        public partial class Game

        {
            private int overallCardCount;
            private Deck playDeck;
            private Deck usedDeck;
            private Hand dealerHand;
            private Hand playerHand;
            public int winOnStand = 0;
            public int loseOnStand = 0;
            public int winOnHit = 0;
            
            public int wins = 0;
            public int losses = 0;
            public int winonbj = 0;
            public int busts = 0;
            public int pushes = 0;
        public int handsplayed = 0;

            private Random random = new Random();
        private int index;
        private Manager manager = Manager.GetInstance();
        CardModel c = new CardModel();


            public Game()
            {
                overallCardCount = Constants.CARD_DECK;
                Setup();
            c.wins = wins;
            c.pushes = pushes;
            c.losses = losses;
            c.busts = busts;
            c.winOnHit = winOnHit;
            c.winOnStand = winOnStand;
            c.loseOnStand = loseOnStand;
            c.blackjacks = winonbj;
            c.handsPlayed = handsplayed;
            }


            private void Setup()
            {
                CreateDecks();

                dealerHand = new Hand();
                playerHand = new Hand();
            }

        public async Task Play(FlexLayout[] views, Label[] totals, Button[] buttons)
        {
            // Clean up last round.
            if (playerHand.Cards.Count > 0 || dealerHand.Cards.Count > 0)
            {

                DiscardHands();
                views[Constants.DEALER].Children.Clear();
                views[Constants.DEALER].Children.Clear();
                totals[Constants.DEALER].Text = "0";
                totals[Constants.PLAYER].Text = "0";
                
            }

            // Draw cards.
            for (int i = 0; i < Constants.STARTING_CARDS; i++)
            {
                await Task.Delay(Constants.DRAW_DELAY);

                if (playDeck.TotalCards == 0)
                    RecombineDecks();

                // Last draw. Hides last dealer card.
                if (i == Constants.STARTING_CARDS - 1)
                {
                    DrawCard(dealerHand, views[Constants.DEALER], true);
                    continue;
                }

                // Flop between drawing a player and dealer card.
                if (i % 2 == 0)
                {
                    DrawCard(playerHand, views[Constants.PLAYER], false);
                    totals[Constants.PLAYER].Text = playerHand.HandValue.ToString();
                }
                else
                {
                    DrawCard(dealerHand, views[Constants.DEALER], false);
                    totals[Constants.DEALER].Text = dealerHand.HandValue.ToString();
                }
            }

            if (dealerHand.HandValue == 21 || playerHand.HandValue == 21)
                {

                    if (dealerHand.HandValue == 21 && playerHand.HandValue == 21)
                    {
                        Console.WriteLine("this is a blackjack push" + "\n");
                        await Task.Delay(Constants.TIMEOUT_DELAY);
                        ShowCard(views[Constants.DEALER], totals[Constants.DEALER]);
                    await App.Current.MainPage.DisplayAlert("Push", "Hands are equal.", "OK");
                   // GameOver(buttons);
                    pushes++;
                    manager.GetObsList();
                    //put


                }

                    else if (dealerHand.HandValue == 21 && playerHand.HandValue != 21)
                    {
                        await Task.Delay(Constants.TIMEOUT_DELAY);
                        ShowCard(views[Constants.DEALER], totals[Constants.DEALER]);
                        buttons[1].IsEnabled = false;
                        buttons[2].IsEnabled = false;
                    await App.Current.MainPage.DisplayAlert("Dealer Blackjack!", "You Lose!", "OK");
                    
                    losses++;
                    manager.GetObsList();
                    //put


                }

                    else if (dealerHand.HandValue != 21 && playerHand.HandValue == 21)
                    {
                    
                    
                    ShowCard(views[Constants.DEALER], totals[Constants.DEALER]);
                    buttons[1].IsEnabled = false;
                    buttons[2].IsEnabled = false; //<-------------- Still needs fixed
                    await App.Current.MainPage.DisplayAlert("Blackjack!", "You Win!", "OK");
                   
                    wins++;
                    winonbj++;
                    manager.GetObsList();
                    //put



                }

                    PlayAgain(buttons);
                }

                
            }


            public async Task Stand(FlexLayout[] views, Label[] totals, Button[] buttons)
            {
                
                await Task.Delay(Constants.TIMEOUT_DELAY);

                ShowCard(views[Constants.DEALER], totals[Constants.DEALER]);


                while (dealerHand.HandValue < 17)
                {
                    await Task.Delay(Constants.DRAW_DELAY);

                    if (playDeck.TotalCards == 0)
                        RecombineDecks();


                    DrawCard(dealerHand, views[Constants.DEALER], false);
                    totals[Constants.DEALER].Text = dealerHand.HandValue.ToString();
                }

                await Task.Delay(Constants.TIMEOUT_DELAY);


                if (dealerHand.HandValue > 21)
                {
                await App.Current.MainPage.DisplayAlert("You Win!", "Dealer Busted!", "OK");
               // GameOver(buttons);
                    winOnStand++;
                    wins++;
                manager.GetObsList();
                //put

            }

                else if (dealerHand.HandValue == playerHand.HandValue)
                {
                await App.Current.MainPage.DisplayAlert("Push", "Hands are equal.", "OK");
               // GameOver(buttons);
                    pushes++;
                manager.GetObsList();
                //put

            }

                else if (dealerHand.HandValue < playerHand.HandValue)
                {
                await App.Current.MainPage.DisplayAlert("You Win!", "Your hand is higher!", "OK");
                //GameOver(buttons);
                    wins++;
                    winOnStand++;
                manager.GetObsList();
                //put
            }

                else
                {
                await App.Current.MainPage.DisplayAlert("You Lose!", "Sorry!", "OK");
                //GameOver(buttons);
                losses++;
                loseOnStand++;
                manager.GetObsList();
                //put
            }
                
                PlayAgain(buttons);
            }


            public async Task Hit(FlexLayout[] views, Label[] totals, Button[] buttons)
            {
                if (playDeck.TotalCards == 0)
                    RecombineDecks();


                DrawCard(playerHand, views[Constants.PLAYER], false);
                totals[Constants.PLAYER].Text = playerHand.HandValue.ToString();

                if (playerHand.HandValue > 21)
                {
                
                    busts++;
                    
                    losses++;
                manager.GetObsList();
                //put
                ShowCard(views[Constants.DEALER], totals[Constants.DEALER]);
                buttons[1].IsEnabled = false;
                buttons[2].IsEnabled = false;
                await App.Current.MainPage.DisplayAlert("Bust", "You Lose!", "OK");
                
                manager.Add(c);
                PlayAgain(buttons);
                }
            }


            private void DrawCard(Hand hand, FlexLayout panel, Boolean hideCard)
            {

                hand.Cards.AddLast(playDeck.Cards[playDeck.TotalCards - 1]);
                playDeck.Cards[playDeck.TotalCards - 1] = null;
                playDeck.TotalCards--;
                CalcHandVal(hand);

                Image img = new Image();

                if (hideCard)
                    img.Source = "BackSide.jpg";
                else
                    img.Source = hand.Cards.Last.Value.ImageSource;

                img.HeightRequest = 150;
                
                img.Margin = new Thickness(5);
                panel.Children.Add(img);
            }

            private void CalcHandVal(Hand hand)
            {
                int numOfAces = 0;
                int value = 0;

                foreach (Card card in hand.Cards)
                {
                    if (card.CardVal == 11)
                    {
                        numOfAces++;
                        value += card.CardVal;
                    }
                    else
                    {
                        value += card.CardVal;
                    }
                    if (value > 21 && numOfAces > 0)
                    {
                        numOfAces--;
                        value -= 10;
                    }
                }
                hand.HandValue = value;
            }




            private void DiscardHands()
            {

                while (playerHand.Cards.Count > 0)
                {
                    
                
                    usedDeck.Cards[usedDeck.TotalCards] = playerHand.Cards.Last.Value;
                    usedDeck.TotalCards++;
                    
                
                }


                while (dealerHand.Cards.Count > 0)
                {
                    
                    usedDeck.Cards[usedDeck.TotalCards] = dealerHand.Cards.Last.Value;
                    usedDeck.TotalCards++;
                    dealerHand.Cards.RemoveLast();
                   
                }
            }


            private void ShowCard(FlexLayout dealerPanel, Label dealerTotal)
            {

                dealerPanel.Children.RemoveAt(dealerPanel.Children.Count - 1);

                Image img = new Image();
                img.Source = dealerHand.Cards.Last.Value.ImageSource;
                img.HeightRequest = 150;
                
                img.Margin = new Thickness(5);
                dealerPanel.Children.Add(img);


                dealerTotal.Text = dealerHand.HandValue.ToString();
            }



            private void RecombineDecks()
            {
                while (usedDeck.TotalCards > 0)
                {
                    playDeck.Cards[playDeck.TotalCards] = usedDeck.Cards[usedDeck.TotalCards - 1];
                    usedDeck.Cards[usedDeck.TotalCards - 1] = null;
                    usedDeck.TotalCards--;
                    playDeck.TotalCards++;
                }

                ShuffleDeck(playDeck);
            }


            private void PlayAgain(Button[] buttons)
            {
                buttons[Constants.PLAY_BUTTON].IsEnabled = true;
            }



        

            private void CreateDecks()
            {

                playDeck = new Deck(overallCardCount);
                for (int i = 0; i < overallCardCount; i++)
                {
                    playDeck.Cards[i] = new Card();
                    playDeck.Cards[i].CardVal = Constants.CARDS[i % 13];
                    playDeck.Cards[i].ImageSource = "card" + ((i % 52) + 1) + ".png";
                }

                usedDeck = new Deck(0);

                ShuffleDeck(playDeck);
            }

            private void ShuffleDeck(Deck deck)
            {
                Card[] cards = deck.Cards;
                int n = deck.Cards.Length;

                for (int i = 0; i < (n - 1); i++)
                {
                    int r = i + random.Next(n - i);
                    Card temp = cards[r];
                    cards[r] = cards[i];
                    cards[i] = temp;
                }
            }
        }
}



    

