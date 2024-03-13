using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algoritmas
{
    public class DesEncryptionSystem
    {
        public static byte[] EncryptText(string plainText, string key, CipherMode mode)
        {
            using (DES des = DES.Create())
            {
                des.Mode = mode;
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = des.Key;

                ICryptoTransform encryptor = des.CreateEncryptor(des.Key, des.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string DecryptText(byte[] cipherText, string key, CipherMode mode)
        {
            using (DES des = DES.Create())
            {
                des.Mode = mode;
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = des.Key;

                ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static void SaveToFile(string filename, byte[] data)
        {
            File.WriteAllBytes(filename, data);
        }

        public static byte[] ReadFromFile(string filename)
        {
            return File.ReadAllBytes(filename);
        }
    }

}
