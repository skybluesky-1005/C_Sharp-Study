using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

// 카드 한 장을 표현하는 클래스
public class Card
{
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }

    public Card(Suit s, Rank r)
    {
        Suit = s;
        Rank = r;
    }

    public int GetValue()
    {
        if ((int)Rank <= 10) // 2~10 숫자카드
        {
            return (int)Rank;
        }
        else if ((int)Rank <= 13) // 킹,퀸,잭
        {
            return 10;
        }
        else //에이스
        {
            return 11;
        }
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

// 덱을 표현하는 클래스
public class Deck
{
    private List<Card> cards;

    public Deck() //52장의 트럼프덱 생성
    {
        cards = new List<Card>();

        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(s, r));
            }
        }

        Shuffle();
    }

    public void Shuffle()
    {
        Random rand = new Random();

        for (int i = 0; i < cards.Count; i++)
        {
            int j = rand.Next(i, cards.Count); //i ~ cards.Count 범위 안에서 무작위로 j에 대입
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    public Card DrawCard()
    {
        Card card = cards[0]; //카드 덱에서 1번째 카드 복사
        cards.RemoveAt(0); //카드 덱에서 1번째 카드 제거
        return card;
    }
}

// 패를 표현하는 클래스
public class Hand
{
    private List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public int GetTotalValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            if (card.Rank == Rank.Ace)
            {
                aceCount++;
            }
            total += card.GetValue();
        }

        while (total > 21 && aceCount > 0) //버스트 시 에이스 1장을 1점으로 계산
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }

    public List<Card> GetCards()
    {
        return cards;
    }
}

// 플레이어를 표현하는 클래스
public class Player
{
    public Hand Hand { get; private set; }

    public Player()
    {
        Hand = new Hand();
    }

    public Card DrawCardFromDeck(Deck deck)
    {
        Card drawnCard = deck.DrawCard();
        Hand.AddCard(drawnCard);
        return drawnCard;
    }
}

// 여기부터는 학습자가 작성
// 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
public class Dealer : Player
{
    public void Play(Deck deck)
    {
        while (Hand.GetTotalValue() < 17) DrawCardFromDeck(deck);
    }
}

// 블랙잭 게임을 구현하세요. 
public class Blackjack
{
    private Deck Deck;
    private Player Player;
    private Dealer Dealer;

    public Blackjack(Deck deck, Player player, Dealer dealer)
    {
        Deck = deck;
        Player = player;
        Dealer = dealer;
    }

    public void StartGame()
    {
        Console.WriteLine("블랙잭 게임 시작");
        Dealer.Play(Deck);
        Card card1, card2;
        int currentScore;
        card1 = Player.DrawCardFromDeck(Deck);
        card2 = Player.DrawCardFromDeck(Deck);
        currentScore = Player.Hand.GetTotalValue();
        Console.WriteLine($"첫번째 카드 : {card1.ToString()}");
        Console.WriteLine($"두번째 카드 : {card2.ToString()}");
        Console.WriteLine($"현재 카드의 총 합 : {currentScore}");
    }
}

class Program
{
    static bool isStay = false;

    static void Main()
    {
        Deck deck = new();
        Player player = new();
        Dealer dealer = new();
        Blackjack blackjack = new(deck, player, dealer);

        blackjack.StartGame();
        int dealerScore = dealer.Hand.GetTotalValue();

        while (!isStay)
        {
            DrawOrStay(player, deck);
        }

        Console.WriteLine("딜러의 패 공개");
        Console.WriteLine($"딜러의 패 공개");
        foreach (Card card in dealer.Hand.GetCards())
        {
            Console.WriteLine(card);
        }
        Console.WriteLine($"딜러 카드의 총 합 : {dealerScore}");

        if (player.Hand.GetTotalValue() > dealer.Hand.GetTotalValue()
            || dealer.Hand.GetTotalValue() > 21)
            Console.WriteLine("플레이어 승");
        else if (player.Hand.GetTotalValue() < dealer.Hand.GetTotalValue()
            || dealer.Hand.GetTotalValue() <= 21)
            Console.WriteLine("딜러 승");
        else
            Console.WriteLine("무승부");
    }
    static void DrawOrStay(Player player, Deck deck)
    {
        Console.WriteLine("카드를 뽑습니까?\n뽑는다 : 1\n유지한다 : 2");
        string isDraw = Console.ReadLine();

        if (int.Parse(isDraw) == 1)
        {
            Card card = player.DrawCardFromDeck(deck);
            int currentScore = player.Hand.GetTotalValue();
            Console.WriteLine($"뽑은 카드 : {card}");
            Console.WriteLine($"현재 카드의 총 합 : {currentScore}");
            if (currentScore > 21)
            {
                Console.WriteLine("버스트");
                Console.WriteLine("패배하였습니다");
                Environment.Exit(0); // 버스트 시 콘솔 종료
            }
        }
        else if (int.Parse(isDraw) == 2)
        {
            isStay = true;
        }
        else Console.WriteLine("잘못된 값 입력. 1과 2 중 하나를 입력하세요");
    }
}