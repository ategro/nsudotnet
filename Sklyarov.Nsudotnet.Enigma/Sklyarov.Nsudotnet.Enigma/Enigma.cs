using System;
using System.IO;
using System.Security.Cryptography;

namespace Sklyarov.Nsudotnet.Enigma
{
    class Enigma
    {
        public static void Encrypt(String inName,  String algoName, String outName)
        {
            SymmetricAlgorithm algo = SymmetricAlgorithm.Create(algoName);
            BaseAlgorithm(inName, outName, algo.CreateEncryptor());
            CreateKeyFile(inName, algo);
        }

        public static void Decrypt(String inName, String algoName, String keyName, String outName)
        {
            SymmetricAlgorithm algo = SymmetricAlgorithm.Create(algoName);
            StreamReader keyStream = new StreamReader(keyName);

            string key = keyStream.ReadLine();
            string iV = keyStream.ReadLine();

            algo.Key = Convert.FromBase64String(key);
            algo.IV = Convert.FromBase64String(iV);

            BaseAlgorithm(inName, outName, algo.CreateDecryptor());
        }

        private static void BaseAlgorithm(String inName, String outName, ICryptoTransform cryptoTransform)
        {
            FileStream inStream = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream outStream = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            outStream.SetLength(0);

            CryptoStream cryptoStream = new CryptoStream(outStream, cryptoTransform, CryptoStreamMode.Write);

            Write(inStream, cryptoStream);

            cryptoStream.Close();
            outStream.Close();
            inStream.Close();
        }

        private static void Write(FileStream fileStream, CryptoStream cryptoStream)
        {
            byte[] buffer = new byte[4096];
            long readLen = 0;
            long totalLen = fileStream.Length;
            int len;
            byte totalPercent = 0;

            while (readLen < totalLen)
            {
                len = fileStream.Read(buffer, 0, buffer.Length);
                cryptoStream.Write(buffer, 0, len);
                readLen = readLen + len;
                byte temp = (byte)(100.0 * readLen / totalLen);
                if (temp != totalPercent)
                {
                    totalPercent = temp;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("{0}% processed.", totalPercent);
                }
            }
        }

        private static void CreateKeyFile(String fileName, SymmetricAlgorithm algo)
        {
            int pos = fileName.LastIndexOf('.');
            string keyName;
            if (pos == -1)
            {
                pos = fileName.Length;
            }
            keyName = string.Format("{0}.key{1}", fileName.Substring(0, pos), fileName.Substring(pos));

            StreamWriter keyStream = new StreamWriter(keyName);
            
            string key = Convert.ToBase64String(algo.Key);
            string iV = Convert.ToBase64String(algo.IV);
            
            keyStream.WriteLine(key);
            keyStream.WriteLine(iV);

            keyStream.Close();
        }
    }
}
