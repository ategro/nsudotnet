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
        void PrintResult(State player);
        string GetNextStep();
    }
}
