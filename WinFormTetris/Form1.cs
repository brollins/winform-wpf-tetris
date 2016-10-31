using System;
using System.Windows.Forms;

namespace WinFormTetris
{
    public partial class Form1 : Form
    {
        private WinFormsTetrisBoard tetrisBoard;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tetrisBoard = new WinFormsTetrisBoard(playArea);
            tetrisBoard.StartGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                tetrisBoard.MoveRight();
            }
            if (e.KeyCode == Keys.Left)
            {
                tetrisBoard.MoveLeft();
            }

            if (e.KeyCode == Keys.Down)
            {
                tetrisBoard.MoveDown();
            }

            if (e.KeyCode == Keys.A)
            {
                tetrisBoard.RotateCounterClockwise();
            }

            if (e.KeyCode == Keys.D)
            {
                tetrisBoard.RotateClockwise();
            }
        }
    }
}
