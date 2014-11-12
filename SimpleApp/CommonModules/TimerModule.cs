using System;
using System.Diagnostics;
using System.Web;

namespace CommonModules {
    public class TimerModule : IHttpModule {
        public event EventHandler<RequestTimerEventArgs> RequestTimed;
        private Stopwatch timer;

        public void Init(HttpApplication app) {
            app.BeginRequest += OnBeginRequest;
            app.EndRequest += OnEndRequest;
        }

        public void OnBeginRequest(object src, EventArgs args) {
            timer = Stopwatch.StartNew();
        }

        public void OnEndRequest(object src, EventArgs args) {
            var ctx = HttpContext.Current;
            var duration = (float)timer.ElapsedTicks / Stopwatch.Frequency;
            ctx.Response.Write(String.Format("<div class='alert alert-success'>Elapsed: {0:F5} seconds</div>",
                duration));
            if (RequestTimed != null) {
                RequestTimed(this, new RequestTimerEventArgs { Duration = duration });
            }
        }

        public void Dispose() { }
    }
}