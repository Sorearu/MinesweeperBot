using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace MinesweeperBot
{
    class Bot
    {
        static void Main(string[] args)
        {
            Mouse.MoveToAndClick(0, 0, MouseButton.None);
            WindowController.SetUpGameWindow();
            GameField msGame = GameField.Instance;

            msGame.Update();

            while(msGame.Health == Health.Alive)
            {
                Algorithm algorithm;
                switch (msGame.State)
                {
                    case State.Initial:
                        algorithm = new InitialAlgorithm();
                        break;
                    case State.Simple:
                        algorithm = new SimpleAlgorithm();
                        break;
                    case State.Complex:
                        algorithm = new ComplexAlgorithm();
                        break;
                    case State.Luck:
                        algorithm = new LuckyAlgorithm();
                        break;
                    default:
                        algorithm = new InitialAlgorithm();
                        break;                    
                }
                algorithm.Execute();

                Health health = msGame.Health;

                if(health == Health.Dead)
                {
                    msGame.ResetGame();
                }
        
            } 

        }

        public Bot() { }

    }
}
