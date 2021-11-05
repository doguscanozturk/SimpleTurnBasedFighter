namespace Commons.Copy
{
    public interface IDeepCopyable<T> where T : class
    {
        T DeepCopy();
    }
}