using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_01
{
    internal class EmojiList
    {
        private readonly Random _random = new Random();

        private readonly List<string> _emoji = new List<string>
        {
            "😼","🙊","\U0001f989","\U0001f99a",
            "🐤","🐌","🕷️","🐶",
            "🐺","🦁","🐯","\U0001f992",
            "\U0001f98a","\U0001f99d","🐮","🐷",
            "🐗","🐭","🐰","🐻",
            "🐨","🐸","🦄","🐔",
            "🐍","\U0001f988","🐳","🦀",
            "\U0001f986",
        };

        private readonly int _pairsTotal;

        private List<string> _elements = new List<string>();

        public string NextOne
        {
            get
            {
                if (_elements.Count == 0)
                {
                    throw new IndexOutOfRangeException("No emoji left in list");
                }
                var nextOne = _elements[0];
                _elements.RemoveAt(0);
                return nextOne;
            }
        }

        public EmojiList(int pairsTotal)
        {
            if (pairsTotal >= _emoji.Count)
            {
                throw new ArgumentException($"pairsTotal cannot be grater than {_emoji.Count}");
            }

            _pairsTotal = pairsTotal;
            FillPairs();
        }

        private void FillPairs()
        {
            var sortedEmoji = new List<string>();

            for (var i = 0; i < _pairsTotal; i++)
            {
                int emojiIndex = _random.Next(_emoji.Count);
                string nextEmoji = _emoji[emojiIndex];
                sortedEmoji.Add(nextEmoji);
                sortedEmoji.Add(nextEmoji);
                _emoji.RemoveAt(emojiIndex);
            }

            while (sortedEmoji.Count > 0) 
            {
                int emojiIndex = _random.Next(sortedEmoji.Count);
                string nextEmoji = sortedEmoji[emojiIndex];
                _elements.Add(nextEmoji);
                sortedEmoji.RemoveAt(emojiIndex);
            }
        }
    }
}
