using ImdbDAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Helpers
{
    public static class MyHtmlHelpers
    {
        public static string PrettyJoin(this IHtmlHelper html, IEnumerable<Person> persons)
        {
            string s = null;
            int count = 0;
            foreach (var person in persons.Reverse())
            {
                switch (count++)
                {
                    case 0:
                        s = person.Name;
                        break;

                    case 1:
                        s = person.Name + " og " + s;
                        break;

                    default:
                        s = person.Name + ", " + s;
                        break;
                }
            }

            return s;
        }
    }
}
