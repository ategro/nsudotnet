using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklyarov.Nsudotnet.Enigma
{
    class Program
    {
        public static readonly int modePos = 0;
        public static readonly int inNamePos = 1;
        public static readonly int typePos = 2;
        public static readonly int outNameEncPos = 3;
        public static readonly int outNameDecPos = 4;
        public static readonly int keyNamePos = 3;

        static void Main(string[] args)
        {
            try
            {
                switch (args[modePos])
                {
                    case "encrypt":
                        Enigma.Encrypt(args[inNamePos], args[typePos], args[outNameEncPos]);
                        break;
                    case "decrypt":
                        Enigma.Decrypt(args[inNamePos], args[typePos], args[keyNamePos], args[outNameDecPos]);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
