using System.Collections.Generic;

namespace MyClassLibrary
{
    /// <summary>
    /// 分解质因数的类
    /// </summary>
    public static class factor_of_decomposition
    {
        /// <summary>
        /// 分解质因数
        /// </summary>
        /// <param name="input">要分解的数，输入负数会导致异常</param>
        /// <returns>返回分解后的字符串</returns>
        public static string decompose(int input)
        {
            int y = 1;
            List<int> list = new List<int>();
            while (y <= input)
            {
                bool zhi_shu = prime_number_judgment(y);
                if (zhi_shu == true)
                {
                    if (input % y == 0)
                    {
                        list.Add(y);
                        input /= y;
                        y = 1;
                    }
                    else { y += 1; }
                }
                else { y += 1; }
            }
            string jieguo = "";
            foreach (int name in list)
            {
                jieguo += name + " ";
            }
            return jieguo;
        }
        /// <summary>
        /// 判断一个数是否为质数
        /// </summary>
        /// <param name="input">要判断的数</param>
        /// <returns>返回值</returns>
        public static bool prime_number_judgment(int input)
        {
            List<int> numbers = new List<int>();
            bool zhi_shu = true;
            for (int i = 2; i < input; i++)
            {
                numbers.Add(i);
            }
            foreach (int number in numbers)
            {
                if (input % number != 0)
                {
                    zhi_shu = true;
                }
                else { zhi_shu = false; break; }
            }

            if (input == 1)
            {
                zhi_shu = false;
            }
            return zhi_shu;
        }
    }
}