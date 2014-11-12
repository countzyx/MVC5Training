using System.Web;

//[assembly: PreApplicationStartMethod(typeof(CommonModules.ModuleRegistration), "RegisterModule")]

namespace CommonModules {
    public class ModuleRegistration {
        public static void RegisterModule() {
            HttpApplication.RegisterModule(typeof(CommonModules.TimerModule));
            HttpApplication.RegisterModule(typeof(CommonModules.TotalTimeModule));
            HttpApplication.RegisterModule(typeof(CommonModules.InfoModule));
        }
    }
}
