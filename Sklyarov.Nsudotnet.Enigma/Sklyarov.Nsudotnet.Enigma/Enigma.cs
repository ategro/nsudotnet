using System;
using System.IO;
using System.Security.Cryptography;

namespace Sklyarov.Nsudotnet.Enigma
{
    class Enigma
    {
        public static void Encrypt(String inName,  String algoName, String outName)
        {
            using (SymmetricAlgorithm algo = SymmetricAlgorithm.Create(algoName))
            {
                CreateKeyFile(inName, algo);
                BaseAlgorithm(inName, outName, algo.CreateEncryptor());
            }
        }
        public static void Decrypt(String inName, String algoName, String keyName, String outName)
        {
            using (SymmetricAlgorithm algo = SymmetricAlgorithm.Create(algoName))
            {
                LoadKeyFile(keyName, algo);
                BaseAlgorithm(inName, outName, algo.CreateDecryptor());
            }
        }
        private static void BaseAlgorithm(String inName, String outName, ICryptoTransform cryptoTransform)
        {
            using (FileStream inStream = new FileStream(inName, FileMode.Open, FileAccess.Read),
                              outStream = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (CryptoStream cryptoStream = new CryptoStream(outStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    inStream.CopyTo(cryptoStream);
                }
            }
        }

        private static void Write(FileStream fileStream, CryptoStream cryptoStream)
        {
            fileStream.CopyTo(cryptoStream);
        }

        private static void CreateKeyFile(String fileName, SymmetricAlgorithm algo)
        {
            string keyName = GetKeyName(fileName);

            using (StreamWriter keyStream = new StreamWriter(keyName))
            {
                string key = Convert.ToBase64String(algo.Key);
                string iV = Convert.ToBase64String(algo.IV);

                keyStream.WriteLine(key);
                keyStream.WriteLine(iV);
            }
        }

        private static void LoadKeyFile(String fileName, SymmetricAlgorithm algo)
        {
            using (StreamReader keyStream = new StreamReader(fileName)) 
            {
                string key = keyStream.ReadLine();
                string iV = keyStream.ReadLine();

                algo.Key = Convert.FromBase64String(key);
                algo.IV = Convert.FromBase64String(iV); 
            }
        }

        private static string GetKeyName(String fileName) 
        {
            int pos = fileName.LastIndexOf('.');
            if (pos == -1)
            {
                pos = fileName.Length;
            }

            return string.Format("{0}.key{1}", fileName.Substring(0, pos), fileName.Substring(pos));
        }
    }
}
