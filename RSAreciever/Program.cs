using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace RSAreciever
{
    class Program
    {
        const string publicKeyPath = "c:\\temp\\publickey.xml";
        const string privateKeyPath = "c:\\temp\\privatekey.xml";
        static void Main(string[] args)
        {
            var rsaer = new RSAdecrypt();

            rsaer.AssignNewKey(publicKeyPath, privateKeyPath);

            while (true)
            {
                ShowKeyData();

                Console.WriteLine("Insert data to decrypt: ");
                string inputString = string.Empty;
                using (var r = new StreamReader(Console.OpenStandardInput(2048)))
                {
                    inputString = r.ReadLine();
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to decrypt.\n");
                Console.ReadKey();

                byte[] decrypted = rsaer.DecryptData(privateKeyPath, Convert.FromBase64String(inputString));

                Console.WriteLine("Decrypted data = " + Encoding.UTF8.GetString(decrypted));
                Console.WriteLine();

                Console.ReadKey();
            }
        }

        public static void ShowKeyData()
        {

            Console.WriteLine("Public key Data:");
            using (XmlReader reader = XmlReader.Create(publicKeyPath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "Modulus":
                                Console.WriteLine("Modulus: " + reader.ReadString());
                                break;
                            case "Exponent":
                                Console.WriteLine("Exponent: " + reader.ReadString());
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Private Key Data");
            using (XmlReader reader = XmlReader.Create(publicKeyPath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "Modulus":
                                Console.WriteLine("Modulus: " + reader.ReadString());
                                break;
                            case "Exponent":
                                Console.WriteLine("Exponent: " + reader.ReadString());
                                break;
                            case "P":
                                Console.WriteLine("P: " + reader.ReadString());
                                break;
                            case "Q":
                                Console.WriteLine("Q: " + reader.ReadString());
                                break;
                            case "DP":
                                Console.WriteLine("DP: " + reader.ReadString());
                                break;
                            case "DQ":
                                Console.WriteLine("DQ: " + reader.ReadString());
                                break;
                            case "InverseQ":
                                Console.WriteLine("InverseQ: " + reader.ReadString());
                                break;
                            case "D":
                                Console.WriteLine("D: " + reader.ReadString());
                                break;

                        }
                    }
                    Console.WriteLine();
                }
            }
        }

    }
}
