using LinQDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LinQDemo.Service
{
    public class LinQToXmlService : ILinQToXmlService
    {
        public IEnumerable<object> Test()
        {
            var custOrdDoc = XDocument.Load("CustomersAndOrders.xml");
            var custOrd = custOrdDoc.Element("Root");
            var result = from c in custOrd.Element("Customers").Elements("Customer")
                         join o in custOrd.Element("Orders").Elements("Order")
                                    on (string)c.Attribute("CustomerID") equals
                                       (string)o.Element("CustomerID")
                         where ((string)c.Attribute("CustomerID")).CompareTo("K") > 0
                         select new
                         {
                             Order = new
                             {
                                 CustomerID = (string)c.Attribute("CustomerID"),
                                 CompanyName = (string)c.Element("CompanyName"),
                                 ContactName = (string)c.Element("ContactName"),
                                 EmployeeID = (string)o.Element("EmployeeID"),
                                 OrderDate = (DateTime)o.Element("OrderDate")
                             }
                         };

            return result;
        }
    }
}
