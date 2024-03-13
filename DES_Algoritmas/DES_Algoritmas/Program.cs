using System.Security.Cryptography;

namespace DES_Algoritmas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DES Sifravimo/Desifravimo sistema:");
            Console.WriteLine("1: Sifruoti teksta");
            Console.WriteLine("2: Desifruoti teksta");
            Console.Write("Pasirinkite opcija: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Write("\nIveskite slapta rakta (8 characters): ");
            string key = Console.ReadLine();
            if (key.Length != 8)
            {
                Console.WriteLine("Raktas turi buti 8 simboliu");
                return;
            }

            Console.WriteLine("Pasirinkite sifravimo moda: ");
            Console.WriteLine("1: ECB");
            Console.WriteLine("2: CBC");
            Console.WriteLine("3: CFB");
            Console.Write("Pasirinkimas: ");
            int modeChoice = Convert.ToInt32(Console.ReadLine());

            CipherMode mode = modeChoice switch
            {
                1 => CipherMode.ECB,
                2 => CipherMode.CBC,
                3 => CipherMode.CFB,
                _ => throw new ArgumentOutOfRangeException("Tokios opcijos nera.")
            };

            if (choice == 1)
            {

                Console.Write("\nIveskite teksta kuri norite sifruoti: ");
                string plainText = Console.ReadLine();

                byte[] encrypted = DesEncryptionSystem.EncryptText(plainText, key, mode);
                string encryptedText = Convert.ToBase64String(encrypted);
                Console.WriteLine($"\nEncrypted text: {encryptedText}");

                Console.Write("Ar norite issaugoti sifruota teksta faile? (y/n): ");
                string save = Console.ReadLine();
                if (save.ToLower() == "y")
                {
                    Console.Write("Iveskite failo pavadinima: ");
                    string filename = Console.ReadLine();
                    DesEncryptionSystem.SaveToFile(filename, encrypted);
                    Console.WriteLine("Sifruotas tekstas issaugotas faile.");
                    Console.WriteLine($"Faila rasite cia: /DES_Algoritmas/bin/Debug/net8.0/{filename}");
                    Console.ReadKey();
                }
            }
            else if (choice == 2)
            {
                Console.Write("Irasykite sifruota teksta arba failo varda: ");
                string input = Console.ReadLine();
                byte[] encryptedData;

                if (File.Exists(input))
                {
                    encryptedData = DesEncryptionSystem.ReadFromFile(input);
                    Console.WriteLine("Sifruotas tekstas perskaitytas is failo.");
                }
                else
                {
                    encryptedData = Convert.FromBase64String(input);
                }

                string decryptedText = DesEncryptionSystem.DecryptText(encryptedData, key, mode);
                Console.WriteLine($"Desifruotas tekstas: {decryptedText}");
            }
            else
            {
                Console.WriteLine("Tokios opcijos nera.");
            }
        }
    }
}

