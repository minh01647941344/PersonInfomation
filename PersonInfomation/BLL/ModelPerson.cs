using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonInfomation.BLL
{
    public class ModelPerson
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }

        public static implicit operator List<object>(ModelPerson v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator ModelPerson(List<object> v)
        {
            throw new NotImplementedException();
        }
    }
}