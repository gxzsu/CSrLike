using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSrLike
{
    class Enemy
    {
        Random random = new Random();

        private string name;
        private char tile;
        private int x, y, level, attack, defense, health, experienceValue;

        public Enemy(string name, char tile, int level, int attack, int defense, int health, int xp)
        {
            this.name = name;
            this.tile = tile;
            this.level = level;
            this.attack = attack;
            this.defense = defense;
            this.health = health;
            this.experienceValue = xp;
        }

        public string GetName() { return name; }

        public char GetTile() { return tile; }

        public int TakeDamage(int attack)
        {
            // attack -= defense;

            if (attack > 0)
            {
                health -= attack;

                if (health <= 0)
                    return experienceValue;
            }

            return 0;
        }

        public int Attack()
        {
            return random.Next(attack / 2, attack);
        }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int[] GetPosition()
        {
            int[] position = { x, y };
            return position;
        }

        public char GetMove(int playerX, int playerY)
        {
            int dx = x - playerX;
            int dy = y - playerY;

            int adx = Math.Abs(dx);
            int ady = Math.Abs(dy);

            if (adx + ady <= 3)
            {
                if (random.Next(1, 10) <= 3)
                    return '.';

                if (adx > ady)
                {
                    if (dx > 0) return 'a';
                    else return 'd';
                }

                else
                {
                    if (dy > 0) return 'w';
                    else return 's';
                }
            }

            else
            {
                var moveRoll = random.Next(0, 6);

                switch (moveRoll) {
                    case 0:
                        return 'a';
                    case 1:
                        return 'w';
                    case 2:
                        return 's';
                    case 3:
                        return 'd';
                    default:
                        return '.';
                }
            }
        }
    }
}
