using System.Drawing;
using System.Windows.Forms;

namespace WinFormTetris
{
    class WinFormsTetrisBoard : TetrisBoard
    {
        private Timer timer = new Timer();
        private PictureBox pictureBox;
        private Graphics graphics;

        public WinFormsTetrisBoard(object drawingContext) : base(drawingContext)
        {
            pictureBox = (PictureBox)drawingContext;
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox.Image = bitmap;
        }

        protected override void ClearCanvasCore()
        {
            graphics.Clear(Color.Black);
        }

        protected override void DrawCore(int column, int row, Color color)
        {
            Pen whitePen = new Pen(Color.FromArgb(color.A, Color.White));
            SolidBrush myBrush = new SolidBrush(color);
            whitePen.Width = 7F;

            int width = 40;
            int height = 40;
            int x = column;
            int y = row;

            graphics.DrawRectangle(whitePen, x * 50 + 3, y * 50 + 3, width, height);
            graphics.FillRectangle(myBrush, x * 50 + 3, y * 50 + 3, width, height);
        }

        protected override void RedrawBoardCore()
        {
            base.RedrawBoardCore();
            pictureBox.Refresh();
        }

        protected override void StartTimerCore()
        {
            timer.Interval = DropTimeInMilliseconds;
            timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            TimerTick();
        }

        protected override void StopTimerCore()
        {
            timer.Stop();
        }
    }
}
