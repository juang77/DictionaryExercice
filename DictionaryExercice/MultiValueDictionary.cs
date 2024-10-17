namespace DictionaryExercice
{
    public class MultiValueDictionary<TKey, TValue> : IMultiValueDictionary<TKey, TValue>
    {
        private Dictionary<TKey, HashSet<TValue>> _dictionary = new Dictionary<TKey, HashSet<TValue>>();


        public void Add(TKey key, TValue value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary[key] = new HashSet<TValue>();
            }
            _dictionary[key].Add(value);
        }

        public bool Remove(TKey key, TValue value)
        {
            if (!_dictionary.ContainsKey(key)) return false;

            var values = _dictionary[key];
            bool removed = values.Remove(value);

            if (values.Count == 0)
            {
                _dictionary.Remove(key);
            }

            return removed;
        }

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public bool ContainsValue(TKey key, TValue value)
        {
            return _dictionary.ContainsKey(key) && _dictionary[key].Contains(value);
        }

        public HashSet<TValue> GetValues(TKey key) => _dictionary.ContainsKey(key) ? _dictionary[key] : new HashSet<TValue>();

        // Implementamos el nuevo método GetKeys
        public IEnumerable<TKey> GetKeys() => _dictionary.Keys;

        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}
