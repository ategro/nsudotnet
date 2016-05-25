using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklyarov.Nsudotnet.TicTacToe
{
    interface IGUI
    {
        void Init();
        void UpdatePlayer(State player);
        void UpdateCell(int x, int y, State player);
        void UpdateTripleCells(int x1, int y1, int x2, int y2, int x3, int y3, State player);
        void PrintResult(State player);
        string GetNextStep();
    }
}
