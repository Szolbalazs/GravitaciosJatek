using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gravitacio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int sebesseg = 10; 

        int eses = 10; 

        bool goLeft, goRight; 
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick; 
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20); 
            dispatcherTimer.Start();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            else if (e.Key == Key.Right)
            {
                goRight = true;
            }
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            else if (e.Key == Key.Right)
            {
                goRight = false;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Canvas.SetTop(rec1, Canvas.GetTop(rec1) + eses);

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if (x.Tag != null)
                {
                    var platformID = (string)x.Tag;

                    if (platformID == "platform")
                    {

                        x.Stroke = Brushes.Black;

                        Rect player = new Rect(Canvas.GetLeft(rec1), Canvas.GetTop(rec1), rec1.Width, rec1.Height);
                        Rect platforms = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (player.IntersectsWith(platforms))
                        {
                            eses = 0;
                            Canvas.SetTop(rec1, Canvas.GetTop(x) - rec1.Height);

                            infoTxt.Content = x.Name.ToString();
                        }
                        else
                        {
                            eses = 10;
                        }
                    }
                }
            }

            if (Canvas.GetTop(rec1) + (rec1.Height * 2) > Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(rec1, -50);
            }

            if (goLeft && Canvas.GetLeft(rec1) > 15)
            {
                Canvas.SetLeft(rec1, Canvas.GetLeft(rec1) - sebesseg);
            }

            if (goRight && Canvas.GetLeft(rec1) + (rec1.Width + 10) < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(rec1, Canvas.GetLeft(rec1) + sebesseg);
            }
        }
    }
}
