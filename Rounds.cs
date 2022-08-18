using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App4
{
    class Rounds
    {
        public static byte[][] startRounds(byte[][] InputbyteArray,List<byte[][]> Keys)
        {
          
            StaticFunctions.transpose(InputbyteArray);
           
            //Round One we just add the round key nothing more....
            StaticFunctions.addRoundKey(Keys[0], InputbyteArray);

            return  rounds(Keys, InputbyteArray);
        }
        public static byte[][] rounds(List<byte[][]> Keys,byte[][] InputbyteArray)
        {
            string Message = "";
            for (int i = 0; i < 10; ++i)
            {
                InputbyteArray = StaticFunctions.subistituteBytesEnc(InputbyteArray, 4, 4);
                InputbyteArray = StaticFunctions.shiftRows(InputbyteArray);
                if (i != 9)
                {
                    InputbyteArray = StaticFunctions.mixCol(InputbyteArray);
                }
                if (i == 9)
                {
                    Console.Write("");
                  
                }
                StaticFunctions.addRoundKey(Keys[i+1], InputbyteArray);
            }
            StaticFunctions.transpose(InputbyteArray);

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    Message+= Convert.ToChar(InputbyteArray[i][j]);
                    
                }
            }
       
            Console.Write(Message);
            return InputbyteArray;
        }

      
    }
}
