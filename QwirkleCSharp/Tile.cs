using System;
using System.Collections.Generic;
using System.Text;

namespace QwirkleCSharp
{
    class Tile
    {
        private ConsoleColor color;
        private char letter;

        public ConsoleColor Color { get => color; set => color = value; }
        public char Letter { get => letter; set => letter=value; }

        public Tile(char letter,ConsoleColor color)
        {
            this.letter = letter;
            this.color = color;
        }

        public void T_print()
        {
            Console.ForegroundColor = this.color;
            Console.Write(letter + " ");
            Console.ResetColor();
        }
    }
}
