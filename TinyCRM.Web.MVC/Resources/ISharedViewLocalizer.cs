namespace TinyCRM.Web.MVC.Resources
{
    public interface ISharedViewLocalizer
    {
        string this[string key] { get; }

        string this[string key, params object[] arguments] { get; }
    }
}
