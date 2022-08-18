using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App4
{
    class InverseRounds
    {
        public static byte[][] startinvRounds(byte[][] InputbyteArray, List<byte[][]> Keys)
        {


            StaticFunctions.transpose(InputbyteArray);
            //    StaticFunctions.printTest(InputbyteArray, 4, 4);
            StaticFunctions.addRoundKey(Keys[10], InputbyteArray);
            //     StaticFunctions.printTest(InputbyteArray, 4, 4);
            return invRounds(Keys, InputbyteArray);
        }
        public static byte[][] invRounds(List<byte[][]> Keys, byte[][] InputbyteArray)
        {
            for (int i = 9; i >= 0; --i)
            {
                InputbyteArray = StaticFunctions.InvShiftRows(InputbyteArray);
                InputbyteArray = StaticFunctions.InvsubistituteBytesEnc(InputbyteArray, 4, 4);
                StaticFunctions.addRoundKey(Keys[i], InputbyteArray);
                if (i != 0)
                    InputbyteArray = StaticFunctions.InvmixCol(InputbyteArray);
            }
            StaticFunctions.transpose(InputbyteArray);
            /*
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    Message += (Convert.ToChar(InputbyteArray[i][j]) + "");
                }
            }
            */
            return InputbyteArray;
        }
    }
}
