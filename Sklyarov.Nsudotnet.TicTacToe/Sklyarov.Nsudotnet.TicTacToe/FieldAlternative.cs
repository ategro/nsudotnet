using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklyarov.Nsudotnet.TicTacToe
{
    class FieldAlternative
    {
        public const int tryAgainCode = 0;
        public const int successfulTryCode = 1;
        public const int drawCode = 2;
        public const int winCode = 3;
       
        public static readonly int size = 3;
        public static readonly int posMin = 1;
        public static readonly int posMax = 9;

        public FieldAlternative(IGUI gui)
        {
            _gui = gui;
        }
        public void Run()
        {
            _gui.Init();
            _gui.UpdatePlayer(_player);

            while (true)
            {
                switch (NextStep())
                {
                    case tryAgainCode: continue;
                    case successfulTryCode: break;
                    case drawCode:
                        _gui.PrintResult(State.Draw);
                        return;
                    case winCode:
                        _gui.PrintResult(_player);
                        return;
                }

                _gui.UpdatePlayer(_player);
            }
        }

        private State _player = State.X;
        private int _x, _y;
        private int _lastX = -1, _lastY = -1;
        private Field<Field<Cell>> _field = new Field<Field<Cell>>();
        private IGUI _gui;

        private int Set(int x, int y, State state)
        {
            if (_field[x / size, y / size][x % size, y % size].Value == State.Empty)
            {
                _field[x / size, y / size][x % size, y % size].Value = state;

                _gui.UpdateCell(_x, _y, _player);

                int result = _field[_x / size, _y / size].Check(_x % size, _y % size);
                if (result != -1 && result != Field<Field<Cell>>.drawCode)
                {
                    switch (result)
                    {
                        case Field<Field<Cell>>.horizontalCode:
                            _gui.UpdateTripleCells(_x, _y - _y % size, _x, _y - _y % size + 1, _x, _y - _y % size + 2, _player);
                            break;
                        case Field<Field<Cell>>.verticalCode:
                            _gui.UpdateTripleCells(_x - _x % size, _y, _x - _x % size + 1, _y, _x - _x % size + 2, _y, _player);
                            break;
                        case Field<Field<Cell>>.slopingCode:
                            _gui.UpdateTripleCells(_x - _x % size, _y - _y % size, _x - _x % size + 1, _y - _y % size + 1, _x - _x % size + 2, _y - _y % size + 2, _player);
                            break;
                        case Field<Field<Cell>>.backSlopingCode:
                            _gui.UpdateTripleCells(_x - _x % size + 2, _y - _y % size, _x - _x % size + 1, _y - _y % size + 1, _x - _x % size, _y - _y % size + 2, _player);
                            break;
                    }

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

            return -1;
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
            string str = _gui.GetNextStep();

            string[] strs = str.Split(new char[] { ' ' });
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

            int result = Set(_x, _y, _player);
            if (result != -1)
            {
                return result;
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
