using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklyarov.Nsudotnet.TicTacToe
{
    class ConsoleGUI : IGUI
    {
        public static readonly int horizontalOffset = 4;
        public static readonly int verticalOffset = 2;

        public void Init()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("    1   2   3   4   5   6   7   8   9 ");
            Console.WriteLine("  ╔═══╤═══╤═══╦═══╤═══╤═══╦═══╤═══╤═══╗");
            Console.WriteLine("1 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            Console.WriteLine("2 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            Console.WriteLine("3 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
            Console.WriteLine("4 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            Console.WriteLine("5 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            Console.WriteLine("6 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
            Console.WriteLine("7 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            Console.WriteLine("8 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            Console.WriteLine("9 ║   │   │   ║   │   │   ║   │   │   ║");
            Console.WriteLine("  ╚═══╧═══╧═══╩═══╧═══╧═══╩═══╧═══╧═══╝");
            Console.WriteLine("Player: Row Column. Example: \"X: 5 5\"");
            Console.WriteLine(" : ");
        }
        public void UpdatePlayer(State player)
        {
            SetMessagePosition();
            Console.Write(PlayerToString(player));
        }
        public void PrintResult(State player)
        {
            if (player == State.Draw)
            {
                SetMessagePosition();
                Console.WriteLine("DRAW!");
                Console.ReadLine();
            }
            else
            {
                SetMessagePosition();
                Console.WriteLine("{0} WINS!", PlayerToString(player));
                Console.ReadLine();
            }
        }
        public void UpdateCell(int x, int y, State player)
        {
            Console.SetCursorPosition(horizontalOffset * (y + 1), verticalOffset * (x + 1));
            Console.Write(PlayerToString(player));
        }
        public string GetNextStep()
        {
            SetMessagePosition(_messageColumn);

            String result;
            result = Console.ReadLine();

            SetMessagePosition(_messageColumn);
            for (int i = 0; i < result.Length; ++i)
            {
                Console.Write(" ");
            }

            return result;
        }



        private static readonly int _messageLine = 21;
        private static readonly int _messageColumn = 3;

        private void SetMessagePosition(int position = 0)
        {
            Console.SetCursorPosition(position, _messageLine);
        }
        private string PlayerToString(State player)
        {
            return (player == State.X) ? "X" : "O";
        }
    }
}
