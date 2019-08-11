namespace BusinessLayer.Interfaces
{
    public interface IAbstractFactory<T>
    {
        T GetData(string key);
    }
}
