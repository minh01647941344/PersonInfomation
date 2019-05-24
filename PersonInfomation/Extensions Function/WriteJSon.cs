using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PersonInfomation.Extensions_Function;
using System.Collections;

namespace PersonInfomation.Extensions_Function
{
    public class WriteJSon
    {
        public static String writeFileJSON(IEnumerable<object> array)
        {
            //Create object serializer
            JsonSerializer serializer = new JsonSerializer();                    
            //Parse JSON
            var json = JsonConvert.SerializeObject(array);
            //Sort Array
            string result = SortArrayFunction.sortArray(json);

            return result;
        } 
    }
}