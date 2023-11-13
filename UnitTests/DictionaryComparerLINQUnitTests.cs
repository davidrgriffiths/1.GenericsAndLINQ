using System;
using GenericsAndLINQ;
using static GenericsAndLINQ.DictionaryComparer;

namespace UnitTests
{
	public class DictionaryComparerLINQUnitTests
	{
        [Test]
        public static void CompareIntIntDictionariesSameCountDifferentContents()
        {
            Dictionary<int, int> d1 = new Dictionary<int, int> { { 1, 1 } };
            Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 2 } };

            // Expect INequality
            Assert.IsTrue(!DictionaryComparer.CheckEquality(d1, d2));
        }

        [Test]
        public void CompareIntIntDictionariesWithDifferentItemCounts()
        {
            Dictionary<int, int> d1 = new Dictionary<int, int> { { 1, 1 } };
            Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };

            // Expect INequality
            Assert.IsTrue(!DictionaryComparer.CheckEquality(d1, d2));
        }

        [Test]
        public void CompareSameInstance()
        {
            Dictionary<int, int> d1 = new Dictionary<int, int> { { 1, 1 } };

            // Expect equality
            Assert.IsTrue(DictionaryComparer.CheckEquality(d1, d1));
        }
    }
}