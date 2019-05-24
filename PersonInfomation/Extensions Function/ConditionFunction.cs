using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfomation.Extensions_Function
{
    class ConditionFunction
    {
        public static Boolean filterCondition(DataRow item)
        {
            string fName = item.ItemArray[1].ToString().ToUpper();
            string firstChar = fName[0].ToString();
            var fullname = fName.Length + item.ItemArray[2].ToString().ToUpper().Trim().Length;
            if (firstChar == "M" && fullname > 12)
            {
                return true;
            }
            return false;
        }
    }
}