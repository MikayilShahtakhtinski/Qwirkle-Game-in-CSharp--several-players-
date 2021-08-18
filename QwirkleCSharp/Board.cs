using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QwirkleCSharp
{
    class Board
    {
        private int b_row;
        private int b_col;
        private Cell[,] tiles;

        public int B_row { get => b_row; set => b_row = value; }
        public int B_col { get => b_col; set => b_col = value; }
        public Cell[,] Tiles { get => tiles; set => tiles = value; }

        public Board(int b_row,int b_col)
        {
            this.b_row = b_row;
            this.b_col = b_col;
            this.tiles = new Cell[b_row, b_col];
            B_initialize();
        }

        public void B_initialize()
        {

            for (int i = 0; i < this.b_row; i++)
            {

                for (int j = 0; j < this.b_col; j++)
                {
                    this.tiles[i, j] = new Cell(i, j, new Tile('—', ConsoleColor.White));
                }
            }
        }

        public void B_show()
        {
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < this.b_row; i++) { Console.Write(" " + i); }
            Console.WriteLine();
            for (int i = 0; i < this.b_row; i++)
            {
                Console.Write(i + " ");
                if (i < 10) { Console.Write(" "); }
                for (int j = 0; j < this.b_col; j++)
                {
                    this.tiles[i, j].Tile.T_print();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool B_isBelongToBoard(int cell_row, int cell_col)
        {
            if (cell_row < this.b_row && cell_col < this.b_col && cell_row >= 0 && cell_col >= 0)
            {
                return true;
            }
            return false;
        }

        public bool B_isEmptyCell(int cell_row, int cell_col)
        {
            if (B_isBelongToBoard(cell_row, cell_col))
            {
                if (this.tiles[cell_row, cell_col].Tile.Letter == '—')
                {
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCell is occupied!");
                    Console.ResetColor();

                    return false;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOut of the board borders!");
            Console.ResetColor();
            return false;

        }

        public bool B_addToBoard(Cell cell,Player player)
        {
            if (B_isEmptyCell(cell.Row, cell.Col) && B_hasNeighbours(cell))
            {
                TileList new_tilelist = B_checkRow(cell);
                TileList new_tilelist2 = B_checkCol(cell);
                if (new_tilelist2.TL_rules() && new_tilelist.TL_rules())
                {
                    this.tiles[cell.Row, cell.Col].Tile = cell.Tile;
                    player.P_calculateScore(new_tilelist, new_tilelist2);
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCan't place here!\n");
                    Console.ResetColor();
                    return false;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can't add!\n");
                Console.ResetColor();
            }
            return false;
        }

        public bool B_hasNeighbours(Cell cell)
        {
            if (B_isBelongToBoard(cell.Row - 1, cell.Col))
            {
                if (this.tiles[cell.Row - 1, cell.Col].Tile.Letter != '—')
                {
                    return true;
                }
            }
            if (B_isBelongToBoard(cell.Row + 1, cell.Col))
            {
                if (this.tiles[cell.Row + 1, cell.Col].Tile.Letter != '—')
                {
                    return true;
                }
            }
            if (B_isBelongToBoard(cell.Row, cell.Col+1))
            {
                if (this.tiles[cell.Row, cell.Col+1].Tile.Letter != '—')
                {
                    return true;
                }
            }
            if (B_isBelongToBoard(cell.Row, cell.Col-1))
            {
                if (this.tiles[cell.Row, cell.Col-1].Tile.Letter != '—')
                {
                    return true;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNo neighbours!");
            Console.ResetColor();
            return false;
        }

        public void B_addCenteredTile(TileList bag)
        {
            Random _random = new Random();
            int index = _random.Next(bag.Tilelist.Count);
            Tile removed_tile = bag.Tilelist.ElementAt(index);
            bag.Tilelist.Remove(removed_tile);
            this.tiles[5, 5] = new Cell(5, 5, removed_tile);
        }

        public TileList B_checkRow(Cell cell)
        {
            TileList new_tilelist = new TileList();
            new_tilelist.Tilelist.AddFirst(cell.Tile);
            for(int i = cell.Col-1;i>=0;i--)
            {
                if(this.tiles[cell.Row,i].Tile.Letter!= '—')
                {
                    new_tilelist.Tilelist.AddFirst(this.tiles[cell.Row, i].Tile);
                }
                else
                {
                    break;
                }
            }
            for (int j = cell.Col+1; j < b_col; j++)
            {
                if (this.tiles[cell.Row, j].Tile.Letter != '—')
                {
                    new_tilelist.Tilelist.AddLast(this.tiles[cell.Row, j].Tile);
                }
                else
                {
                    break;
                }
            }
            return new_tilelist;
        }
        public TileList B_checkCol(Cell cell)
        {
            TileList new_tilelist = new TileList();
            new_tilelist.Tilelist.AddFirst(cell.Tile);
            for (int i = cell.Row - 1; i >= 0; i--)
            {
                if (this.tiles[i, cell.Col].Tile.Letter != '—')
                {
                    new_tilelist.Tilelist.AddFirst(this.tiles[i, cell.Col].Tile);
                }
                else
                {
                    break;
                }
            }
            for (int j = cell.Row + 1; j < b_row; j++)
            {
                if (this.tiles[j, cell.Col].Tile.Letter != '—')
                {
                    new_tilelist.Tilelist.AddLast(this.tiles[j, cell.Col].Tile);
                }
                else
                {
                    break;
                }
            }
            return new_tilelist;
        }


    }
}
