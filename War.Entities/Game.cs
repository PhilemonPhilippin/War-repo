namespace War.Entities
{
    public class Game
    {
        public List<Card> jackpot = new List<Card>();
        string winner = "";
        public void Run()
        {
            Card[] cardDeck = CreateCardDeck();
            foreach (Card card in cardDeck)
                Console.WriteLine($"{card.value} + {card.suit}");
            Player playerOne = RegisterPlayer();
            Player playerTwo = RegisterPlayer();
            DistributeCards(playerOne, playerTwo, cardDeck);
            // Loops to check that the decks are well distributed
            foreach (Card card in playerOne.Deck)
                Console.WriteLine($"Player One Card: {card.value} + {card.suit}");
            foreach (Card card in playerTwo.Deck)
                Console.WriteLine($"Player Two Card: {card.value} + {card.suit}");
            while (!IsAnyQueueEmpty(playerOne, playerTwo))
            {
                PlayTurn(playerOne, playerTwo);
            }
            Console.WriteLine("Game over");
            Console.WriteLine($"Winner is : {winner}");
        }


        public void PlayTurn(Player playerOne, Player playerTwo)
        {
            if (!IsAnyQueueEmpty(playerOne, playerTwo))
                Duel(playerOne, playerTwo);
        }
        public void War(Player playerOne, Player playerTwo)
        {
            bool equality = true;
            while (equality)
            {
                Console.WriteLine("WAR!!");
                if (!IsAnyQueueEmpty(playerOne, playerTwo))
                {
                    Card hiddenCardOne = playerOne.Deck.Dequeue();
                    Console.WriteLine($"Hidden card of player one: {hiddenCardOne.value} + {hiddenCardOne.suit}");
                    Card hiddenCardTwo = playerTwo.Deck.Dequeue();
                    Console.WriteLine($"Hidden card of player two: {hiddenCardTwo.value} + {hiddenCardTwo.suit}");
                    jackpot.Add(hiddenCardOne);
                    jackpot.Add(hiddenCardTwo);
                    Console.WriteLine($"Jackpot count: {jackpot.Count}");
                    if (!IsAnyQueueEmpty(playerOne, playerTwo))
                    {
                        Card cardOne = playerOne.Deck.Dequeue();
                        Console.WriteLine($"Card of player one : {cardOne.value} + {cardOne.suit}");
                        Card cardTwo = playerTwo.Deck.Dequeue();
                        Console.WriteLine($"Card of player two : {cardTwo.value} + {cardTwo.suit}");
                        jackpot.Add(cardOne);
                        jackpot.Add(cardTwo);
                        Console.WriteLine($"Jackpot count: {jackpot.Count}");
                        if (cardOne.value > cardTwo.value)
                        {
                            CashOutJackpot(playerOne);
                            equality = false;
                        }
                        else if (cardTwo.value > cardOne.value)
                        {
                            CashOutJackpot(playerTwo);
                            equality = false;
                        }
                        else
                        {
                            Console.WriteLine("Equality again");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Un des joueurs n'a plus de carte");
                        equality = false;
                        if (playerOne.Deck.Count == 0)
                            winner = "player two";
                        else if (playerTwo.Deck.Count == 0)
                            winner = "player one";
                        Console.WriteLine($"Winner is : {winner}");
                    }
                }
            }
        }

        public void Duel(Player playerOne, Player playerTwo)
        {
            if (!IsAnyQueueEmpty(playerOne, playerTwo))
            {
                Card cardOne = playerOne.Deck.Dequeue();
                Console.WriteLine($"Card of player one : {cardOne.value} + {cardOne.suit}");
                Card cardTwo = playerTwo.Deck.Dequeue();
                Console.WriteLine($"Card of player two : {cardTwo.value} + {cardTwo.suit}");
                jackpot.Add(cardOne);
                jackpot.Add(cardTwo);
                Console.WriteLine($"Jackpot count: {jackpot.Count}");
                if (cardOne.value > cardTwo.value)
                {
                    CashOutJackpot(playerOne);
                }
                else if (cardTwo.value > cardOne.value)
                {
                    CashOutJackpot(playerTwo);
                }
                else
                {
                    War(playerOne, playerTwo);
                }
            }
        }
        public void CashOutJackpot(Player player)
        {
            Console.WriteLine($"{player.Name} wins the jackpot of : {jackpot.Count} cards");
            // je simule de mélanger les cartes du jackpot avant de les remettre dans le deck du joueur
            while (jackpot.Count > 0)
            {
                Random rand = new Random();
                int jackpotCardIndex = rand.Next(0, jackpot.Count);
                player.Deck.Enqueue(jackpot[jackpotCardIndex]);
                jackpot.Remove(jackpot[jackpotCardIndex]);
            }
        }
        public bool IsAnyQueueEmpty(Player playerOne, Player playerTwo)
        {
            if (playerOne.Deck.Count == 0)
                winner = "player two";
            else if (playerTwo.Deck.Count == 0)
                winner = "player one";
            return playerOne.Deck.Count == 0 || playerTwo.Deck.Count == 0;
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

        public void DistributeCards(Player playerOne, Player playerTwo, Card[] cardDeck)
        {
            playerOne.Deck = new Queue<Card>();
            playerTwo.Deck = new Queue<Card>();
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
            while (playerTwo.Deck.Count < 26)
            {
                Random rand = new Random();
                int randIndex = rand.Next(0, list.Count);
                playerTwo.Deck.Enqueue(list[randIndex]);
                list.Remove(list[randIndex]);
            }
        }

        public Player RegisterPlayer()
        {
            Player player = new Player();
            Console.WriteLine("Hello player. What is your name?");
            player.Name = Console.ReadLine();
            return player;
        }
    }
}
