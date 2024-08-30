using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using A2.Models;
using Microsoft.AspNetCore.Http;

namespace A2.Helper
{
    public class CalendarOutputFormatter : TextOutputFormatter
    {
        public CalendarOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/calendar"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            string time = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");

            Event e = (Event)context.Object;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:sgua726");
            sb.AppendLine("BEGIN:VEVENT");
            sb.Append("UID:");
            sb.AppendLine(e.Id + "");
            sb.Append("DTSTAMP:");
            sb.AppendLine(time);
            sb.Append("DTSTART:");
            sb.AppendLine(e.Start);
            sb.Append("DTEND:");
            sb.AppendLine(e.End);
            sb.Append("SUMMARY:");
            sb.AppendLine(e.Summary);
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            string outString = sb.ToString();
            byte[] outBytes = selectedEncoding.GetBytes(outString); 
            var response = context.HttpContext.Response.Body;
            return response.WriteAsync(outBytes, 0, outBytes.Length);
        }
    }
}