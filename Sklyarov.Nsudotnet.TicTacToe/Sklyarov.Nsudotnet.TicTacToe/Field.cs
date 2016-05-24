using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklyarov.Nsudotnet.TicTacToe
{
    class Field<T> : IValue
        where T : IValue, new()
    {
        public const int drawCode = 0;
        public const int horizontalCode = 1;
        public const int verticalCode = 2;
        public const int slopingCode = 3;
        public const int backSlopingCode = 4;

        public Field()
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    _field[i, j] = new T();
                }
            }
        }

        public T this[int i, int j]
        {
            get { return _field[i, j]; }
        }

        public State Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int Check(int i, int j)
        {
            if (Value != State.Empty)
            {
                return -1;
            }

            if (this[i, j].Value == State.O || this[i, j].Value == State.X)
            {
                int returnCode = -1;

                if (this[i, 0].Value == this[i, 1].Value && this[i, 1].Value == this[i, 2].Value)
                {
                    returnCode = horizontalCode;
                }
                else if (this[0, j].Value == this[1, j].Value && this[1, j].Value == this[2, j].Value)
                {
                    returnCode = verticalCode;
                }
                else if (i == j && this[0, 0].Value == this[1, 1].Value && this[1, 1].Value == this[2, 2].Value)
                {
                    returnCode = slopingCode;
                }
                else if (i + j == size - 1 && this[0, 2].Value == this[1, 1].Value && this[1, 1].Value == this[2, 0].Value)
                {
                    returnCode = backSlopingCode;
                }

                if (returnCode != -1)
                {
                    Value = this[i, j].Value;
                    return returnCode;
                }
            }

            bool isDraw = true;
            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    if (this[x, y].Value == State.Empty)
                    {
                        isDraw = false;
                    }
                }
            }

            if (isDraw)
            {
                Value = State.Draw;
                return drawCode;
            }

            return -1;
        }

        public static readonly int size = 3;
        private T[,] _field = new T[size, size];
        private State _value = State.Empty;
    }
}
