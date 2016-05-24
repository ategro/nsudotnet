using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklyarov.Nsudotnet.TicTacToe
{
    class FieldAlternative
    {
        public void Run()
        {
            Print();
            PrintPlayer();

            while (true)
            {
                switch (NextStep())
                {
                    case tryAgainCode: continue;
                    case successfulTryCode: break;
                    case drawCode:
                        SetMessagePosition();
                        Console.WriteLine("DRAW!");
                        Console.ReadLine();
                        return;
                    case winCode:
                        SetMessagePosition();
                        Console.WriteLine("{0} WINS!", PlayerToString());
                        Console.ReadLine();
                        return;
                }

                PrintPlayer();
            }
        }

        private const int tryAgainCode = 0;
        private const int successfulTryCode = 1;
        private const int drawCode = 2;
        private const int winCode = 3;

        private static readonly int size = 3;
        private static readonly int posMin = 1;
        private static readonly int posMax = 9;
        private static readonly int horizontalOffset = 4;
        private static readonly int verticalOffset = 2;

        private State _player = State.X;
        private int _x, _y;
        private int _lastX = -1, _lastY = -1;
        private Field<Field<Cell>> _field = new Field<Field<Cell>>();

        private bool Set(int x, int y, State state)
        {
            if (_field[x / size, y / size][x % size, y % size].Value == State.Empty)
            {
                _field[x / size, y / size][x % size, y % size].Value = state;
                return true;
            }

            return false;
        }

        private void Print()
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

        private string PlayerToString()
        {
            return (_player == State.X) ? "X" : "O";
        }

        private void SetMessagePosition(int position = 0)
        {
            Console.SetCursorPosition(position, 21);
        }

        private void PrintCell(int x, int y)
        {
            Console.SetCursorPosition(horizontalOffset * (y + 1), verticalOffset * (x + 1));
            Console.Write(PlayerToString());
        }

        private void PrintPlayer()
        {
            SetMessagePosition();
            Console.Write(PlayerToString());
        }

        private bool isFull(int x, int y)
        {
            bool result = true;
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    result &= _field[x, y][i, j].Value != State.Empty;
                }
            }

            return result;
        }

        private int NextStep()
        {
            SetMessagePosition(3);

            String str;
            str = Console.ReadLine();

            SetMessagePosition(3);
            for (int i = 0; i < str.Length; ++i)
            {
                Console.Write(" ");
            }

            String[] strs = str.Split(new char[] { ' ' });
            if (strs.Length < 2 ||
                !int.TryParse(strs[0], out _x) ||
                !int.TryParse(strs[1], out _y) ||
                _x < posMin || _y < posMin || _x > posMax || _y > posMax)
            {
                return tryAgainCode;
            }

            --_x; --_y;
            if (_lastX != -1 && _lastY != -1 &&
                (_lastX != _x / size || _lastY != _y / size))
            {
                return tryAgainCode;
            }

            if (Set(_x, _y, _player))
            {
                PrintCell(_x, _y);

                int result = _field[_x / size, _y / size].Check(_x % size, _y % size);
                if (result != -1 && result != Field<Field<Cell>>.drawCode)
                {
                    Console.ForegroundColor = (_player == State.X) ? ConsoleColor.Red : ConsoleColor.Blue;
                    switch (result)
                    {
                        case Field<Field<Cell>>.horizontalCode:
                            PrintCell(_x, _y - _y % size);
                            PrintCell(_x, _y - _y % size + 1);
                            PrintCell(_x, _y - _y % size + 2);
                            break;
                        case Field<Field<Cell>>.verticalCode:
                            PrintCell(_x - _x % size, _y);
                            PrintCell(_x - _x % size + 1, _y);
                            PrintCell(_x - _x % size + 2, _y);
                            break;
                        case Field<Field<Cell>>.slopingCode:
                            PrintCell(_x - _x % size, _y - _y % size);
                            PrintCell(_x - _x % size + 1, _y - _y % size + 1);
                            PrintCell(_x - _x % size + 2, _y - _y % size + 2);
                            break;
                        case Field<Field<Cell>>.backSlopingCode:
                            PrintCell(_x - _x % size + 2, _y - _y % size);
                            PrintCell(_x - _x % size + 1, _y - _y % size + 1);
                            PrintCell(_x - _x % size, _y - _y % size + 2);
                            break;
                    }

                    Console.ForegroundColor = ConsoleColor.White;

                    result = _field.Check(_x / size, _y / size);
                    if (result == Field<Field<Cell>>.drawCode)
                    {
                        return drawCode;
                    }
                    else if (result != -1)
                    {
                        return winCode;
                    }
                }

                _player = (_player == State.X) ? State.O : State.X;

                _lastX = _x % size;
                _lastY = _y % size;
                if (isFull(_lastX, _lastY))
                {
                    _lastX = -1;
                    _lastY = -1;
                }

                return successfulTryCode;
            }

            return tryAgainCode;
        }
    }

    class Cell : IValue
    {
        public State Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private State _value = State.Empty;
    }
}
