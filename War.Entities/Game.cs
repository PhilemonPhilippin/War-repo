namespace War.Entities
{
    public class Game
    {
        //TODO: Faire une méthode WarDeclaration() interactive
        //Personnaliser les "player one player two"
        //Clean le code
        public List<Card> jackpot = new List<Card>();
        string winner = "";
        Player playerOne;
        Player artificialEnemy;
        
        public void Run()
        {
            Card[] cardDeck = CreateCardDeck();
            foreach (Card card in cardDeck)
                Console.WriteLine($"{card.value} + {card.suit}");
            playerOne = RegisterPlayer();
            artificialEnemy = new Player () { Name="ArtificialEnemy"};
            DistributeCards(cardDeck);
            while (!IsAnyQueueEmpty())
            {
                Duel();
            }
            Console.WriteLine("Game over");
            Console.WriteLine($"Winner is : {winner}");
        }
        public void War()
        {
            bool equality = true;
            while (equality)
            {
                DeclarationOfWar();
                if (!IsAnyQueueEmpty())
                {
                    Card hiddenCardOne = playerOne.Deck.Dequeue();
                    Console.WriteLine($"Hidden card of player one: {hiddenCardOne.value} + {hiddenCardOne.suit}");
                    Card hiddenCardTwo = artificialEnemy.Deck.Dequeue();
                    Console.WriteLine($"Hidden card of player two: {hiddenCardTwo.value} + {hiddenCardTwo.suit}");
                    jackpot.Add(hiddenCardOne);
                    jackpot.Add(hiddenCardTwo);
                    Console.WriteLine($"Jackpot count: {jackpot.Count}");
                    if (!IsAnyQueueEmpty())
                    {
                        Card cardOne = playerOne.Deck.Dequeue();
                        Console.WriteLine($"Card of player one : {cardOne.value} + {cardOne.suit}");
                        Card cardTwo = artificialEnemy.Deck.Dequeue();
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
                            CashOutJackpot(artificialEnemy);
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
                        else if (artificialEnemy.Deck.Count == 0)
                            winner = "player one";
                        Console.WriteLine($"Winner is : {winner}");
                    }
                }
            }
        }
        public void DeclarationOfWar()
        {
            Console.WriteLine("Equality between your cards!");
            Console.WriteLine("Declare war to your ennemy if you want to proceed.");
            string userInput = "";
            while (userInput != "war")
            {
                Console.WriteLine("To declare war, simply write war");
                userInput = Console.ReadLine().ToLower();
            }
        }

        public void Duel()
        {
            if (!IsAnyQueueEmpty())
            {
                Card cardOne = playerOne.Deck.Dequeue();
                Console.WriteLine($"Card of player one : {cardOne.value} + {cardOne.suit}");
                Card cardTwo = artificialEnemy.Deck.Dequeue();
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
                    CashOutJackpot(artificialEnemy);
                }
                else
                {
                    War();
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
        public bool IsAnyQueueEmpty()
        {
            if (playerOne.Deck.Count == 0)
                winner = artificialEnemy.Name;
            else if (artificialEnemy.Deck.Count == 0)
                winner = playerOne.Name;
            return playerOne.Deck.Count == 0 || artificialEnemy.Deck.Count == 0;
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
            Console.WriteLine("Hello player. What is your name?");
            player.Name = Console.ReadLine();
            return player;
        }
    }
}
