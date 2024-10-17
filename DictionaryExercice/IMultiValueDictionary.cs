namespace DictionaryExercice
{
    public interface IMultiValueDictionary<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        bool Remove(TKey key, TValue value);
        bool ContainsKey(TKey key);
        bool ContainsValue(TKey key, TValue value);
        HashSet<TValue> GetValues(TKey key);
        IEnumerable<TKey> GetKeys();
        void Clear();
    }
}
