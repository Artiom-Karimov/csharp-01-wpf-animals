using Microsoft.Win32;
using System;
using System.Text;
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

namespace csharp_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int tenthsOfSecondsElapsed = 0;

        private int pairsFound = 0;
        private readonly int pairsTotal = 8;

        private TextBlock lastTextBlockClicked;
        private bool findingMatch = false;

        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
            StartTimer();
        }

        private void StartTimer() 
        {
            tenthsOfSecondsElapsed = 0;
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed += 1;
            timerTicksDisplay.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        }

        private void SetUpGame()
        {
            pairsFound = 0;

            var animalEmoji = new List<string>()
            {
                "🐺","🐺",
                "🐶","🐶",
                "🙊","🙊",
                "🦒","🦒",
                "🦊","🦊",
                "🦝","🦝",
                "🐮","🐮",
                "🐭","🐭",
            };

            foreach (var block in mainGrid.Children.OfType<TextBlock>())
            {
                InitailizeBlock(block, animalEmoji);
            }
        }

        private void InitailizeBlock(TextBlock block, List<string> animalEmoji)
        {
            if (!(block.Text.Equals("?") || block.Visibility == Visibility.Hidden)) return;
            int index = random.Next(animalEmoji.Count);
            string nextEmoji = animalEmoji[index];
            block.Text = nextEmoji;
            block.Visibility = Visibility.Visible;
            animalEmoji.RemoveAt(index);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (!(textBlock is TextBlock)) return;

            if (!findingMatch)
            {
                StartFindingMatch(textBlock);
                return;
            }

            CheckMatch(textBlock);
        }

        private void StartFindingMatch(TextBlock textBlock)
        {
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
        }

        private void CheckMatch(TextBlock textBlock)
        {
            if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                IncrementFoundCounter();
                findingMatch = false;
                return;
            }

            lastTextBlockClicked.Visibility = Visibility.Visible;
            findingMatch = false;
        }

        private void IncrementFoundCounter()
        {
            pairsFound++;
            if (pairsFound == pairsTotal)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            timer.Stop();
        }

        private void timerTicksDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartTimer();
            SetUpGame();
        }
    }
}