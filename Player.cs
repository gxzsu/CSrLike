using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSrLike
{
    class Player
    {
        Random random = new Random();
        public int x = 0, y = 0;
        private int level, health, attack, defense, experience;

        public Player(int level, int health, int attack, int defense, int experience)
        {
            this.level = level;
            this.health = health;
            this.attack = attack;
            this.defense = defense;
            this.experience = experience;
        }

        public string GetStatsToDisplay()
        { 
            // return $"\n統計：\nレベル：{level}\n健康: {health}\nアタック：{attack}\nＸＰ：{experience}/50";
            return $"\n" + LanguageEngine.langInUse[LanguageEngine.Level] + $": {level}\n" + 
                LanguageEngine.langInUse[LanguageEngine.Health] + $": {health}\n" + 
                LanguageEngine.langInUse[LanguageEngine.Attack] + $": {attack}\n" + 
                LanguageEngine.langInUse[LanguageEngine.XP] + $": {experience} / 50";
        }

        public int TakeDamage(int attack)
        {
            if (attack > 0)
            {
                health -= attack;

                if (health <= 0)
                    return 1;
            }

            return 0;
        }

        public void AddExperience(int experienceToAdd)
        {
            experience += experienceToAdd;

            while(experience >= 50)
            {
                Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.LevelledUp]);
                // Console.WriteLine("Levelled up!");
                // Console.WriteLine("レベルアップした!");

                experience -= 50;

                attack += 10;
                defense += 5;
                health += 10;
                level++;

                Console.ReadLine();
            }
        }

        public int Attack()
        {
            return random.Next(attack / 2, attack);
        }

        public int GetHealth()
        {
            return this.health;
        }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int[] GetPosition()
        {
            int[] positionsArray = { this.x, this.y };

            return positionsArray;
        }
    }
}
