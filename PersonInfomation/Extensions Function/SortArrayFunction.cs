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
        public static int CheckCompare(int current,int next)
        {
            if (current > next)
            {
                return 1;
            }
            else
            {
                return -1;
            }          
        }
        //Sort Age
        public static List<ModelPerson> sortArray(List<ModelPerson> persons)
        {
            //for (var i = 0; i < persons.Count() - 1; i++)
            //{
            //    for (var j = i + 1; j < persons.Count(); j++)
            //    {
            //        if (persons[i].age > persons[j].age)
            //        {
            //            object temp = persons[i];
            //            persons[i] = persons[j];
            //            persons[j] = (ModelPerson)temp;
            //        }
            //    }
            //}
            persons.Sort((current,next)=>CheckCompare(current.age,next.age));

            return persons;
        }
    }
}


