using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace WinFormTetris
{
    public class TetrisBoard : INotifyPropertyChanged
    {
        protected Collection<Tetromino> tetrominosOnScreen;
        private Queue<Tetromino> tetrominoQueue;
        public event PropertyChangedEventHandler PropertyChanged;
        private static Random random = new Random();
        private Tetromino currentTetromino;
        private object drawingContext;
        private int previewOffset = 4;
        private int clearLocation = 200;
        private int topRow = 0;
        private int bottomRow = 19;
        private int leftMostColumn = 0;
        private int rightMostColumn = 9;
        private int score;
        private int dropTimeInMilliseconds = 1250;

        public TetrisBoard() : this(null)
        {

        }

        public TetrisBoard(object drawingContext)
        {
            this.drawingContext = drawingContext;
        }

        public object DrawingContext
        {
            get
            {
                return drawingContext;
            }

            set
            {
                drawingContext = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }

        public Tetromino CurrentTetromino
        {
            get
            {
                return currentTetromino;
            }

            set
            {
                currentTetromino = value;
            }
        }

        public int TopRow
        {
            get
            {
                return topRow;
            }
        }

        public int BottomRow
        {
            get
            {
                return bottomRow;
            }
        }

        public int LeftMostColumn
        {
            get
            {
                return leftMostColumn;
            }
        }

        public int RightMostColumn
        {
            get
            {
                return rightMostColumn;
            }
        }

        public int DropTimeInMilliseconds
        {
            get
            {
                return dropTimeInMilliseconds;
            }

            set
            {
                dropTimeInMilliseconds = value;
            }
        }

        public void StartGame()
        {
            tetrominosOnScreen = new Collection<Tetromino>();
            tetrominoQueue = new Queue<Tetromino>();
            currentTetromino = null;
            Score = 0;


            DropNewTetromino();

            StartTimer();

            RedrawBoard();
        }

        public void StartTimer()
        {
            StartTimerCore();

        }

        public void StopTimer()
        {
            StopTimerCore();
        }

        protected virtual void StartTimerCore()
        {

        }

        protected virtual void StopTimerCore()
        {

        }

        public void TimerTick()
        {

            if (CurrentTetromino.IsAtBottom())
            {
                tetrominosOnScreen.Add(CurrentTetromino);
                ClearCompletedLines();
                DropNewTetromino();
                RedrawBoard();
            }
            else
            {
                if (!IsColliding())
                {
                    Drop();
                    ClearCompletedLines();
                }
            }
        }

        private void DropNewTetromino()
        {
            bool gameOver = false;
            if (CurrentTetromino == null)
            {
                tetrominoQueue.Enqueue(GetRandomTetromino());
                CurrentTetromino = GetRandomTetromino();
                CurrentTetromino.Draw();
            }

            else
            {
                CurrentTetromino = tetrominoQueue.Dequeue();
                CurrentTetromino.Draw();

                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var block in tetrominoOnScreen.Blocks)
                    {
                        foreach (var currentblock in CurrentTetromino.Blocks)
                        {
                            if (block.Column == currentblock.Column && block.Row == currentblock.Row)
                            {
                                gameOver = true;
                                StopTimer();
                            }
                        }
                    }
                }

                if (gameOver)
                {
                    StopTimer();
                    //if (MessageBox.Show("Would you like to play again?", "Game Over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    StartGame();
                    //}
                    //else
                    //{
                    //    StopTimer();
                    //}
                }

                else
                {
                    tetrominoQueue.Enqueue(GetRandomTetromino());
                }
            }
        }

        public Tetromino GetRandomTetromino()
        {
            int randomNumber = random.Next(1, 8);
            Tetromino randomTetromino = null;
            switch (randomNumber)
            {
                case 1:
                    randomTetromino = new ITetromino(this, tetrominosOnScreen);
                    break;
                case 2:
                    randomTetromino = new JTetromino(this, tetrominosOnScreen);
                    break;
                case 3:
                    randomTetromino = new OTetromino(this, tetrominosOnScreen);
                    break;
                case 4:
                    randomTetromino = new ZTetromino(this, tetrominosOnScreen);
                    break;
                case 5:
                    randomTetromino = new STetromino(this, tetrominosOnScreen);
                    break;
                case 6:
                    randomTetromino = new LTetromino(this, tetrominosOnScreen);
                    break;
                case 7:
                    randomTetromino = new TTetromino(this, tetrominosOnScreen);
                    break;
                default:
                    randomTetromino = new ITetromino(this, tetrominosOnScreen);
                    break;
            }
            return randomTetromino;
        }

        public bool IsColliding()
        {
            bool isColliding = false;
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                {
                    foreach (var block in CurrentTetromino.Blocks)
                    {
                        if (block.Row + 1 == blockOnScreen.Row && block.Column == blockOnScreen.Column)
                        {
                            isColliding = true;
                        }
                    }
                    if (blockOnScreen.Row == TopRow)
                    {
                        isColliding = true;
                    }
                }
            }
            return isColliding;
        }

        public void Draw(int column, int row, Color color)
        {
            DrawCore(column, row, color);
        }

        protected virtual void DrawCore(int column, int row, Color color)
        {

        }

        public void Drop()
        {
            currentTetromino.Drop();
            RedrawBoard();
        }

        public void MoveLeft()
        {
            currentTetromino.MoveLeft();
            RedrawBoard();
        }

        public void MoveRight()
        {
            currentTetromino.MoveRight();
            RedrawBoard();
        }

        public void MoveDown()
        {
            currentTetromino.MoveDown();
            RedrawBoard();
        }

        public void RotateCounterClockwise()
        {
            currentTetromino.RotateCounterClockwise();
            RedrawBoard();
        }

        public void RotateClockwise()
        {
            currentTetromino.RotateClockwise();
            RedrawBoard();
        }

        public void RedrawBoard()
        {
            RedrawBoardCore();
        }

        protected virtual void RedrawBoardCore()
        {
            ClearCanvas();
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                tetrominoOnScreen.Draw();
            }
            currentTetromino.Draw();

            if (tetrominoQueue.Count > 0)
            {
                Tetromino nextTetromino = tetrominoQueue.Peek();
                foreach (var block in nextTetromino.Blocks)
                {
                    block.Column += previewOffset;
                    block.Draw(this, Color.FromArgb(75, nextTetromino.Color));
                    block.Column -= previewOffset;
                }
            }
        }

        public void ClearCanvas()
        {
            ClearCanvasCore();
        }

        protected virtual void ClearCanvasCore()
        {
        }

        private void ClearCompletedLines()
        {
            Collection<TetrisBlock> blocksInALine = new Collection<TetrisBlock>();
            for (int i = bottomRow + 1; i > 0; i -= 1)
            {
                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                    {
                        if (blockOnScreen.Row == i)
                        {
                            blocksInALine.Add(blockOnScreen);
                        }
                    }
                }

                if (blocksInALine.Count == 10)
                {
                    foreach (var block in blocksInALine)
                    {
                        block.Column = clearLocation;
                        block.Row = clearLocation;
                    }

                    Score += 1000;
                    int level = Score / 5000;
                    DropTimeInMilliseconds = (1250 - (level * 10));

                    foreach (var tetrominoOnScreen in tetrominosOnScreen)
                    {
                        foreach (var block in tetrominoOnScreen.Blocks)
                        {
                            if (block.Row <= i)
                            {
                                block.MoveDown();
                            }
                        }
                    }
                }
                blocksInALine.Clear();
            }
        }
    }
}
