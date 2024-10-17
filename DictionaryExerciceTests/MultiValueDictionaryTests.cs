using DictionaryExercice;

namespace DictionaryExerciceTests
{
    public class MultiValueDictionaryTests
    {
        private readonly MultiValueDictionary<string, int> _multiValueDict;

        public MultiValueDictionaryTests()
        {
            _multiValueDict = new MultiValueDictionary<string, int>();
        }

        [Fact]
        public void Add_AddsValueToKey()
        {
            // Act
            _multiValueDict.Add("key1", 1);

            // Assert
            Assert.Contains(1, _multiValueDict.GetValues("key1"));
        }

        [Fact]
        public void Add_DoesNotAllowDuplicateValuesForKey()
        {
            // Act
            _multiValueDict.Add("key1", 1);
            _multiValueDict.Add("key1", 1); // Duplicado

            // Assert
            var values = _multiValueDict.GetValues("key1");
            Assert.Single(values); // Solo debe haber un elemento
        }

        [Fact]
        public void Remove_RemovesValueFromKey()
        {
            // Arrange
            _multiValueDict.Add("key1", 1);
            _multiValueDict.Add("key1", 2);

            // Act
            bool removed = _multiValueDict.Remove("key1", 1);

            // Assert
            Assert.True(removed);
            Assert.DoesNotContain(1, _multiValueDict.GetValues("key1"));
        }

        [Fact]
        public void Remove_ReturnsFalseIfValueDoesNotExist()
        {
            // Arrange
            _multiValueDict.Add("key1", 1);

            // Act
            bool removed = _multiValueDict.Remove("key1", 99); // Valor inexistente

            // Assert
            Assert.False(removed);
        }

        [Fact]
        public void GetValues_ReturnsAllValuesForKey()
        {
            // Arrange
            _multiValueDict.Add("key1", 1);
            _multiValueDict.Add("key1", 2);

            // Act
            var values = _multiValueDict.GetValues("key1");

            // Assert
            Assert.Contains(1, values);
            Assert.Contains(2, values);
        }

        [Fact]
        public void GetValues_ReturnsEmptyIfKeyDoesNotExist()
        {
            // Act
            var values = _multiValueDict.GetValues("key99");

            // Assert
            Assert.Empty(values);
        }

        [Fact]
        public void ContainsKey_ReturnsTrueIfKeyExists()
        {
            // Arrange
            _multiValueDict.Add("key1", 1);

            // Act
            bool contains = _multiValueDict.ContainsKey("key1");

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void ContainsKey_ReturnsFalseIfKeyDoesNotExist()
        {
            // Act
            bool contains = _multiValueDict.ContainsKey("key99");

            // Assert
            Assert.False(contains);
        }

        [Fact]
        public void ContainsValue_ReturnsTrueIfKeyContainsValue()
        {
            // Arrange
            _multiValueDict.Add("key1", 1);

            // Act
            bool contains = _multiValueDict.ContainsValue("key1", 1);

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void ContainsValue_ReturnsFalseIfKeyDoesNotContainValue()
        {
            // Arrange
            _multiValueDict.Add("key1", 1);

            // Act
            bool contains = _multiValueDict.ContainsValue("key1", 99); // Valor inexistente

            // Assert
            Assert.False(contains);
        }

        [Fact]
        public void Clear_RemovesAllKeysAndValues()
        {
            // Arrange
            _multiValueDict.Add("key1", 1);
            _multiValueDict.Add("key2", 2);

            // Act
            _multiValueDict.Clear();

            // Assert
            Assert.False(_multiValueDict.ContainsKey("key1"));
            Assert.False(_multiValueDict.ContainsKey("key2"));
        }

        [Fact]
        public void Union_WithTwoDictionaries_ReturnsCorrectUnion()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("A", 2);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>();
            dict2.Add("A", 2);
            dict2.Add("A", 3);
            dict2.Add("C", 4);

            // Act
            var result = dict1.Union(dict2);

            // Assert
            Assert.Equal(3, result.GetValues("A").Count); // A debe tener los valores 1, 2, 3
            Assert.Contains(1, result.GetValues("A"));
            Assert.Contains(2, result.GetValues("A"));
            Assert.Contains(3, result.GetValues("A"));

            Assert.Single(result.GetValues("B")); // B debe tener solo 3
            Assert.Contains(3, result.GetValues("B"));

            Assert.Single(result.GetValues("C")); // C debe tener solo 4
            Assert.Contains(4, result.GetValues("C"));
        }

        [Fact]
        public void Union_WithEmptyDictionaries_ReturnsEmptyDictionary()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            var dict2 = new MultiValueDictionary<string, int>();

            // Act
            var result = dict1.Union(dict2);

            // Assert
            Assert.Empty(result.GetKeys());
        }

        [Fact]
        public void Intersection_WithTwoDictionaries_ReturnsCorrectIntersection()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("A", 2);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>();
            dict2.Add("A", 2);
            dict2.Add("A", 3);
            dict2.Add("C", 4);

            // Act
            var result = dict1.Intersection(dict2);

            // Assert
            Assert.Single(result.GetValues("A")); // A debe tener solo 2 en común
            Assert.Contains(2, result.GetValues("A"));
            Assert.False(result.ContainsKey("B")); // B no está en la intersección
            Assert.False(result.ContainsKey("C")); // C no está en la intersección
        }

        [Fact]
        public void Intersection_WithNoCommonValues_ReturnsEmptyDictionary()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>();
            dict2.Add("A", 2);
            dict2.Add("C", 4);

            // Act
            var result = dict1.Intersection(dict2);

            // Assert
            Assert.Empty(result.GetKeys()); // No hay intersección
        }

        [Fact]
        public void Intersection_WithEmptyDictionaries_ReturnsEmptyDictionary()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            var dict2 = new MultiValueDictionary<string, int>();

            // Act
            var result = dict1.Intersection(dict2);

            // Assert
            Assert.Empty(result.GetKeys());
        }

        [Fact]
        public void Difference_WithTwoDictionaries_ReturnsCorrectDifference()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("A", 2);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>();
            dict2.Add("A", 2);
            dict2.Add("A", 3);
            dict2.Add("C", 4);

            // Act
            var result = dict1.Difference(dict2);

            // Assert
            Assert.Single(result.GetValues("A")); // A debe tener solo 1, ya que 2 está en dict2
            Assert.Contains(1, result.GetValues("A"));

            Assert.Single(result.GetValues("B")); // B debe mantenerse igual
            Assert.Contains(3, result.GetValues("B"));
            Assert.False(result.ContainsKey("C")); // C no debe aparecer
        }

        [Fact]
        public void Difference_WithNoCommonValues_ReturnsFirstDictionary()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>();
            dict2.Add("C", 2);

            // Act
            var result = dict1.Difference(dict2);

            // Assert
            Assert.Equal(1, result.GetValues("A").Count); // A debe tener 1
            Assert.Contains(1, result.GetValues("A"));

            Assert.Equal(1, result.GetValues("B").Count); // B debe tener 3
            Assert.Contains(3, result.GetValues("B"));
        }

        [Fact]
        public void Difference_WithEmptySecondDictionary_ReturnsFirstDictionary()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>(); // Vacío

            // Act
            var result = dict1.Difference(dict2);

            // Assert
            Assert.Equal(1, result.GetValues("A").Count);
            Assert.Equal(1, result.GetValues("B").Count);
            Assert.Contains(1, result.GetValues("A"));
            Assert.Contains(3, result.GetValues("B"));
        }

        [Fact]
        public void Union_WithEmptySecondDictionary_ReturnsFirstDictionary()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>(); // Vacío

            // Act
            var result = dict1.Union(dict2);

            // Assert
            Assert.Equal(1, result.GetValues("A").Count);
            Assert.Equal(1, result.GetValues("B").Count);
            Assert.Contains(1, result.GetValues("A"));
            Assert.Contains(3, result.GetValues("B"));
        }

        [Fact]
        public void Intersection_WithEmptySecondDictionary_ReturnsEmptyDictionary()
        {
            // Arrange
            var dict1 = new MultiValueDictionary<string, int>();
            dict1.Add("A", 1);
            dict1.Add("B", 3);

            var dict2 = new MultiValueDictionary<string, int>(); // Vacío

            // Act
            var result = dict1.Intersection(dict2);

            // Assert
            Assert.Empty(result.GetKeys());
        }
    }
}