using System;
using System.Collections.Generic;
using System.Text;

namespace QwirkleCSharp
{
    class Cell
    {
        private int col;
        private int row;
        private Tile tile;

        public int Col { get => col ; set => col = value; }
        public int Row { get => row; set => row = value; }
        public Tile Tile { get => tile; set => tile = value; }

        public Cell(int row,int col,Tile tile)
        {
            this.row = row;
            this.col = col;
            this.tile = tile;
        }

    }
}
