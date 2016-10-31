using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using WinFormTetris;

namespace WpfTetris
{
    class WpfTetrisBoard : TetrisBoard
    {
        private DispatcherTimer timer = new DispatcherTimer();

        public WpfTetrisBoard(object drawingContext) : base(drawingContext)
        {
            
        }

        protected override void ClearCanvasCore()
        {
            Canvas playArea = (Canvas)DrawingContext;
            playArea.Children.Clear();
        }

        protected override void DrawCore(int column, int row, System.Drawing.Color color)
        {
            Canvas playArea = (Canvas)DrawingContext;
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
            Canvas.SetTop(rect, row * 50);
            Canvas.SetLeft(rect, column * 50);
            rect.Height = 48;
            rect.Width = 48;
            rect.Stroke = System.Windows.Media.Brushes.White;
            rect.StrokeThickness = 1;
            rect.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            playArea.Children.Add(rect);
        }

        protected override void RedrawBoardCore()
        {
            base.RedrawBoardCore();
        }

        protected override void StartTimerCore()
        {
            timer.Interval = TimeSpan.FromMilliseconds(DropTimeInMilliseconds);
            timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerTick();
        }

        protected override void StopTimerCore()
        {
            timer.Stop();
        }

    }
}
