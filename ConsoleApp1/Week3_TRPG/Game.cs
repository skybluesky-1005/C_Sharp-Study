namespace Week4_TRPG
{
    public interface ICharacter
    {
        string Name { get; }
        int Health { get; set; }
        int Attack { get; }
        bool IsDead { get; }
        void TakeDamage(int damage);
    }

    public class Warrior : ICharacter
    {
        public string Name { get; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public bool IsDead => Health <= 0;
        public int Attack => new Random().Next(10, AttackPower); // 공격력은 랜덤

        public Warrior(string name) // 초기화
        {
            Name = name;
            Health = 100;
            AttackPower = 20;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 대미지를 받았습니다. 남은 체력: {Health}");
        }
    }

    public class Monster : ICharacter
    {
        public string Name { get; }
        public int Health { get; set; }
        public int Attack => new Random().Next(10, 20);
        public bool IsDead => Health <= 0;

        public Monster(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 대미지를 받았습니다. 남은 체력: {Health}");
        }
    }

    public class Goblin : Monster
    {
        public Goblin(string name) : base(name, 50) { }
    }
    
    public class Dragon : Monster
    {
        public Dragon(string name) : base(name, 100) { }
    }

    public interface IItem
    {
        string Name { get; }
        void Use(Warrior warrior);// 전사에게 아이템을 사용하는 메서드
    }

    public class HealthPotion : IItem
    {
        public string Name => "체력 포션";

        public void Use(Warrior warrior)
        {
            Console.WriteLine("체력 포션을 사용합니다. 체력이 50 증가합니다.");
            warrior.Health += 50;
            if (warrior.Health > 100) warrior.Health = 100;
        }
    }

    public class StrengthPotion : IItem
    {
        public string Name => "공격력 포션";

        public void Use(Warrior warrior)
        {
            Console.WriteLine("공격력 포션을 사용합니다. 공격력이 10 증가합니다.");
            warrior.AttackPower += 10;
        }
    }

    public class Stage
    {
        private ICharacter player;
        private ICharacter monster;
        private List<IItem> reward;

        //이벤트 델리게이트 정의
        public delegate void GameEvent(ICharacter character);
        public event GameEvent OnCharacterDeath;

        public Stage(ICharacter player, ICharacter monster, List<IItem> reward)
        {
            this.player = player;
            this.monster = monster;
            this.reward = reward;
            OnCharacterDeath += StageClear;
        }

        public void Start()
        {
            Console.WriteLine($"스태이지 시작!\n플레이어 정보\n체력: {player.Health}\n 공격력: {player.Attack}");
            Console.WriteLine($"몬스터 정보\n이름: {monster.Name}\n체력: {monster.Health}\n 공격력: {monster.Attack}");
            Console.WriteLine($"─────────────────────────────────────────────────────────────────────────────────");

            while (!player.IsDead && !monster.IsDead)
            {
                Console.WriteLine($"{player.Name}의 턴!");
                monster.TakeDamage(player.Attack);
                Thread.Sleep(1000);

                if (monster.IsDead) break;

                Console.WriteLine($"{monster.Name}의 턴!");
                player.TakeDamage(monster.Attack);
                Thread.Sleep(1000);
            }

            if (player.IsDead) OnCharacterDeath?.Invoke(player);
            else if (monster.IsDead) OnCharacterDeath?.Invoke(monster);
        }

        private void StageClear(ICharacter character)
        {
            if (character is Monster)
            {
                Console.WriteLine($"스테이지 클리어! {character.Name}을(를) 물리쳤습니다!");
                if (reward != null)
                {
                    Console.WriteLine("아래의 보상 아이템 중 하나를 선택하여 사용할 수 있습니다.");
                    foreach (var item in reward) Console.WriteLine(item.Name);
                    Console.WriteLine("사용할 아이템 이름을 입력하세요");
                    string input = Console.ReadLine();
                    IItem selectedItem = reward.Find(item => item.Name == input);
                    if (selectedItem != null) selectedItem.Use((Warrior)player);
                }

                player.Health = 100;
            }
            else Console.WriteLine("게임 오버! 패배했습니다...");
        }

    }


    class Game
    {
        static void Main(string[] args)
        {
            Warrior player = new Warrior("Player");
            Goblin goblin = new Goblin("Goblin");
            Dragon dragon = new Dragon("Dragon");

            List<IItem> stage1Rewards = new List<IItem>
            {
                new HealthPotion(),
                new StrengthPotion()
            };
            List<IItem> stage2Rewards = new List<IItem>
            {
                new HealthPotion(),
                new StrengthPotion()
            };

            Stage stage1 = new Stage(player, goblin, stage1Rewards);
            stage1.Start();

            if (player.IsDead) return;

            Stage stage2 = new Stage(player, dragon, stage2Rewards);
            stage2.Start();

            if (player.IsDead) return;

            Console.WriteLine("축하합니다! 모든 스테이지를 클리어했습니다!");
        }
    }
}