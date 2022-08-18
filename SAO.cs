using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App4
{
    class SAO
    {
        //TODO 
        /*
         * Key expantion
         * Add Round key in each round
         * 
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Message to Encrypet:");
            
            string Message;
            byte[][] Key;
            List<Byte[][]> Ciphers=new List<byte[][]>();
            Key = StaticFunctions.def2DByte(4, 4);
            Message = Console.ReadLine();
            Message = StaticFunctions.msgDivBy16(Message);
          
            StaticFunctions.generateRandom2DByteArray(Key,4);
            StaticFunctions.transpose(Key);//mlhash lazma :S 3shan 5atrak ya dr
            List<Byte[][]> Keys = new List<Byte[][]>();
            StaticFunctions.addTolist(Key, Keys);
            for (int i = 0; i < 10; ++i)
            {
                Key = StaticFunctions.KeyExpantion(Key, i);
                StaticFunctions.addTolist(Key, Keys);

            }
            while (true)
            {
                Console.WriteLine("Choose the mode of operation:\n1)Electronic Codebook\n2)Cipher Block Chaining");
                int x = int.Parse(Console.ReadLine());
                if (x == 1)
                {
                    Ciphers = BlockCipherModes.ECB(Message, Keys);
                    BlockCipherModes.inverseECB(Ciphers, Keys);
                    Ciphers.Clear();
                }
                else if (x == 2)
                {
                    byte[][] IVbyteArray;
                    string IV = "";
                    IVbyteArray = StaticFunctions.def2DByte(4, 4);
                    Random r = new Random();
                    for (int i = 0; i < 16; ++i)
                        IV += r.Next() % 2 == 0 ? (char)(r.Next() % 26 + ((int)'a')) : (char)(r.Next() % 26 + ((int)'A'));
 
                    StaticFunctions.take16Byte(IV, IVbyteArray, 0);
                    Ciphers = BlockCipherModes.CBC(Message, Keys, IVbyteArray);
                    BlockCipherModes.inversecCBC(Ciphers, Keys, IVbyteArray);
                    Ciphers.Clear();
                }
                
            }


        }
    }
}
