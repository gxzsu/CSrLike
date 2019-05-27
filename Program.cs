using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSrLike
{
    static class LanguageEngine {
        public static int MainMenu1 = 0, MainMenu2 = 1, MovementCommands = 2, DeathNotification = 3, Snake = 4,
            Goblin = 5, Ogre = 6, Dragon = 7, Bandit = 8, SnakeAttack = 9, GoblinAttack = 10, OgreAttack = 11,
            DragonAttack = 12, BanditAttack = 13, Damage = 14, SnakeAttacksYou = 15, GoblinAttacksYou = 16,
            OgreAttacksYou = 17, DragonAttacksYou = 18, BanditAttacksYou = 19, LevelledUp = 20, SnakeDied = 21,
            GoblinDied = 22, OgreDied = 23, DragonDied = 24, BanditDied = 25, Level = 26, Health = 27, 
            Attack = 28, XP = 29;

        public static string[] langInUse = langEnglish;

        static string[] langJapanese = 
        {
            "\n\n(おそらくあまり良くない) ローグライクゲーム (C#)",
            "\n\n\t(c) Gareth Egglestone - 全著作権所有。",
            "移動の指揮を入力 (w/s/a/d): ",
            "あなたが死んでいます。",
            "蛇",
            "ゴブリン",
            "鬼",
            "竜",
            "泥棒",
            "プレイヤーは蛇を攻撃した！",
            "プレイヤーはゴブリンを攻撃した！",
            "プレイヤーは鬼を攻撃した！",
            "プレイヤーは竜を攻撃した！",
            "プレイヤーは泥棒を攻撃した！",
            "点のダメージ",
            "蛇はプレイヤーを攻撃した！",
            "ゴブリンはプレイヤーを攻撃した！",
            "鬼はプレイヤーを攻撃した！",
            "竜はプレイヤーを攻撃した！",
            "泥棒はプレイヤーを攻撃した！",
            "レベルアップした!",
            "蛇が敗北した！",
            "ゴブリンが敗北した！",
            "鬼が敗北した！",
            "竜が敗北した！",
            "泥棒が敗北した！",
            "レベル",
            "健康",
            "アタック",
            "ＸＰ",
        };

        static string[] langEnglish =
        {
            "\n\n(probably not very good) rogue-like game (C#)",
            "\n\n\t(c) Gareth Egglestone",
            "Enter a movement command (w/s/a/d): ",
            "You are dead.",
            "Snake",
            "Goblin",
            "Ogre",
            "Dragon",
            "Bandit",
            "You attacked the Snake!",
            "You attacked the Goblin!",
            "You attacked the Ogre!",
            "You attacked the Dragon!",
            "You attacked the Bandit!",
            "damage",
            "The Snake attacks you!",
            "The Goblin attacks you!",
            "The Ogre attacks you!",
            "The Dragon attacks you!",
            "The Bandit attacks you!",
            "Levelled up!",
            "The Snake died!",
            "The Goblin died!",
            "The Ogre died!",
            "The Dragon died!",
            "The Bandit died!",
            "Level",
            "Health",
            "Attack",
            "XP",
        };

        static string[] langJvtsli = 
        {
            "\n\n(hkgwqwsn fgz ctkn uggr) kguxt-soat uqdt (E#)",
            "\n\n\t(e) Uqktzi Tuustlzgft - qss kouizl ktltkctr.",
            "Tfztk q dgctdtfz egddqfr (v/l/q/r): ",
            "Ngx qkt rtqr.",
            "Lfqat",
            "Ugwsof",
            "Gukt",
            "Rkqugf",
            "Wqfroz",
            "Ngx qzzqeatr zit Lfqat!",
            "Ngx qzzqeatr zit Ugwsof!",
            "Ngx qzzqeatr zit Gukt!",
            "Ngx qzzqeatr zit Rkqugf!",
            "Ngx qzzqeatr zit Wqfroz!",
            "rqdqut",
            "Zit Lfqat qzzqeal ngx!",
            "Zit Ugwsof qzzqeal ngx!",
            "Zit Gukt qzzqeal ngx!",
            "Zit Rkqugf qzzqeal ngx!",
            "Zit Wqfroz qzzqeal ngx!",
            "Stctsstr xh!",
            "Zit Lfqat rotr!",
            "Zit Ugwsof rotr!",
            "Zit Gukt rotr!",
            "Zit Rkqugf rotr!",
            "Zit Wqfroz rotr!",
            "Stcts",
            "Itqszi",
            "Qzzqea",
            "BH",
        };

        static string[] langFrench =
        {
            "\n\n(probablement pas très bien) jeu de rogue-like (C#)",
            "\n\n\t(c) Gareth Egglestone - tous les droits sont réservés.",
            "Tapez une commande de mouvement (w/s/a/d): ",
            "Tu es mort.",
            "Serpent",
            "Lutin",
            "Ogre",
            "Dragon",
            "Bandit",
            "Vous avez attaqué le serpent!",
            "Vous avez attaqué le lutin!",
            "Vous avez attaqué l'ogre!",
            "Vous avez attaqué le dragon!",
            "Vous avez attaqué le bandit!",
            "dommage",
            "Le serpent vous attaque!",
            "Le lutin vous attaque!",
            "L'ogre vous attaque!",
            "Le dragon vous attaque!",
            "Le bandit vous attaque!",
            "Nivelé!",
            "Le serpent est mort!",
            "Le lutin est mort!",
            "L'ogre est mort!",
            "Le dragon est mort!",
            "Le bandit est mort!",
            "Niveau",
            "Santé",
            "Attaque",
            "XP",
        };

        public static void SetupEngine(string langCode = "en")
        {
            if (langCode == "en")
                langInUse = langEnglish;

            else if (langCode == "jp")
                langInUse = langJapanese;

            else if (langCode == "jv")
                langInUse = langJvtsli;

            else if (langCode == "fr")
                langInUse = langFrench;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            if (args.Length > 0)
                LanguageEngine.SetupEngine(args[0].ToLower());

            else
                LanguageEngine.SetupEngine();

            Program program = new Program();
            program.Entry();

        }

        void Entry()
        {
            Player _player = new Player(1, 100, 10, 10, 0);
            Level _level = new Level();
            GameSystem gameSystem = new GameSystem();

            if (LanguageEngine.langInUse[0].Contains("ローグライクゲーム"))
            {
                Console.WriteLine("\n\t警告：日本語の文字がWindowsで正しく表現されないことがあります。");
                Console.WriteLine("\tKeikoku: nihon-go no moji ga Windows de tadashiku hyōgen sa renai koto ga arimasu.");
                Console.WriteLine("\t私のゲームをプレーしていただきありがとうございます！\n\t注意：タズテクの助言を受け入れる結果は火ですよ～\n");
            }

            Console.WriteLine("Translations: ");
            Console.WriteLine("> English (EN), Français (FR), 日本語 (JP), Jvtsli (JV): Gareth Egglestone");

            Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.MainMenu1]);
            Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.MainMenu2]);

            // Console.WriteLine("\n\n(probably not very good) rogue-like game (C#)");
            // Console.WriteLine("\n\n\t(c) Gareth Egglestone");

            Console.ReadKey();

            Console.Clear();

            _level.LoadLevel("level1.txt");
            _level.ProcessLevel(_player);

            gameSystem.GameLoop(_level, _player);

            Console.ReadLine();
        }
    }
}