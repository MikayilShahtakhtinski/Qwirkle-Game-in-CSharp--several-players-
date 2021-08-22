using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QwirkleCSharp
{
    class Game
    {
        Player[] players;
        TileList bag;
        Board board;

        public Player[] Players { get => players; set => players=value; }
        public TileList Bag { get => bag; set => bag=value; }

        public Game() 
        {
            this.bag = new TileList(2);
            this.board = new Board(11, 11);
            this.board.B_initialize();
            this.board.B_addCenteredTile(this.bag);
            int numPlayers = getCountPlayers();
            players = new Player[numPlayers];
            G_getPlayer();
        }

        public void G_getPlayer()
        {
            Console.WriteLine();
            for(int i =0; i < players.Length; i++)
            {
                Console.Write("Enter a name of the player {0}: ",i+1);
                string name = Console.ReadLine();
                players[i] = new Player(name);
                this.bag.TL_addDeck(players[i].Deck, 6);
            }
        }

        public void G_displayAll()
        {
            Console.WriteLine();
            this.board.B_show();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nThere are {0} tiles left in the bag.\n", bag.Tilelist.Count);
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < players.Length; i++)
            {
                players[i].P_show();
            }
            Console.ResetColor();
            Console.WriteLine();

        }


        public void Q_showHand(int index)
        {
            Console.WriteLine("Player {0} hand: ", index+1);
            players[index].Deck.TL_show();
            Console.WriteLine();
        }


        public int getCountPlayers()
        {
            string count;
            while (true)
            {
                Console.Write("How many players want to play? ");
                count = Console.ReadLine();
                if (!int.TryParse(count, out int number))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter only a number!\n");
                    Console.ResetColor();
                    continue;
                }
                else if(int.Parse(count) <= 0 || int.Parse(count) > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe maximum number of players is 4!");
                    Console.WriteLine("Please enter the postive number greater than zero and less that 5!.\n");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    break;
                }
            }
            /*
            while(!int.TryParse(count,out int number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPlease enter only a number!\n");
                Console.ResetColor();
                Console.Write("How many players want to play? ");
                count = Console.ReadLine();
            }
            int count2 = int.Parse(count);
            while (count2 <= 0 )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The maximum number of player is 4!");
                Console.WriteLine("\nPlease enter the postive number greater that zero!.");
            }
            */
            return int.Parse(count);
        }

        public bool ValidInput(string input, int[] output)
        {
            string[] inputs = input.Split(' ');
            if (inputs.Length != 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nMore or less than 3 inputs! Only 3 inputs must be written\n");
                Console.ResetColor();
                return false;
            }
            foreach (string str in inputs)
            {
                if (!int.TryParse(str, out int number))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter valid inputs(Only integers, and only 3 numbers\n!");
                    Console.ResetColor();
                    return false;
                }
            }
            for (int i = 0; i < inputs.Length; i++)
            {
                output[i] = int.Parse(inputs[i]);
            }
            return true;
        }

        public void Q_oneTurn(int index)
        {
            int tile_index = -1, row = -1, col = -1;
            int saved_col=-1, saved_row=-1;
            bool turn = false;
            bool workInRow = false;
            bool workInCol = false;
            while (!turn)
            {                
                Console.WriteLine("{0}, your deck: ", players[index].Name);
                players[index].Deck.TL_show();
                Console.WriteLine("\n");
                Console.WriteLine("Dear {0} chose one tile on the deck and its position on the board",players[index].Name);
                Console.Write("In this order : tile_index, row, column (type 's' if want to skip) : ");
                string input = Console.ReadLine();
                if (input == "s")
                {
                    turn = true;
                    continue;
                }
                int[] output = new int[3];
                if (ValidInput(input, output))
                {
                    tile_index = output[0];
                    row = output[1];
                    col = output[2];
                }
                else { continue; }

                if (tile_index < this.players[index].Deck.Tilelist.Count && tile_index >= 0)
                {
                    Cell new_cell = new Cell(row, col, this.players[index].Deck.Tilelist.ElementAt(tile_index));
                   

                    if (this.board.B_addToBoard(new_cell, players[index], saved_row, saved_col))
                    {
                        if (!workInRow)
                        {
                            if (!board.B_isEmptyCell(row, col + 1, false) || !board.B_isEmptyCell(row, col - 1, false))
                            {
                                saved_row = row;
                                workInRow = true;
                                workInCol = true;
                            }
                        }
                        if (!workInCol)
                        {
                            if (!board.B_isEmptyCell(row + 1, col, false) || !board.B_isEmptyCell(row - 1, col, false))
                            {
                                saved_col = col;
                                workInRow = true;
                                workInCol = true;
                            }
                        }
                        this.players[index].Deck.Tilelist.Remove(new_cell.Tile);
                        if (bag.Tilelist.Count != 0)
                        {
                            bag.TL_addDeck(this.players[index].Deck, 1);
                        }
                        /* turn = true;*/
                        G_displayAll();
                    }
                    else { continue; }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter valid index_col!\n");
                    Console.ResetColor();
                    continue;
                }
                if (Q_gameOver())
                {
                    break;
                }

            }
        }

        public bool Q_gameOver()
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (players[i].Deck.Tilelist.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Q_congratulate()
        {
            int[] scores = new int[players.Length];
            int count = 0;
            for(int i = 0; i < players.Length; i++)
            {
                scores[i] = players[i].Score;
            }
            int index = findMaxElementArray(scores);
            int num = scores[index];
            for(int i = 0; i < players.Length; i++)
            {
                if (scores[i] == num)
                {
                    count++;
                }
            }
            if (count >= 2)
            {
                Console.WriteLine("\nThere is no winner! Draw!\n");
                return;
            }

            Console.WriteLine("\nCongratualiton {0}, you won. Well done!", this.players[index].Name);
            players[index].P_show();
        }

        public int findMaxElementArray(int[] arr)
        {
            int max = arr[0];
            int index = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
