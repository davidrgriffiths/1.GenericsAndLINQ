using UBSTechnicalInterview;

namespace UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CompareDictionariesOfDifferentTypes()
    {
        Dictionary<int, string> d1 = new Dictionary<int, string>{ { 1,"1"} };
        Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 1 } };

        if (DictionaryComparer.Compare<Dictionary<int, string>, Dictionary<int, int>>(d1, d2))
        {
            Assert.Fail();
        }
    }

    [Test]
    public void CompareDictionariesOfSameType()
    {
        Dictionary<int, int> d1 = new Dictionary<int, int> { { 1, 1 } };
        Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 1 } };

        if (DictionaryComparer.Compare<Dictionary<int, int>, Dictionary<int, int>>(d1, d2))
        {
            Assert.Pass();
        }
    }
}