using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace CSrLike
{
    class Level 
    {
        private string[,] mapData = new string[17, 41];

        List<Enemy> enemies = new List<Enemy>();

        public void LoadLevel(string levelFile)
        {
            string input = File.ReadAllText(levelFile);

            int i = 0, j = 0;

            foreach (var row in input.Split('\n'))
            {
                j = 0;

                foreach (var col in row.Trim())
                {
                    mapData[i, j] = col.ToString();
                    j++;
                }

                i++;
            }
        }

        public void ProcessLevel(Player player)
        {
            for (short ROW = 0; ROW <= mapData.GetUpperBound(0); ROW++)
            {
                for (short COL = 0; COL <= mapData.GetUpperBound(1); COL++)
                {
                    string tile = mapData[ROW, COL];

                    switch (tile)
                    {
                        case "@":
                            player.SetPosition(COL, ROW);
                            break;
                        case "S":
                            enemies.Add(new Enemy("Snake", tile.ToCharArray()[0], 1, 3, 1, 10, 50));
                            // enemies.Add(new Enemy("蛇", tile.ToCharArray()[0], 1, 3, 1, 10, 50));
                            enemies[enemies.Count - 1].SetPosition(COL, ROW);
                            break;
                        case "g":
                            enemies.Add(new Enemy("Goblin", tile.ToCharArray()[0], 2, 10, 5, 35, 150));
                            // enemies.Add(new Enemy("ゴブリン", tile.ToCharArray()[0], 2, 10, 5, 35, 150));
                            enemies[enemies.Count - 1].SetPosition(COL, ROW);
                            break;
                        case "O":
                            enemies.Add(new Enemy("Ogre", tile.ToCharArray()[0], 4, 20, 40, 200, 500));
                            // enemies.Add(new Enemy("鬼", tile.ToCharArray()[0], 4, 20, 40, 200, 500));
                            enemies[enemies.Count - 1].SetPosition(COL, ROW);
                            break;
                        case "D":
                            enemies.Add(new Enemy("Dragon", tile.ToCharArray()[0], 100, 2000, 2000, 2000, 10000));
                            // enemies.Add(new Enemy("竜", tile.ToCharArray()[0], 100, 2000, 2000, 2000, 10000));
                            enemies[enemies.Count - 1].SetPosition(COL, ROW);
                            break;
                        case "B":
                            enemies.Add(new Enemy("Bandit", tile.ToCharArray()[0], 3, 15, 10, 100, 250));
                            // enemies.Add(new Enemy("泥棒", tile.ToCharArray()[0], 3, 15, 10, 100, 250));
                            enemies[enemies.Count - 1].SetPosition(COL, ROW);
                            break;
                    }
                }
            }
        }

        public void Print()
        {
            for (short ROW = 0; ROW <= mapData.GetUpperBound(0); ROW++)
            {
                for (short COL = 0; COL <= mapData.GetUpperBound(1); COL++)
                {
                    var currentPosition = mapData[ROW, COL];
                    if (currentPosition == "@")
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;

                    else if (currentPosition == "#")
                        Console.ForegroundColor = ConsoleColor.Gray;

                    else if (currentPosition == ".")
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                    else Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write(currentPosition);
                }

                Console.Write("\n");
            }
        }

        public void MovePlayer(char input, Player player)
        {
            var playerX = player.GetPosition()[0];
            var playerY = player.GetPosition()[1];

            switch (input)
            {
                case 'w':
                case 'W':
                    ProcessPlayerMove(player, playerX, playerY - 1);
                    break;
                case 's':
                case 'S':
                    ProcessPlayerMove(player, playerX, playerY + 1);
                    break;
                case 'a':
                case 'A':
                    ProcessPlayerMove(player, playerX - 1, playerY);
                    break;
                case 'd':
                case 'D':
                    ProcessPlayerMove(player, playerX + 1, playerY);
                    break;

                default:
                    Console.WriteLine("Invalid input..");
                    break;
            }
        }

        void ProcessPlayerMove(Player player, int targX, int targY)
        {
            var playerX = player.GetPosition()[0];
            var playerY = player.GetPosition()[1];

            var moveTile = GetTileAt(targX, targY);

            Console.WriteLine();

            switch (moveTile)
            {
                case '.':
                    player.SetPosition(targX, targY);
                    SetTile(playerX, playerY, '.');
                    SetTile(targX, targY, '@');
                    break;
                case '#':
                    break;

                default:
                    BattleEnemy(player, targX, targY);
                    break;
            }
        }

        void ProcessEnemyMove(Player player, int enemyIndex, int targX, int targY)
        {
            var playerX = player.GetPosition()[0];
            var playerY = player.GetPosition()[1];

            var enemyX = enemies[enemyIndex].GetPosition()[0];
            var enemyY = enemies[enemyIndex].GetPosition()[1];

            var moveTile = GetTileAt(targX, targY);

            switch (moveTile)
            {
                case '.':
                    enemies[enemyIndex].SetPosition(targX, targY);
                    SetTile(enemyX, enemyY, '.');
                    SetTile(targX, targY, enemies[enemyIndex].GetTile());
                    break;
                case '@':
                    BattleEnemy(player, enemyX, enemyY);
                    break;
            }
        }

        void BattleEnemy(Player player, int targX, int targY)
        {
            for (int index = 0; index < enemies.Count; index++)
            {
                var enemyX = enemies[index].GetPosition()[0];
                var enemyY = enemies[index].GetPosition()[1];

                if (targX == enemyX && targY == enemyY)
                {
                    var attackRoll = player.Attack();
                    var attackResult = enemies[index].TakeDamage(attackRoll);

                    if (enemies[index].GetName() == "Snake")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.SnakeAttack] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Goblin")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.GoblinAttack] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Ogre")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.OgreAttack] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Dragon")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.DragonAttack] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Bandit")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.BanditAttack] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    // Console.WriteLine($"You attacked the {enemies[index].GetName()} with {attackRoll} damage!");
                    // Console.WriteLine($"プレイヤーは「{enemies[index].GetName()}」を攻撃した！（{attackRoll}点のダメージ）");

                    Console.ReadLine();

                    if (attackResult != 0)
                    {
                        player.AddExperience(attackResult);
                        SetTile(targX, targY, '.');
                        if (enemies[index].GetName() == "Snake")
                            Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.SnakeDied]);

                        if (enemies[index].GetName() == "Goblin")
                            Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.GoblinDied]);

                        if (enemies[index].GetName() == "Ogre")
                            Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.OgreDied]);

                        if (enemies[index].GetName() == "Dragon")
                            Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.DragonDied]);

                        if (enemies[index].GetName() == "Bandit")
                            Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.BanditDied]);

                        // Console.WriteLine($"「{enemies[index].GetName()}」が敗北した！");

                        enemies.RemoveAt(index);

                        Console.ReadLine();

                        return;
                    }

                    attackRoll = enemies[index].Attack();
                    attackResult = player.TakeDamage(attackRoll);

                    if (enemies[index].GetName() == "Snake")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.SnakeAttacksYou] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Goblin")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.GoblinAttacksYou] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Ogre")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.OgreAttacksYou] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Dragon")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.DragonAttacksYou] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    if (enemies[index].GetName() == "Bandit")
                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.BanditAttacksYou] + $" ({attackRoll} " + LanguageEngine.langInUse[LanguageEngine.Damage] + ")");

                    // Console.WriteLine($"The {enemies[index].GetName()} attacked you with {attackRoll} damage!");
                    // Console.WriteLine($"「{enemies[index].GetName()}」はプレイヤーを攻撃した！（{attackRoll}点のダメージ）");

                    Console.ReadLine();

                    if (attackResult != 0) 
                    {
                        SetTile(player.x, player.y, 'x');

                        Console.WriteLine(LanguageEngine.langInUse[LanguageEngine.DeathNotification]);
                        // Console.WriteLine("プレイヤーが死んでいる。");
                        Console.ReadLine();
                    }

                    return;
                }                    
            }
        }

        public void UpdateEnemies(Player player)
        {
            char aiMove;

            int playerX = player.GetPosition()[0];
            int playerY = player.GetPosition()[1];

            for (int i = 0; i < enemies.Count; i++)
            {
                aiMove = enemies[i].GetMove(playerX, playerY);

                var enemyX = enemies[i].GetPosition()[0];
                var enemyY = enemies[i].GetPosition()[1];

                switch (aiMove)
                {
                    case 'w':
                        ProcessEnemyMove(player, i, enemyX, enemyY - 1);
                        break;
                    case 's':
                        ProcessEnemyMove(player, i, enemyX, enemyY + 1);
                        break;
                    case 'a':
                        ProcessEnemyMove(player, i, enemyX - 1, enemyY);
                        break;
                    case 'd':
                        ProcessEnemyMove(player, i, enemyX + 1, enemyY);
                        break;
                }
            }
        }

        char GetTileAt(int x, int y)
        {
            return mapData[y, x].ToCharArray()[0];
        }

        void SetTile(int x, int y, char tile)
        {
            mapData[y, x] = tile.ToString();
        }
    }
}
