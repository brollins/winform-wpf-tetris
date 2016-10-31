﻿using System.Windows;
using System.Windows.Input;
using WinFormTetris;

namespace WpfTetris
{
    public partial class MainWindow : Window
    {
        private WpfTetrisBoard tetrisBoard;

        public MainWindow()
        {
            InitializeComponent();
            tetrisBoard = new WpfTetrisBoard(playArea);
            scoreBox.DataContext = tetrisBoard;
            tetrisBoard.StartGame();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                tetrisBoard.MoveRight();
            }

            if (e.Key == Key.Left)
            {
                tetrisBoard.MoveLeft();
            }

            if (e.Key == Key.Down)
            {
                tetrisBoard.MoveDown();
            }

            if (e.Key == Key.A)
            {
                tetrisBoard.RotateCounterClockwise();
            }

            if (e.Key == Key.D)
            {
                tetrisBoard.RotateClockwise();
            }
        }
    }
}