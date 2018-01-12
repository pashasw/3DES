using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3des
{
    class _3DES
    {
        public static String code(String input,String key1,String key2,String key3)
        {
            des.key = key1;
            String code1 = des.code(input);
            des.key = key2;
            String code2 = des.code(code1);
            des.key = key3;
            String code3 = des.code(code2);
            return code3;
        }

        public static String decode(String code, String key1, String key2, String key3)
        {
            des.key = key3;
            String decode3 = des.DeCode(code);
            des.key = key2;
            String decode2 = des.DeCode(decode3);
            des.key = key1;
            String decode1 = des.DeCode(decode2);
            return decode1;
        }
    }
}
