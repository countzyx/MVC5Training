using System;
using System.Web;

namespace SimpleApp.Infrastructure {
    public class DayOfWeekHandler : IHttpHandler {
        public void ProcessRequest(HttpContext context) {
            string day = DateTime.Now.DayOfWeek.ToString();

            if (context.Request.CurrentExecutionFilePathExtension == ".json") {
                context.Response.ContentType = "application/json";
                context.Response.Write(String.Format("{{\"day\": \"{0}\"}}", day));
            } else {
                context.Response.ContentType = "text/html";
                context.Response.Write(String.Format("<span>It is: {0}</span>", day));
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}