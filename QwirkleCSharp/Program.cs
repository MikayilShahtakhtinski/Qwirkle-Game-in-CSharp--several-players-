using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QwirkleCSharp
{
    class Program
    {
              
        static void Main(string[] args)
        {

            Game q = new Game();
            q.G_displayAll();
            while (!q.Q_gameOver())
            {
                for(int i = 0; i < q.Players.Length; i++) {
                    q.Q_oneTurn(i);
                    if (q.Q_gameOver())
                    {
                        break;
                    }
                    q.G_displayAll();
                }
            }

            q.Q_congratulate();



            /*
            string name,input;
            int samples = -1;
            Console.Write("Hello, what is your name? ");
            name = Console.ReadLine();
            samples = getSample(samples,name);

            TileList bag = new TileList(samples);
            TileList deck = new TileList();
            bag.TL_addDeck(deck, 6);
            

            Board new_board = new Board(11,11);
            new_board.B_initialize();
            // putting a tile to center
            new_board.B_addCenteredTile(bag);
            new_board.B_show();
            deck.TL_show();
            Console.WriteLine();

            Player p1 = new Player("Mikail Shah");
            p1.Deck = deck;
            

            while (deck.Tilelist.Count != 0)
            {
                Console.WriteLine("\nChoose one tile on the deck and its position on the board.");
                Console.Write("In this order : tile_index, row, column\t: ");

                input = Console.ReadLine();
                int[] output = new int[3];

                int tile_index,row,col;

                if (ValidInput(input, output))
                {
                    tile_index = output[0];
                    row = output[1];
                    col = output[2];
                }
                else { continue; }
                if (tile_index < deck.Tilelist.Count && tile_index>=0)
                {
                    Cell new_cell = new Cell(row, col, deck.Tilelist.ElementAt(tile_index));


                    if (new_board.B_addToBoard(new_cell,p1))
                    {
                     
                        deck.Tilelist.Remove(new_cell.Tile);
                        if (bag.Tilelist.Count != 0)
                        {
                            bag.TL_addDeck(deck, 1);
                            
                        }
                    }
                    else { continue; }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter valid index_col!");
                    Console.ResetColor();
                    continue; 
                }

                Console.WriteLine();
                new_board.B_show();
                deck.TL_show();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("\nThere are {0} tiles left in the bag.\n", bag.Tilelist.Count);
                Console.ResetColor();

            }

            
            */
        }
    }
}
