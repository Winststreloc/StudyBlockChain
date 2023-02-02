using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BlockChain_Study
{
    static class Blockchain
    {

        public static List<Block> blockchain = new List<Block>();
        static readonly int difficulity = 3;
        public static void AddBlock(string data = "genesis", string prvHash = "") 
        {
            int nonce = 0; 
            string timestamp = Convert.ToString(DateTime.Now);
            while (true)
            {

                string newHash = GetHash(timestamp, data, prvHash, nonce); 
                
                if (newHash.StartsWith(String.Concat(Enumerable.Repeat("0", difficulity))))
                {
                    Console.WriteLine("Нашёл {0}, nonce - {1}", newHash, nonce);
                    blockchain.Add(new Block(timestamp, data, newHash, nonce));

                    break;
                }
                nonce++;
            }

        }
        static string GetHash(string ts, string dat, string prvHash, int nonce)
        {

            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                    .ComputeHash(Encoding.UTF8.GetBytes(ts + dat + prvHash + nonce))
                    .Select(item => item.ToString("x2"))); ;
            }
        }
        public static void EDIT(int block, string data) 
        {
            blockchain[block].data = data;
        }

        public static void Verification()
        {
            for (int i = 1; i != blockchain.Count; i++)
            {
                string verHash = GetHash(blockchain[i].timestamp, blockchain[i].data, 
                    blockchain[i - 1].hash, blockchain[i].nonce);
                if (verHash == blockchain[i].hash)
                {
                    Console.WriteLine("Block {0} - OK", i);
                }
                else
                {
                    Console.WriteLine("Кто-то(вы) изменили данные блока.");
                    return;
                }

            }
            Console.WriteLine("Все блоки подтверждены.");
        }

        static readonly BinaryFormatter formatter = new BinaryFormatter();
        public static void SaveBlockchain(string filename)
        {

            using (FileStream filesave = new FileStream(filename + ".amber", FileMode.Create))
            {

                formatter.Serialize(filesave, blockchain);
            }
        }

        public static void LoadBlockhain(string filename)
        {
            using (FileStream fs = new FileStream(filename + ".amber", FileMode.OpenOrCreate))
            {

                blockchain = (List<Block>)formatter.Deserialize(fs);
                foreach (Block blc in blockchain)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", blc, blc.data, blc.hash, blc.timestamp);
                }
            }



        } 

    }
}