using PersonInfomation.Database;
using PersonInfomation.Extensions_Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using PersonInfomation.Extensions_Function;
using System.Xml.Linq;

namespace PersonInfomation.BLL
{
    /// <summary>
    /// Summary description for PersonFunny
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PersonFunny : System.Web.Services.WebService
    {

        [WebMethod]
        public string personList()
        {
            string strSQL = "Select * from Person";

            //Get data from Database Layer
            DataTable person = connectDatabase.Excute(strSQL);

            //Use LINQ to Dataset
            var query = from item in person.AsEnumerable()
                        where ConditionFunction.filterCondition(item) == true
                        select new
                        {
                            firstName = item.Field<string>("FirstName"),
                            lastName = item.Field<string>("LastName"),
                            age = item.Field<int>("Age")
                        };

            //Convert EnumerableRow to array
            var array = query.ToArray();
            var json = WriteJSon.writeFileJSON(array);

            return json;
        }
    }
}
