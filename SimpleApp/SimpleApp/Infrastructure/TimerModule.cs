using System;
using System.Diagnostics;
using System.Web;

namespace SimpleApp.Infrastructure {
    public class TimerModule : IHttpModule {
        private Stopwatch timer;

        public void Init(HttpApplication app) {
            app.BeginRequest += OnBeginRequest;
            app.EndRequest += OnEndRequest;
        }

        public void OnBeginRequest(object src, EventArgs args) {
            HttpContext ctx = HttpContext.Current;
            timer = Stopwatch.StartNew();
        }

        public void OnEndRequest(object src, EventArgs args) {
            HttpContext ctx = HttpContext.Current;
            ctx.Response.Write(String.Format("<div class='alert alert-success'>Elapsed: {0:F5} seconds</div>",
                ((float)timer.ElapsedTicks / Stopwatch.Frequency)));
        }

        public void Dispose() { }
    }
}