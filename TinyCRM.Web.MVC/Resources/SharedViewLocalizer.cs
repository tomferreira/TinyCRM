using Microsoft.Extensions.Localization;
using System.Reflection;

namespace TinyCRM.Web.MVC.Resources
{
    public class SharedViewLocalizer : ISharedViewLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public SharedViewLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public string this[string key] => _localizer[key];

        public string this[string key, params object[] arguments] => _localizer[key, arguments];
    }
}
