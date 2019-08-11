namespace BusinessLayer.Interfaces
{
    public interface ICache<T>
    {
        T GetObject(string key);
    }
}
