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

namespace lineChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point[] myPoints;
        Rectangle[] myRectangles;
        Key PREV_KEY;
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            PREV_KEY = Key.D;
            //start Timer
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

            myPoints = new Point[4];
            myRectangles = new Rectangle[4];
            for (int i = 0; i < myPoints.Length; i++)
            {
                myPoints[i] = new Point(i * 50.0 + 50.0, 50.0);
                myRectangles[i] = new Rectangle();
                myRectangles[i].Fill = Brushes.Red;
                myRectangles[i].Width = 50;
                myRectangles[i].Height = 1;
                canvas.Children.Add(myRectangles[i]);
                Canvas.SetTop(myRectangles[i], myPoints[i].Y);
                Canvas.SetLeft(myRectangles[i], myPoints[i].X);
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Down) && PREV_KEY != Key.Down)
            {
                for (int i = 0; i < myPoints.Length; i++)
                {
                    myPoints[i].Y += 10;
                    Canvas.SetTop(myRectangles[i], myPoints[i].Y);
                    PREV_KEY = Key.Down;
                }
            }
            else if (Keyboard.IsKeyUp(Key.Down) && PREV_KEY == Key.Down)
            {
                PREV_KEY = Key.D;
            }
            if (Keyboard.IsKeyDown(Key.Left) && PREV_KEY != Key.Left)
            {
                Point centreOfRotation = myPoints[2];

                for (int i = 0; i < myPoints.Length; i++)
                {
                    //RotateTransform rtf = new RotateTransform(20.0, centreOfRotation.X, centreOfRotation.Y);
                    RotateTransform rtf = new RotateTransform(15, myPoints[i].X - centreOfRotation.X, myPoints[i].Y - centreOfRotation.Y);

                    Canvas.SetTop(myRectangles[i], Canvas.GetTop(myRectangles[i]) + i * 50);//fix with trig
                    //myRectangles[i].RenderTransformOrigin = centreOfRotation;

                    myRectangles[i].RenderTransform = rtf;
                    myPoints[i].Y = Canvas.GetTop(myRectangles[i]);
                    myPoints[i].X = Canvas.GetLeft(myRectangles[i]);
                    //Canvas.SetTop(myRectangles[i], myPoints[i].Y);
                    PREV_KEY = Key.Left;
                }
            }
            else if (Keyboard.IsKeyUp(Key.Left) && PREV_KEY == Key.Left)
            {
                PREV_KEY = Key.D;
            }
        }
    }
}
