﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _3des
{
    class des 
    {
        public const int SIZE_OF_BLOCK = 64;
        public const int COUNT_ROUNDS = 16;
        public const int SHIFT_KEY = 2;
        public const int SIZE_KEY = 48;

        public static Random rand = new Random();
        public static string key;
        //public static string key1 = "";
        //public static string key2 = "";
        //public static string key3 = "";
         
         public static string[] Mykeys = new string[16];

        public static int[] Transformation(int[] bits, int[] transition)
        {
            int[] newbits = new int[transition.Count()];
            for (int i = 0; i < transition.Count(); i++)
            {
                newbits[i] = bits[transition[i] - 1];
            }
            return newbits;
        }
        public static int[] UnTransformation(int[] bits)
        {
            int[] transition = new int[] { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7,47,15,55,23,63,31, 38,6,46,14,54,22,62,30,37,5,45,13,53,21,61,29,36,4,44,12,52,20,60,28,35,3,43,11,51,19,59,27,34,2,42,10,50,18,58,26,33,1,41,9,49,17,57,25 };
            int[] newbits = new int[bits.Count()];
            for (int i = 0; i < bits.Count(); i++)
            {
                newbits[i] = bits[transition[i] - 1];
            }
            return newbits;
        }
        public static int Conv(string a)
        {
            int len = a.Length; 
            int nomber = 0;
            int[] b = new int[len];
            for (int i = 0; i < len; i++)
            {
                if (a[i] == '1')
                    b[i] = 1;
                else b[i] = 0;
            }
                for (int i = 0; i < len; i++)
                {
                   nomber += b[i] * Convert.ToInt32(Math.Pow(2, len - i-1));
                }
            return nomber;
        }
        public static string InBite(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                string temp = Convert.ToString(input[i], 2);

                while (temp.Length < 16)
                    temp = "0" + temp;

                output += temp;
            }

            return output;
 
        }// 1
        public static string AddSymbol(string textInByte)
        {
                    
        if (textInByte.Length % 4 ==0)
        {}
        else 
        {
            while (textInByte.Length % 4 !=0)
                textInByte += '#';
        }
            return textInByte;
        }
        public static string[] DivideOnBlock(string textInByte)
        {
            int size = textInByte.Length;

            if ((size % SIZE_OF_BLOCK) == 0)
            {
                int j = 0;
                string[] BlockOfString = new string[(size / SIZE_OF_BLOCK)];
                for (int i = 0; i < (size / SIZE_OF_BLOCK); i++)
                {
                    BlockOfString[i] = textInByte.Substring(i * SIZE_OF_BLOCK, SIZE_OF_BLOCK);
                }
                return BlockOfString;
            }
            else
            {
                string[] BlockOfString = new string[(size / SIZE_OF_BLOCK) + 1];
                for (int i = 0; i < (size / SIZE_OF_BLOCK)+1; i++)
                {
                    for (int j = 0; j < SIZE_OF_BLOCK; )
                    {
                        if ( i == (size / SIZE_OF_BLOCK) && (Math.Abs(size- SIZE_OF_BLOCK) < j))
                        {
                            BlockOfString[i] += 0;
                            j++;
                        }
                        else
                        {
                            BlockOfString[i] += textInByte[j];
                            j++;
                        }
                    }
                }
                return BlockOfString;
            }
            
 
        } // добавляю в конец 0, если неполный блок 2
        public static string[] AfterTrans(string[] Blocks, int[] IP)
        {
            string[] output = new string[Blocks.Length];
            for (int i = 0; i < Blocks.Length; i++)
            {
                int[] bits = new int[SIZE_OF_BLOCK];
                for (int j = 0; j < Blocks[i].Length; j++)
                {
                    if ((Blocks[i][j]) == '0')
                        bits[j] = 0;
                    else bits[j] = 1;
                }
                int[] temp = new int[bits.Length];
                temp = Transformation(bits, IP);
                for (int k = 0; k < temp.Count(); k++)
                {
                    output[i] += temp[k].ToString();
                }
            }
            return output;
        }//3
        public static string PBox(string HalfBlock)//половина блока расширяется
        {
            int[] bits = new int[SIZE_KEY];
            string output = "";
            for (int i = 0; i < HalfBlock.Length; i++)
            {
                if (HalfBlock[i] == '0')
                    bits[i] = 0;
                else bits[i] = 1;
            }
            int[] P = new int[] { 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1 };
            int[] temp = Transformation(bits, P);
            for (int k = 0; k < bits.Count(); k++)
            {
                output += temp[k].ToString();
            }
            return output;
        }
        public static string XOR (string R, string key)
        {
            string answer = "";
            for (int i = 0; i < R.Length; i++)
            {
                if ((R[i] == '1' && key[i] == '1') || (R[i] == '0' && key[i] == '0'))
                    answer += '0';
                if ((R[i] == '0' && key[i] == '1') || (R[i] == '1' && key[i] == '0'))
                    answer += '1';
            }
            return answer;
        }
        public static string code(string input) // 
        {
            int[] IP = new int[] { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
            string[] a = des.DivideOnBlock(des.InBite( des.AddSymbol(input)));
            string[] b = des.AfterTrans(a, IP);
            string answer = "";
            //key = GenerKey();
            for (int i = 0; i < b.Length; i++)
            {
                answer += NetworkF(b[i]);
            }
            return answer;
        }
        public static string GenerKey()
        {
            int sum = 0;
            String key = "";
            for (int i = 0; i < 56; i++)
            {
                int randomNumber = rand.Next(2);
                key += randomNumber.ToString();
                sum += randomNumber;
                if ((i + 1) % 7 == 0)
                {
                    if (sum % 2 == 0) key += '1';
                    else key += '0';
                    sum = 0;
                }
            
            }
            return key;
        }
        public static string NetworkF(string block)
        {
           // GenerKey(key1);
            //Console.WriteLine(key.Length);
            Mykeys = des.CreateKey(key);
            string answer = "";
            int len = block.Length / 2;
            string L = "", R = "";
            for (int j = 0; j < len; j++)
            {
                L += block[j];
                R += block[j + len];
            }
            for (int i = 0; i < COUNT_ROUNDS; i++)
            {
                string temp = R;
                string fun = Round(temp,Mykeys[i]);
                R = XOR(L,fun);
                L = temp;
            }
            string answer2 = "";
            answer2 = L+R;
            int[] answer3 = new int[answer2.Length];
            for (int i = 0; i < answer3.Length; i++)
            {
                if (answer2[i] == '1')
                    answer3[i] = 1;
                else answer3[i] = 0;
            
            }
            int[] transition = new int[] { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 };
            
            
            for (int i = 0; i < des.Transformation(answer3,transition).Length; i = i+ 16)
            {
                string word = "";
                for (int j = 0; j < 16; j++)
                {
                    word += (des.Transformation(answer3, transition)[j + i]);
                }
                int number = Conv(word);
                answer += (char)number;
            }
            
            return answer;
        }
        public static string Round(string R, string key)
        {
            string ExtendedR = "";
            ExtendedR = des.PBox(R);
            string RxorKEY = "";
            RxorKEY = des.XOR(ExtendedR, key);
            string[] B = new string[8];
            for (int i = 0; i < B.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    B[i] += RxorKEY[j + 6 * (i)];
                }
            }
            string[] newB = new string[8];
            int[, ,] S = new int[8, 4, 16] {
            { 
            { 14, 4 ,13, 1, 2, 15, 11, 8, 3, 10 ,6, 12 ,5, 9, 0, 7},
            {0, 15, 7, 4, 14, 2 ,13, 1, 10, 6 ,12, 11, 9, 5, 3, 8}, 
            {4, 1, 14 ,8, 13, 6, 2, 11, 15 ,12, 9, 7, 3 ,10, 5, 0},
            {15, 12 ,8 ,2, 4 ,9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13} },
            {
                {15, 1 ,8 ,14, 6 ,11, 3 ,4 ,9, 7, 2 ,13, 12, 0, 5 ,10} ,
                {3 ,13, 4, 7, 15 ,2 ,8, 14, 12, 0, 1 ,10, 6 ,9, 11, 5} ,
                {0, 14, 7, 11, 10, 4, 13, 1, 5 ,8, 12, 6, 9 ,3, 2, 15} ,
                {13 ,8 ,10 ,1 ,3 ,15, 4, 2, 11, 6, 7 ,12, 0 ,5, 14 ,9}},
            { 
                {10, 0, 9, 14, 6 ,3, 15 ,5, 1, 13,12, 7, 11, 4 ,2, 8} ,
                {13, 7, 0 ,9, 3 ,4, 6, 10, 2, 8, 5, 14 ,12 ,11 ,15, 1} ,
                {13, 6, 4, 9 ,8 ,15, 3, 0 ,11, 1, 2, 12, 5, 10 ,14, 7},
                {1, 10, 13, 0, 6 ,9 ,8 ,7, 4, 15 ,14, 3, 11 ,5 ,2 ,12} } ,

            { {7, 13 ,14 ,3, 0 ,6 ,9, 10, 1, 2, 8 ,5, 11, 12, 4 ,15}, 
            {13,8, 11, 5, 6, 15 ,0 ,3, 4 ,7, 2, 12, 1 ,10, 14, 9},
            {10 ,6,9 ,0, 12 ,11 ,7 ,13, 15, 1 ,3, 14 ,5, 2, 8, 4} ,
            {3 ,15, 0, 6 ,10 ,1 ,13, 8, 9 ,4, 5 ,11 ,12, 7, 2, 14}, } ,

            {{2 ,12 ,4, 1, 7 ,10, 11, 6 ,8 ,5, 3 ,15, 13, 0 ,14, 9}, 
                {14 ,11 ,2 ,12 ,4 ,7, 13 ,1, 5, 0 ,15, 10, 3 ,9, 8, 6},
                {4, 2, 1, 11, 10, 13, 7 ,8 ,15 ,9, 12, 5, 6, 3 ,0, 14},
                {11, 8 ,12, 7, 1 ,14, 2 ,13, 6 ,15, 0, 9 ,10 ,4 ,5, 3}, }, 

            {{12, 1, 10, 15 ,9, 2, 6, 8 ,0, 13 ,3, 4 ,14, 7, 5 ,11},
                {10, 15, 4 ,2 ,7, 12, 9 ,5, 6, 1 ,13, 14, 0, 11 ,3 ,8} ,
                {9, 14, 15 ,5 ,2 ,8, 12, 3, 7, 0 ,4, 10, 1 ,13, 11, 6} ,
                {4, 3, 2, 12 ,9 ,5 ,15, 10, 11 ,14, 1 ,7 ,6, 0, 8 ,13} ,},

            {   {4, 11, 2 ,14, 15 ,0, 8 ,13 ,3 ,12, 9, 7 ,5, 10 ,6, 1}, 
                {13 ,0 ,11, 7, 4 ,9, 1, 10 ,14, 3, 5, 12 ,2, 15, 8 ,6} ,
                {1, 4, 11, 13, 12 ,3, 7, 14 ,10, 15 ,6 ,8, 0, 5, 9 ,2}, 
                {6 ,11, 13, 8, 1 ,4 ,10 ,7, 9, 5, 0, 15, 14, 2, 3, 12} ,},
            {{13, 2, 8 ,4 ,6, 15 ,11 ,1, 10, 9, 3, 14, 5 ,0 ,12 ,7} ,
                {1 ,15, 13, 8, 10 ,3, 7, 4 ,12, 5, 6, 11 ,0 ,14, 9, 2} ,
                {7, 11 ,4, 1 ,9, 12 ,14, 2 ,0 ,6 ,10, 13, 15, 3, 5 ,8} ,
                {2 ,1, 14 ,7 ,4, 10, 8 ,13, 15 ,12, 9, 0 ,3 ,5 ,6, 11}, },};
            for (int i = 0; i < B.Length; i++)
            {
                string temp = "";
                temp += B[i][0];
                temp += B[i][5];
                int m = des.Conv(temp);
                temp = "";
                for (int k = 1; k < 5; k++)
                    temp += B[i][k];
                int l = des.Conv(temp);
                newB[i] = Convert.ToString(S[i,m,l], 2);
                while (newB[i].Length < 4)
                {
                    newB[i] = '0' + newB[i];
                }
            }
            string a ="";
            for (int i = 0; i < newB.Length; i++)
            {
                a += newB[i];
            }
            string b = "";
            b = UNPBox(a);
                return b;
        }
        public static string UNPBox(string Block)
        {
            int[] bits = new int[Block.Length];
            string output = "";
            for (int i = 0; i < Block.Length; i++)
            {
                if (Block[i] == '0')
                    bits[i] = 0;
                else bits[i] = 1;
            }
            int[] P = new int[] { 16, 7, 20 ,21, 29, 12, 28, 17,1 ,15, 23, 26 ,5 ,18, 31, 10 ,2, 8, 24, 14 ,32, 27, 3, 9,19, 13, 30, 6, 22, 11, 4 ,25 };
            int[] temp = Transformation(bits, P);
            for (int k = 0; k < temp.Count(); k++)
            {
                output += temp[k];
            }
            return output;
        }
        public static string[] CreateKey(string key)
        {
            int[] K = new int[] {57,49,41,33,25,17,9,1,58,50,42,34,26,18,
                                 10,2,59,51,43,35,27,19,11,3,60,52,44,36,
                                 63,55,47,39,31,23,15,7,62,54,46,38,30,22,
                                 14,6,61,53,45,37,29,21,13,5,28,20,12,4};
            string key0 = "";
            for (int i = 0; i < 56; i++) key0 += key[K[i] - 1];
            string[] inputKey = new string[16];
            int[] Shift = new int[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            string C = "";
            string D = "";
            for (int i = 0; i < 28; i++)
            {
                 C += key0[i];
                 D += key0[i + 28];
            }
            
            for (int i = 0; i < 16; i++)
            {
                for (int k = 0; k < Shift[i]; k++)
                {
                    string temp = "";
                    string temp2 = "";
                    C = C + C[0];
                    D = D + D[0];
                    for (int j = 1; j < C.Length; j++)
                    {
                        temp += C[j];
                        temp2 += D[j];
                    }
                    C = temp;
                    D = temp2;
                }
                inputKey[i] = C + D;
            }
            int[] P = new int[] {14,17,11,24,1,5,3,28,15,6,21,10,23,19,12,4,
                                 26,8,16,7,27,20,13,2,41,52,31,37,47,55,30,40,
                                 51,45,33,48,44,49,39,56,34,53,46,42,50,36,29,32};
            string[] KEYINTHEEND = new string[16];
            for (int i = 0; i < 16; i++)
            {
                int[] temp = new int[inputKey[i].Length];
                for (int j = 0; j < temp.Count(); j++)
                {
                    if (inputKey[i][j] == '1')
                        temp[j] = 1;
                    else temp[j] = 0;
                }
                int[] keyint = new int[48];
                keyint = des.Transformation(temp,P);
                KEYINTHEEND[i] = "";
                for (int k = 0; k < keyint.Count(); k++)
                {
                    KEYINTHEEND[i] += keyint[k].ToString();
                }
            }
            return KEYINTHEEND;
        }
        public static string DeCode(string input)
        {
            Mykeys = des.CreateKey(key);


            int[] IP = new int[] { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
            string[] a = des.DivideOnBlock(des.InBite(input));
            string[] b = des.AfterTrans(a, IP);
            string answer = "";
            for (int i = 0; i < b.Length; i++)
            {
                answer += UNNetworkF(b[i]);
            }
            string output = "";
            output = DeleteSymbol(answer);
            return output;
        }
        public static string UNNetworkF(string block)
        {
            string answer = "";
            int len = block.Length / 2;
            string L = "", R = "";
            for (int j = 0; j < len; j++)
            {
                L += block[j];
                R += block[j + len];
            }
            for (int i = COUNT_ROUNDS -1; i >=0; i--)
            {

                string temp = L;
                string fun = Round(L, Mykeys[i]);
                L = XOR(R, fun);
                R = temp;
            }
            string answer2 = "";
            answer2 = L+R;
            int[] answer3 = new int[answer2.Length];
            for (int i = 0; i < answer3.Length; i++)
            {
                if (answer2[i] == '1')
                    answer3[i] = 1;
                else answer3[i] = 0;

            }
            int[] transition = new int[] { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 };

            int[] transformedBlock = des.Transformation(answer3, transition);

            for (int i = 0; i < transformedBlock.Length; i = i + 16)
            {
                string word = "";
                for (int j = 0; j < 16; j++)
                {
                    word += transformedBlock[j + i].ToString();
                }
                int number = Conv(word);
                answer += Convert.ToChar(number);
            }

            return answer;
        
        }
        public static string DeleteSymbol(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '#')
                    output += input[i];
                else return output;
            }
            return output;
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Form1 form = new Form1();
            form.ShowDialog();
            //string input = "vova";

            //string output = "";
            //String key1 = des.GenerKey();
            //String key2 = des.GenerKey();
            //String key3 = des.GenerKey();

            //output = _3DES.code(input, key1, key2, key3);
            //Console.WriteLine(output);
            //string a = "";
            //a = _3DES.decode(output, key1, key2, key3);
            //Console.WriteLine(a);
        }
    }
}
    
    

