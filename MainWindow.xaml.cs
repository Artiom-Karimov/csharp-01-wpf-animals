using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace csharp_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int pairsFound = 0;
        private readonly int pairsTotal = 8;

        private TextBlock lastTextBlockClicked;
        private bool findingMatch = false;

        private Random random = new Random();

        private readonly GameTimer timer = new GameTimer();

        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, GameTimerEventArgs e)
        {
            var elapsed = e.TenthsOfSecondsElapsed;
            timerTicksDisplay.Text = (elapsed / 10F).ToString("0.0s");
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
            timerTicksDisplay.Text += ". Play again?";
        }

        private void timerTicksDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetUpGame();
            timer.Start();
        }
    }
}