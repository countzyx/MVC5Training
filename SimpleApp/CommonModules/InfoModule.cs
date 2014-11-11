using System;
using System.Web;

namespace CommonModules {
    public class InfoModule : IHttpModule {
        public void Init(HttpApplication app) {
            app.EndRequest += (src, args) => {
                var ctx = HttpContext.Current;
                ctx.Response.Write(String.Format("<div class='alert alert-success'>URL: {0} Status: {1}</div>",
                    ctx.Request.RawUrl, ctx.Response.StatusCode));
            };
        }

        public void Dispose() { }
    }
}
