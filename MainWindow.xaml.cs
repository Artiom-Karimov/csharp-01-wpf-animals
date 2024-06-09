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

        private readonly GameTimer timer = new GameTimer();

        private EmojiList emojiList;

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
            emojiList = new EmojiList(pairsTotal);
            pairsFound = 0;

            foreach (var block in mainGrid.Children.OfType<TextBlock>())
            {
                if (block.Name.Equals("timerTicksDisplay")) continue;
                block.Text = emojiList.NextOne;
                block.Visibility = Visibility.Visible;
            }
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
            EndGameIfNeeded();
        }

        private void EndGameIfNeeded()
        {
            if (pairsFound < pairsTotal) return;

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