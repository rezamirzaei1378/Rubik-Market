using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Application.Generator
{
    public class CodeGenerator
    {
        public static string UniqueCode()
        {
            Random rnd= new Random();

            int randomNum = rnd.Next(100_000, 999_999);
            return randomNum.ToString();
        }
    }
}
