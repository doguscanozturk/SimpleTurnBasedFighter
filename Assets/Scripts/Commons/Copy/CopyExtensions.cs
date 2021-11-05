namespace Commons.Copy
{
    public static class CopyExtensions
    {
        public static T[] GetDeepCopy<T>(this T[] attributesArray) where T : class, IDeepCopyable<T>
        {
            var result = new T[attributesArray.Length];

            for (int i = 0; i < attributesArray.Length; i++)
            {
                result[i] = attributesArray[i].DeepCopy();
            }

            return result;
        }
    }
}