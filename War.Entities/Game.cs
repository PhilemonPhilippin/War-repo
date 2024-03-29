﻿namespace War.Entities
{
    public class Game
    {
        public List<Card> jackpot = new List<Card>();
        Player playerOne;
        Player artificialEnemy;

        public void Run()
        {
            Card[] cardDeck = CreateCardDeck();
            playerOne = RegisterPlayer();
            artificialEnemy = new Player() { Name = "AI Bot" };
            DistributeCards(cardDeck);
            while (!IsAnyQueueEmpty())
            {
                Duel();
            }
            DeclareWinner();
        }
        public void War()
        {
            while (!IsAnyQueueEmpty())
            {
                DeclarationOfWar();
                Card hiddenCardOne = playerOne.Deck.Dequeue();
                Console.WriteLine($"Hidden card of {playerOne.Name}, inserted in the jackpot: {hiddenCardOne.value} + {hiddenCardOne.suit}");
                Card hiddenCardTwo = artificialEnemy.Deck.Dequeue();
                Console.WriteLine($"Hidden card of {artificialEnemy.Name}, inserted in the jackpot: {hiddenCardTwo.value} + {hiddenCardTwo.suit}");
                jackpot.Add(hiddenCardOne);
                jackpot.Add(hiddenCardTwo);
                if (!IsAnyQueueEmpty())
                {
                    Card cardOne = playerOne.Deck.Dequeue();
                    Console.WriteLine($"Card of {playerOne.Name} : {cardOne.value} + {cardOne.suit}");
                    Card cardTwo = artificialEnemy.Deck.Dequeue();
                    Console.WriteLine($"Card of {artificialEnemy.Name} : {cardTwo.value} + {cardTwo.suit}");
                    jackpot.Add(cardOne);
                    jackpot.Add(cardTwo);
                    if (cardOne.value > cardTwo.value)
                        CashOutJackpot(playerOne);
                    else if (cardTwo.value > cardOne.value)
                        CashOutJackpot(artificialEnemy);
                    else
                        Console.WriteLine("Equality again");
                }
            }
        }
        public void DeclarationOfWar()
        {
            Console.WriteLine("EQUALITY between your cards !!");
            Console.WriteLine("Declare WAR to your enemy if you want to proceed.");
            string userInput;
            do
            {
                Console.WriteLine("To declare war, simply write war ...");
                userInput = Console.ReadLine().ToLower();
            } while (userInput != "war");
        }
        public void Duel()
        {
            if (!IsAnyQueueEmpty())
            {
                Card cardOne = playerOne.Deck.Dequeue();
                Console.WriteLine($"Card of {playerOne.Name} : {cardOne.value} + {cardOne.suit}");
                Card cardTwo = artificialEnemy.Deck.Dequeue();
                Console.WriteLine($"Card of {artificialEnemy.Name} : {cardTwo.value} + {cardTwo.suit}");
                jackpot.Add(cardOne);
                jackpot.Add(cardTwo);
                if (cardOne.value > cardTwo.value)
                    CashOutJackpot(playerOne);
                else if (cardTwo.value > cardOne.value)
                    CashOutJackpot(artificialEnemy);
                else
                    War();
            }
        }
        public void CashOutJackpot(Player player)
        {
            Console.WriteLine($"{player.Name} wins the jackpot of : {jackpot.Count} cards");
            // I simulate shuffling cards before putting them in the winner's queue
            while (jackpot.Count > 0)
            {
                Random rand = new Random();
                int jackpotCardIndex = rand.Next(0, jackpot.Count);
                player.Deck.Enqueue(jackpot[jackpotCardIndex]);
                jackpot.Remove(jackpot[jackpotCardIndex]);
            }
            Console.WriteLine($"Number of cards in {playerOne.Name}'s deck: {playerOne.Deck.Count}.");
            Console.WriteLine($"Number of cards in {artificialEnemy.Name}'s deck: {artificialEnemy.Deck.Count}.");
            Console.WriteLine("====================================");
        }
        public bool IsAnyQueueEmpty()
        {
            return playerOne.Deck.Count == 0 || artificialEnemy.Deck.Count == 0;
        }
        public void DeclareWinner()
        {
            if (playerOne.Deck.Count == 0)
            {
                Console.WriteLine($"{playerOne.Name} has no card anymore!");
                Console.WriteLine($"The winner is: {artificialEnemy.Name}");
            }
            else if (artificialEnemy.Deck.Count == 0)
            {
                Console.WriteLine($"{artificialEnemy.Name} has no card anymore!");
                Console.WriteLine($"The winner is: {playerOne.Name}");
            }
            Console.WriteLine("Game over");
        }
        public Card[] CreateCardDeck()
        {
            Card[] cardDeck = new Card[52];
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Card card = new Card();
                    card.suit = (Suit)i;
                    card.value = (Value)j;
                    cardDeck[counter] = card;
                    counter++;
                }
            }
            return cardDeck;
        }
        public void DistributeCards(Card[] cardDeck)
        {
            playerOne.Deck = new Queue<Card>();
            artificialEnemy.Deck = new Queue<Card>();
            // Create a list to store all cards of the central deck to distribute them later
            List<Card> list = new List<Card>();
            foreach (Card card in cardDeck)
                list.Add(card);
            while (playerOne.Deck.Count < 26)
            {
                Random rand = new Random();
                int randIndex = rand.Next(0, list.Count);
                playerOne.Deck.Enqueue(list[randIndex]);
                list.Remove(list[randIndex]);
            }
            while (artificialEnemy.Deck.Count < 26)
            {
                Random rand = new Random();
                int randIndex = rand.Next(0, list.Count);
                artificialEnemy.Deck.Enqueue(list[randIndex]);
                list.Remove(list[randIndex]);
            }
        }
        public Player RegisterPlayer()
        {
            Player player = new Player();
            Console.WriteLine("Hello player ! What is your name? ...");
            player.Name = Console.ReadLine();
            return player;
        }
    }
}
