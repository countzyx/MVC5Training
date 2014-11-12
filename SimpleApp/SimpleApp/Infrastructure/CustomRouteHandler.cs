using System;
using System.Web;
using System.Web.Routing;

namespace SimpleApp.Infrastructure {
    public class CustomRouteHandler : IRouteHandler {
        public Type HandlerType { get; set; }

        public IHttpHandler GetHttpHandler(RequestContext requestContext) {
            return (IHttpHandler)Activator.CreateInstance(HandlerType);
        }
    }
}