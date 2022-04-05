namespace War.Entities
{
    public class Game
    {
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
        }


        public void PlayTurn(Player playerOne, Player playerTwo)
        {
            if (!IsAnyQueueFull(playerOne, playerTwo))
            {
                
            }
        }
        public void War(Player playerOne, Player playerTwo)
        {

        }

        public void Duel(Player playerOne, Player playerTwo)
        {

        }
        public bool IsAnyQueueFull(Player playerOne, Player playerTwo)
        {
            return playerOne.Deck.Count == 52 || playerTwo.Deck.Count == 52;
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
