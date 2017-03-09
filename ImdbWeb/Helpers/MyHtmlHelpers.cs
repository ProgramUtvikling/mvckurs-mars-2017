using ImdbDAL;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ImdbWeb.Helpers
{
    public static class MyHtmlHelpers
    {
        public static IHtmlContent PrettyJoin(this IHtmlHelper html, IEnumerable<Person> persons)
        {
            using (var writer = new StringWriter())
            {
                var encoder = HtmlEncoder.Default;
                int count = persons.Count();
                foreach (var person in persons)
                {
                    html.ActionLink(person.Name, "Details", "Person", new { id = person.PersonId }).WriteTo(writer, encoder);
                    switch (--count)
                    {
                        case 0:
                            break;

                        case 1:
                            writer.Write(" og ");
                            break;

                        default:
                            writer.Write(", ");
                            break;
                    }
                }

                return new HtmlString(writer.ToString());
            }
        }
    }
}
