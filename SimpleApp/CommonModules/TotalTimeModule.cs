using System.IO;
using System.Web;
using System.Web.UI;

namespace CommonModules {
    public class TotalTimeModule : IHttpModule {
        private static float totalTime = 0;
        private static int requestCount = 0;
        private static string timerModuleName = null;

        public void Init(HttpApplication app) {
            var timerModule = FindTimerModule(app.Modules);
            if (timerModule != null) {
                timerModule.RequestTimed += (src, args) => {
                    totalTime += args.Duration;
                    requestCount++;
                };
            }

            app.EndRequest += (src, args) => {
                app.Context.Response.Write(CreateSummary());
            };
        }

        private TimerModule FindTimerModule(HttpModuleCollection modules) {
            TimerModule timerModule = null;

            if (timerModuleName == null) {
                foreach (var moduleName in modules.AllKeys) {
                    if (moduleName.Contains("CommonModules.Timer")) {
                        timerModule = modules[moduleName] as TimerModule;
                        if (timerModule != null) {
                            timerModuleName = moduleName;
                            break;
                        }
                    }
                }
            } else {
                timerModule = modules[timerModuleName] as TimerModule;
            }

            return timerModule;
        }

        private string CreateSummary() {
            var stringWriter = new StringWriter();
            var htmlWriter = new HtmlTextWriter(stringWriter);
            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "table table-bordered");
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "success");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write("Requests");
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write(requestCount);
                    htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "success");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write("Total Time");
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write("{0:F5} seconds", totalTime);
                    htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();
            htmlWriter.RenderEndTag();

            return stringWriter.ToString();
        }

        public void Dispose() { }
    }
}
