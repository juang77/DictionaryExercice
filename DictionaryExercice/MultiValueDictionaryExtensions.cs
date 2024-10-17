namespace DictionaryExercice
{
    public static class MultiValueDictionaryExtensions
    {
        public static IMultiValueDictionary<TKey, TValue> Union<TKey, TValue>(
        this IMultiValueDictionary<TKey, TValue> first,
        IMultiValueDictionary<TKey, TValue> second)
        {
            var result = new MultiValueDictionary<TKey, TValue>();

            foreach (var key in first.GetKeys())
            {
                foreach (var value in first.GetValues(key))
                {
                    result.Add(key, value);
                }
            }

            foreach (var key in second.GetKeys())
            {
                foreach (var value in second.GetValues(key))
                {
                    result.Add(key, value);
                }
            }

            return result;
        }

        public static IMultiValueDictionary<TKey, TValue> Intersection<TKey, TValue>(
            this IMultiValueDictionary<TKey, TValue> first,
            IMultiValueDictionary<TKey, TValue> second)
        {
            var result = new MultiValueDictionary<TKey, TValue>();

            foreach (var key in first.GetKeys())
            {
                if (second.ContainsKey(key))
                {
                    foreach (var value in first.GetValues(key))
                    {
                        if (second.ContainsValue(key, value))
                        {
                            result.Add(key, value);
                        }
                    }
                }
            }

            return result;
        }

        public static IMultiValueDictionary<TKey, TValue> Difference<TKey, TValue>(
            this IMultiValueDictionary<TKey, TValue> first,
            IMultiValueDictionary<TKey, TValue> second)
        {
            var result = new MultiValueDictionary<TKey, TValue>();

            foreach (var key in first.GetKeys())
            {
                foreach (var value in first.GetValues(key))
                {
                    if (!second.ContainsValue(key, value))
                    {
                        result.Add(key, value);
                    }
                }
            }

            return result;
        }
    }
}
