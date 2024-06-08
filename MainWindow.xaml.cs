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

namespace csharp_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
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

            var random = new Random();

            foreach (var block in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                block.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }
    }
}