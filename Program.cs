/**
 * Author: Marimel Llamoso
 * Date: 02-27-2022
 * Description: Sample blackjack 21 for DesignSpec Coding Exercise
 * 
 */
using BlackJack21.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace BlackJack21
{

    public class Program
    {
        public static readonly IList<string> CardTypes = new ReadOnlyCollection<string>
        (new List<String> {
          "Clubs", "Diamonds", "Hearts", "Spades"
        });
        public static readonly List<CardNameValue> Cards = new()
        {
            new CardNameValue(){
                Name="Ace",
                Value=1
            },
            new CardNameValue(){
                Name="2",
                Value=2
            },
            new CardNameValue(){
                Name="3",
                Value=3
            },
            new CardNameValue(){
                Name="4",
                Value=4
            },
            new CardNameValue(){
                Name="5",
                Value=5
            },
            new CardNameValue(){
                Name="6",
                Value=6
            },
            new CardNameValue(){
                Name="7",
                Value=7
            },
            new CardNameValue(){
                Name="8",
                Value=8
            },
            new CardNameValue(){
                Name="9",
                Value=9
            },
            new CardNameValue(){
                Name="10",
                Value=10
            },
            new CardNameValue(){
                Name="Jack",
                Value=10
            },
            new CardNameValue(){
                Name="Queen",
                Value=10
            },
            new CardNameValue(){
                Name="King",
                Value=10
            }
        };
        static void Main(string[] args)
        {
            Console.WriteLine("BLACK JACK 21");
            //card index used to draw
            var i = 0;
            var isDealerTurn = true;
            var deck = new List<Card>();

            //this will create 1 deck of cards
            foreach(var cardNameVal in Cards)
            {
                // create each card of 4 different types
                foreach (var cardType in CardTypes)
                {
                    Card card = new Card()
                    {
                        Name = cardNameVal.Name,
                        Type = cardType,
                        Value = cardNameVal.Value
                    };
                    deck.Add(card);
                }
            }
            //Shuffles the deck of cards
            deck.Shuffle();
            Console.WriteLine("Shuffling..");
            Thread.Sleep(1000);
            
            //get User First Card
            var userTotal = 0;
            var initialWithAce = false;
            var firstCard = MyExtensions.Draw(deck,i,true);
            string userCardStr = firstCard.Name;
            userTotal += firstCard.Value;
            i = firstCard.Index;
            //this will take account of checking if 1st card has ace therefore different value
            if (firstCard.IsInitialAce == true)
            {
                initialWithAce = true;
            }
            //get Second card
            var secondCard = MyExtensions.Draw(deck, i, true, userTotal,userCardStr);
            userCardStr = secondCard.Name;
            userTotal += secondCard.Value;
            i = secondCard.Index;
            if (secondCard.IsInitialAce == true)
            {
                initialWithAce = true;
            }
            //if both 1st and 2nd card is the total value would be 2
            if (firstCard.IsInitialAce && secondCard.IsInitialAce)
            {
                initialWithAce = false;
                userTotal = 2;
            }
            Console.WriteLine(userCardStr + " = " + userTotal);
            if (userTotal == 21)
            {
                Console.WriteLine("You Won!");
            }
            else
            {
                bool allowUserDraw = true;
                while (allowUserDraw)
                {
                    Console.WriteLine("Would you like to draw another card? type (y/n) then enter- any another letter means");
                    var draw = (Console.ReadLine().ToLower() == "y") ? true : false;
                    if (!draw)
                    {
                        allowUserDraw = draw;
                        

                    }
                    else
                    {
                        var userDraw = MyExtensions.Draw(deck, i, false, userTotal, userCardStr);
                        userCardStr = userDraw.Name;
                        userTotal += userDraw.Value;
                        i = userDraw.Index;
                        //if 1st pair of cards has ace and value is greater than 21 value of ace would be 1
                        if (initialWithAce&& userTotal > 21)
                        {
                            userTotal = userTotal - 10;
                            initialWithAce = false;
                        }
                        else if (userTotal > 21)
                        {
                            
                            Console.WriteLine(userCardStr + " = "+ userTotal);
                            Console.WriteLine("Busted! Dealer Won");
                            Console.WriteLine("Game Over");
                            isDealerTurn = false;
                            break;
                        }
                        //display outcome first
                        Console.WriteLine(userCardStr + " = " + userTotal);
                        if (userTotal == 21)
                        {
                            Console.WriteLine("You Won");
                            isDealerTurn = false;
                            break;
                        }

                    }
                }
                
                if (isDealerTurn)
                {
                    //dealer's turn
                    var dealerTotal = 0;
                    var dealerFirstCard = MyExtensions.Draw(deck, i, true);
                    var isDealerInitialAce = false;
                    var allowDealerDraw = true;
                    string dealerCardStr = dealerFirstCard.Name;
                    dealerTotal += dealerFirstCard.Value;
                    i = dealerFirstCard.Index;
                    if (dealerFirstCard.IsInitialAce)
                    {
                        isDealerInitialAce = true;
                    }
                    Console.WriteLine("Dealer's Turn");
                    Console.WriteLine(dealerCardStr + " = " + dealerTotal);
                    
                    var dealersSecondCard = MyExtensions.Draw(deck, i, true, dealerTotal, dealerCardStr);
                    dealerCardStr = dealersSecondCard.Name;
                    dealerTotal += dealersSecondCard.Value;
                    i = dealersSecondCard.Index;
                    if (dealersSecondCard.IsInitialAce)
                    {
                        isDealerInitialAce = true;
                    }
                    //if both ace
                    if (dealerFirstCard.IsInitialAce && dealersSecondCard.IsInitialAce)
                    {
                        isDealerInitialAce = false;
                        dealerTotal = 2;
                    }
                    Thread.Sleep(2000);
                    Console.WriteLine(dealerCardStr + " = " + dealerTotal);
                    if (dealerTotal == 21)
                    {
                        Console.WriteLine("Dealer Won");
                    }
                    else
                    {
                        while (allowDealerDraw)
                        {
                            
                            //delear total 16 to 21
                            if (dealerTotal>=16&& dealerTotal <= 21)
                            {
                                allowDealerDraw = false;
                                Console.WriteLine((userTotal > dealerTotal) ? "You Won!" : (userTotal == dealerTotal)?"Draw":"Dealer Won");
                            }
                            else
                            {
                                var dealerDraw = MyExtensions.Draw(deck, i, false, dealerTotal, dealerCardStr);
                                dealerCardStr = dealerDraw.Name;
                                dealerTotal += dealerDraw.Value;
                                i = dealerDraw.Index;
                                if (isDealerInitialAce && dealerTotal > 21)
                                {
                                    dealerTotal = dealerTotal - 10;
                                    isDealerInitialAce = false;
                                }
                                else if (dealerTotal > 21)
                                {

                                    Console.WriteLine(dealerCardStr + " = " + dealerTotal);
                                    Console.WriteLine("Dealer Busted! You Won");
                                    Console.WriteLine("Game Over");
                                    isDealerTurn = false;
                                    break;
                                }
                                Console.WriteLine(dealerCardStr + " = " + dealerTotal);
                            }
                            Thread.Sleep(2000);

                        }
                    }
                }
                
            }
            Console.WriteLine("Would you like to restart?(y/n)");
            var resetVal = Console.ReadLine();
            if (resetVal.ToLower() == "y")
            {
                Console.Clear();
                Program.Main(args);
            }
            else
            {
                Environment.Exit(0);
            }
        }
        
    }
    public class GlobalVariables
    {
        public int IndexData { get; set; } = 0;
    }
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
    //credits https://stackoverflow.com/questions/273313/randomize-a-listt
    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static DrawModel Draw(this IList<Card> card, int i, bool isAceTen=false, int prevVal=0, string prevStr="")
        {
            var draw = new DrawModel();
            var cardsDrawnStr = prevStr;
            var value = 0;
            i++;
            var initialStr = (prevStr != "") ? " + " : "";
            cardsDrawnStr += initialStr + card[i].Name + " of " + card[i].Type;
            value += (isAceTen && card[i].Value == 1) ? 11 : card[i].Value;
            if(card[i].Value == 1)
            {
                draw.IsInitialAce = true;
            }
            draw.Name = cardsDrawnStr;
            draw.Value = value;
            draw.Index = i;
            return draw;
        }
    }

}
