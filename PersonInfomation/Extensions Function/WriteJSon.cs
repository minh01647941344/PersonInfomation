using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PersonInfomation.Extensions_Function;
using System.Collections;
using PersonInfomation.BLL;

namespace PersonInfomation.Extensions_Function
{
    public class WriteJSon
    {
        public static String writeFileJSON(List<ModelPerson> array)
        {                 
            //Sort Array
            List<ModelPerson> result = SortArrayFunction.sortArray(array);

            //Parse JSON
            var parseJson = JsonConvert.SerializeObject(result);

            return parseJson;
        } 
    }
}