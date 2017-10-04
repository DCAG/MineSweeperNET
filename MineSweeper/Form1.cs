using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    enum LotContent{
        Mine  = -1,
        Empty = 0,
        Num_1 = 1,
        Num_2 = 2,
        Num_3 = 3,
        Num_4 = 4,
        Num_5 = 5,
        Num_6 = 6,
        Num_7 = 7,
        Num_8 = 8
    }

    enum GameState
    {
        Lose = -1,
        Continue = 0,
        Win = 1
    }

    public partial class Form1 : Form
    {
        LotContent[,] lots = new LotContent[8,8];
        int numberOfMines = 10;

        GameState gameState;

        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateMinesBoard()
        {
            PlaceMines();
            CalculateLotsNumbers();
        }
        private void PlaceMines()
        {
            int minesLeftToPlace = this.numberOfMines;
            Random r = new Random();
            for (int i = 0; i < lots.Length & 0 < minesLeftToPlace; i++)
            {
                if (r.NextDouble() < CalculateMineDrop(minesLeftToPlace / (double)numberOfMines, i / (double)lots.Length))
                {
                    lots.SetValue(LotContent.Mine, i);
                    minesLeftToPlace--;
                }
            }
        }

        private double CalculateMineDrop(double P_MinesLeft, double P_LotsPassed)
        {
            return P_MinesLeft * P_LotsPassed;
        }

        private void CalculateLotsNumbers()
        {
            for (int i = 0; i < lots.GetLength(0); i++)
            {
                for (int j = 0; j < lots.GetLength(1); j++)
                {
                    lots[i, j] = CalculateLotValue(i, j); 
                }
            }
        }

        private LotContent CalculateLotValue(int i, int j)
        {
            return (LotContent) (isMineNextToMe(1, 1) +
            isMineNextToMe(1, 0)   +
            isMineNextToMe(1, -1)  +
            isMineNextToMe(0, -1)  +
            isMineNextToMe(-1, -1) +
            isMineNextToMe(-1, 0)  +
            isMineNextToMe(-1, 1)  +
            isMineNextToMe(0, 1));
        }

        private int isMineNextToMe(int v1, int v2)
        {
            return lots[v1, v2] == LotContent.Mine ? 1 : 0;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            GenerateMinesBoard();
            StartTimer();
            this.gameState = GameState.Continue;
        }

        private object Turn(int index)
        {
            if(this.gameState != GameState.Continue)
            {
                throw new Exception("This is a game over");
            }

            switch ((LotContent)lots.GetValue(index))       
            {
                case LotContent.Mine:
                    gameState = GameState.Lose;
                    return index;
                    break;
                case LotContent.Empty:
                    gameState = GameState.Continue;
                    return GetSafeZone();
                    break;
                case LotContent.Num_1:
                case LotContent.Num_2:
                case LotContent.Num_3:
                case LotContent.Num_4:
                case LotContent.Num_5:
                case LotContent.Num_6:
                case LotContent.Num_7:
                case LotContent.Num_8:
                default:
                    gameState = GameState.Continue;
                    return index;
                    break;                    
            }
        }

        private int[,] GetSafeZone()
        {
            // TODO: Get a list of all coordinates of empty lots and the border of lots with numbers.
        }

        private GameState Turn(int index1, int index2)
        {
            if (this.gameState != GameState.Continue)
            {
                throw "This is a game over";
            }
        }

        private void StartTimer()
        {
            
        }
    }
}
