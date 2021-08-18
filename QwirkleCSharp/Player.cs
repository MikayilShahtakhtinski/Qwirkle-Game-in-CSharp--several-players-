using System;
using System.Collections.Generic;
using System.Text;

namespace QwirkleCSharp
{
    class Player
    {
        private string name;
        private int score;
        private TileList deck;

        public int Score { get => score; set => score = value; }
        public string Name { get => name; set => name = value; }
        public TileList Deck { get => deck; set => deck = value; }

        public Player(string name)
        {

            this.name = name;
            this.deck = new TileList();
            this.score = 0;
        }

        public void P_calculateScore(TileList tl1,TileList tl2)
        {
            if (tl1.Tilelist.Count > 1)
            {
                if (tl1.Tilelist.Count == 4)
                {
                    this.score += 4;
                }
                this.score += tl1.Tilelist.Count;
            }
            if (tl2.Tilelist.Count > 1)
            {
                if (tl2.Tilelist.Count == 4)
                {
                    this.score += 4;
                }
                this.score += tl2.Tilelist.Count;
            }
        }

        public void P_show()
        {
            Console.WriteLine("Score of {0}: {1}", this.name, this.score);
        }
    }
}
