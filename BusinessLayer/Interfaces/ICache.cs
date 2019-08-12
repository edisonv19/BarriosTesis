namespace BusinessLayer.Interfaces
{
    public interface ICache<T>
    {
        T GetObject(string key);
        void SetObject(string key, T o);
    }
}
