using Newtonsoft.Json;
using PersonInfomation.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonInfomation.Extensions_Function
{
    public class SortArrayFunction
    {
        //Parse JSON to List
        public static List<T> GetObject<T>(string json)
        {
            var obj = JsonConvert.DeserializeObject<List<T>>(json);
            return obj.ToList();
        }
        //Sort Age
        public static string sortArray(string json)
        {
            //Call function GetObject
            var persons = GetObject<ModelPerson>(json);

            for (var i=0;i<persons.Count()-1;i++)
            {
                for(var j=i+1;j<persons.Count();j++)
                {
                    if (persons[i].age > persons[j].age)
                    {
                        object temp = persons[i];
                        persons[i] = persons[j];
                        persons[j] = (ModelPerson)temp;
                    }
                }
            }
            //Parse List to JSON
            var jsonAfter = JsonConvert.SerializeObject(persons);
            return jsonAfter;
        }
    }
}

