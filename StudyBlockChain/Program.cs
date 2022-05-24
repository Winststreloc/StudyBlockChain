using System;
using System.Linq;

namespace BlockChain_Study
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain.AddBlock();
            while (true)
            {

                string cmd = Console.ReadLine();
                if (cmd == "newblock") // создание блока с записью пользователя
                {
                    Console.Write("Данные на запись: ");
                    string dat = Console.ReadLine();
                    string prevHash = Blockchain.blockchain.Last().hash;
                    Blockchain.AddBlock(dat, prevHash);
                }
                else if (cmd == "history") // история блока
                {
                    int i = 0;
                    foreach (Block blc in Blockchain.blockchain)
                    {
                        Console.WriteLine("{0}, {1}, {2}, {3}, {4}", i, blc.data, blc.hash, blc.timestamp, blc.nonce);
                        i++;
                    }
                }

                else if (cmd == "verfi") //реализация веривикации пользователя
                {
                    Blockchain.Verification();
                }
                else if (cmd == "edit") //изменения блока
                {
                    int block = Convert.ToInt32(Console.ReadLine());
                    string newData = Console.ReadLine();
                    Blockchain.EDIT(block, newData);
                }
                else if (cmd == "save") //сохранение блока в файл
                {
                    Console.Write("Filename: ");
                    string filename = Console.ReadLine();
                    Blockchain.SaveBlockchain(filename);
                }
                else if (cmd == "load") // загрузка заранее загруженного блока
                {
                    Console.Write("Filename: ");
                    string filename = Console.ReadLine();
                    Blockchain.LoadBlockhain(filename);
                }
            }
        }
    }
}
