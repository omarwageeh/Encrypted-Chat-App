using System;
using System.Collections.Generic;

namespace App4
{
    class BlockCipherModes // Block cipher modes
    {
        public static List<byte[][]> ECB(string Message,List<Byte[][]> Keys)//Electronic Code Book
        {
           
        
            byte[][] InputbyteArray;
            InputbyteArray = StaticFunctions.def2DByte(4, 4);
            List<Byte[][]> Ciphers = new List<Byte[][]>();

           

            Console.WriteLine("Cipher:");
            for (int i = 0; i * 16 < Message.Length; i++)
            {
                StaticFunctions.take16Byte(Message, InputbyteArray, i);
                StaticFunctions.addTolist(Rounds.startRounds(InputbyteArray, Keys), Ciphers);
            }


            return Ciphers;
        }
        public static List<byte[][]> CBC(string Message,List<byte[][]>Keys,byte[][] IVbyteArray)//Cipher Block Chaining
        {
           
           
            byte[][] InputbyteArray;
           
            List<Byte[][]> Ciphers = new List<Byte[][]>();
            InputbyteArray = StaticFunctions.def2DByte(4, 4);
              
            for (int i = 0; i * 16 < Message.Length; i++)
            {
         
                StaticFunctions.take16Byte(Message, InputbyteArray, i);
           
                InputbyteArray=StaticFunctions.AddXY(InputbyteArray, IVbyteArray);
                StaticFunctions.addTolist(Rounds.startRounds(InputbyteArray, Keys), Ciphers);
                IVbyteArray=StaticFunctions.clone2DByteArray(Ciphers[i]);
        
              
                
            }


            return Ciphers;
        }
        public static void CFB(string Message)//Cipher Feed Back
        {

        }

        public static List<byte[][]> inverseECB(List<byte[][]> Ciphers,List<byte[][]>Keys)
        {
            Console.WriteLine("\nDecryption:");
            string Message = "";
            byte[][] InputbyteArray = new byte[4][];
            InputbyteArray=StaticFunctions.def2DByte(4, 4);
            List<byte[][]> Ret = new List<byte[][]>();

            for (int i = 0; i < Ciphers.Count; i++)
            {

                InputbyteArray=InverseRounds.startinvRounds(Ciphers[i], Keys);
                StaticFunctions.addTolist(InputbyteArray, Ret);
            }
            Console.WriteLine(Message);
            return Ret;
        }
        public static List<byte[][]> inversecCBC(List<byte[][]>Ciphers,List<byte[][]>Keys, byte[][] IVbyteArray)//Cipher Block Chaining
        {
            Console.WriteLine("\nDecryption:");
            string Message = "";
            byte[][] InputbyteArray;
            List<byte[][]> TempCiphers = StaticFunctions.clone2DByteList(Ciphers);
            InputbyteArray = StaticFunctions.def2DByte(4, 4);
            List<byte[][]> Ret = new List<byte[][]>();

            for (int i = 0; i < Ciphers.Count; i++)
            {
                if(i==0)
                {
                    InputbyteArray=InverseRounds.startinvRounds(TempCiphers[i], Keys);
                 
                    InputbyteArray =StaticFunctions.AddXY(InputbyteArray, IVbyteArray);     
                }
                else
                {
                    InputbyteArray = InverseRounds.startinvRounds(TempCiphers[i], Keys);
                    InputbyteArray= StaticFunctions.AddXY(InputbyteArray, Ciphers[i-1]);
             
                }

                StaticFunctions.addTolist(InputbyteArray, Ret);
                
            }
            Console.WriteLine(Message);
            return Ret;
        }
        public static void inverseCFB(string Message)//Cipher Feed Back
        {

        }

    }
}
