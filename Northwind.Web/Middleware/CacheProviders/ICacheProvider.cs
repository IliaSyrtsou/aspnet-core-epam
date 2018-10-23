namespace Northwind.Web.Middleware.CacheProviders
{
    internal interface ICacheProvider<T>
    {
        T Get(string key);
        void Set(string key, T value);
    }
}