using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QwirkleCSharp
{
    class TileList
    {
        private LinkedList<Tile> tilelist;

        public LinkedList<Tile> Tilelist { get => tilelist; set => tilelist=value; }

        public TileList()
        {
            this.tilelist = new LinkedList<Tile>(); 
        }

        public TileList(int sample)
        {
            TL_fillTiles(sample);
        }

        public void TL_fillTiles(int n)
        {
            this.tilelist = new LinkedList<Tile>();
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.DarkYellow };
            char[] characters = { 'A', 'B', 'C', 'D' };
            for (int i = 0; i < 16 * n; i++)
            {
                this.tilelist.AddLast(new Tile(characters[i%4], colors[i/4 % 4]));             
            }
        }

        public void TL_show()
        {
            if (this.tilelist.Count == 0)
            {
                Console.WriteLine("Nothing to show, deck is empty!");
            }
            foreach (Tile tile in this.tilelist)
            {
                tile.T_print();
            }
        }

        public void TL_addDeck(TileList deck, int n)
        {
            Random _random = new Random();
            for (int i = 0; i < n; i++)
            {
                if (this.tilelist.Count == 0)
                {
                    return;
                }
                int index = _random.Next(this.tilelist.Count);
                Tile removed_tile = this.tilelist.ElementAt(index);
                this.tilelist.Remove(removed_tile);
                deck.tilelist.AddLast(removed_tile);
            }
        }

        public bool TL_rules()
        {
            if (this.tilelist.Count > 4)
            {
                return false;
            }
            if (this.tilelist.Count == 1)
            {
                return true;
            }
            bool samecolor = true;
            bool sameletter = true;
            for(int i = 0; i < this.tilelist.Count-1; i++)
            {
                for(int j = i+1; j < this.tilelist.Count; j++)
                {
                    if(this.tilelist.ElementAt(i).Color == this.tilelist.ElementAt(j).Color && this.tilelist.ElementAt(i).Letter == this.tilelist.ElementAt(j).Letter)
                    {
                        return false;
                    }
                    if (this.tilelist.ElementAt(i).Color != this.tilelist.ElementAt(j).Color)
                    {
                        samecolor = false;
                    }
                    if (this.tilelist.ElementAt(i).Letter != this.tilelist.ElementAt(j).Letter)
                    {
                        sameletter = false;
                    }
                }             
            }
            return samecolor != sameletter;
        }
    }
}
