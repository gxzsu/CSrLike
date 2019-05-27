using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSrLike
{
    class GameSystem
    {
        public GameSystem()
        {

        }

        public void GameLoop(Level level, Player player)
        {
            bool isDone = false;

            while (!isDone)
            {
                level.Print();

                PlayerMove(level, player);

                level.UpdateEnemies(player);

                Console.Clear();
            }
        }

        void PlayerMove(Level level, Player player)
        {
            Console.WriteLine(player.GetStatsToDisplay());

            if (player.GetHealth() > 0)
            {
                Console.Write(LanguageEngine.langInUse[LanguageEngine.MovementCommands]);

                level.MovePlayer(Console.ReadKey().KeyChar, player);
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(LanguageEngine.langInUse[LanguageEngine.DeathNotification]);
                Console.ReadKey();
            }
        }
    }
}
