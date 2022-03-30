namespace War.Entities
{
    public class Game
    {
        public void Run()
        {
            Card[] cardDeck = CreateCardDeck();
            foreach(Card card in cardDeck)
                Console.WriteLine($"{card.value} + {card.suit}");
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
    }
}
