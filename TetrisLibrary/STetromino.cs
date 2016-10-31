﻿using System.Collections.ObjectModel;

namespace WinFormTetris
{
    public class STetromino : Tetromino
    {
        private bool isUpright = true;
        private bool isRight = false;
        private bool isLeft = false;
        private bool isDown = false;

        public STetromino(TetrisBoard tetrisBoard, Collection<Tetromino> tetrominosOnScreen) : base(tetrisBoard, tetrominosOnScreen)
        {
            this.Blocks.Add(new TetrisBlock(5, 0));
            this.Blocks.Add(new TetrisBlock(4, 0));
            this.Blocks.Add(new TetrisBlock(4, 1));
            this.Blocks.Add(new TetrisBlock(3, 1));
        }

        protected override void RotateCounterClockwiseCore()
        {
            if (isUpright)
            #region
            {
                Blocks[0].Column -= 2;
                Blocks[1].Column -= 1;
                Blocks[1].Row += 1;
                Blocks[3].Column += 1;
                Blocks[3].Row += 1;
                isUpright = false;
                isLeft = true;
            }

            else if (isLeft)
            {
                Blocks[0].Row += 2;
                Blocks[1].Column += 1;
                Blocks[1].Row += 1;
                Blocks[3].Column += 1;
                Blocks[3].Row -= 1;
                isLeft = false;
                isDown = true;
            }

            else if (isDown)
            {
                Blocks[0].Column += 2;
                Blocks[1].Column += 1;
                Blocks[1].Row -= 1;
                Blocks[3].Column -= 1;
                Blocks[3].Row -= 1;
                isDown = false;
                isRight = true;
            }

            else if (isRight)
            {
                Blocks[0].Row -= 2;
                Blocks[1].Column -= 1;
                Blocks[1].Row -= 1;
                Blocks[3].Column -= 1;
                Blocks[3].Row += 1;
                isRight = false;
                isUpright = true;
            }
            #endregion
        }

        protected override void RotateClockwiseCore()
        {
            if (isUpright)
            #region
            {
                Blocks[0].Row += 2;
                Blocks[1].Column += 1;
                Blocks[1].Row += 1;
                Blocks[3].Column += 1;
                Blocks[3].Row -= 1;
                isUpright = false;
                isRight = true;
            }

            else if (isLeft)
            {
                Blocks[0].Column += 2;
                Blocks[1].Column += 1;
                Blocks[1].Row -= 1;
                Blocks[3].Column -= 1;
                Blocks[3].Row -= 1;
                isLeft = false;
                isUpright = true;
            }

            else if (isDown)
            {
                Blocks[0].Row -= 2;
                Blocks[1].Column -= 1;
                Blocks[1].Row -= 1;
                Blocks[3].Column -= 1;
                Blocks[3].Row += 1;
                isDown = false;
                isLeft = true;
            }

            else if (isRight)
            {
                Blocks[0].Column -= 2;
                Blocks[1].Column -= 1;
                Blocks[1].Row += 1;
                Blocks[3].Column += 1;
                Blocks[3].Row += 1;
                isRight = false;
                isDown = true;
            }
            #endregion
        }
    }
}
