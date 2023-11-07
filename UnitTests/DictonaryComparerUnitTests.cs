using UBSTechnicalInterview;
using static UBSTechnicalInterview.DictionaryComparer;

namespace UnitTests;

public class Tests
{
    public record TestRecord(int param1, int param2);

    [SetUp]
    public void Setup() { }

    #region Test Different Dictionary Types (simple data types)

    [Test]
    public void CompareIntStringIntIntDictionaries()
    {
        Dictionary<int, string> d1 = new Dictionary<int, string>{ { 1,"1"} };
        Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 1 } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality<Dictionary<int, string>, Dictionary<int, int>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentDictionaryTypes;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareStringStringIntIntDictionaries()
    {
        Dictionary<string, string> d1 = new Dictionary<string, string> { { "1", "1" } };
        Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 1 } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality<Dictionary<string, string>, Dictionary<int, int>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentDictionaryTypes;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareFloatStringDoubleBoolDictionaries()
    {
        Dictionary<float, string> d1 = new Dictionary<float, string> { { 1.23f, "Test" } };
        Dictionary<double, bool> d2 = new Dictionary<double, bool> { { 1.0d, true } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality<Dictionary<float, string>, Dictionary<double, bool>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentDictionaryTypes;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    #endregion

    #region Test Same Contents (simple data types)

    [Test]
    public void CompareIntIntDictionariesWithSameContents()
    {
        Dictionary<int, int> d1 = new Dictionary<int, int> { { 1, 1 } };
        Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 1 } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality<Dictionary<int, int>, Dictionary<int, int>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareIntStringDictionariesWithSameContents()
    {
        Dictionary<int, string> d1 = new Dictionary<int, string> { { 1, "Hello" } };
        Dictionary<int, string> d2 = new Dictionary<int, string> { { 1, "Hello" } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality<Dictionary<int, string>, Dictionary<int, string>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareStringBoolDictionariesWithSameContents()
    {
        Dictionary<string, bool> d1 = new Dictionary<string, bool> { { "Hello", true }, { "Goodbye", false } };
        Dictionary<string, bool> d2 = new Dictionary<string, bool> { { "Hello", true }, { "Goodbye", false } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality<Dictionary<string, bool>, Dictionary<string, bool>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareStringBoolDictionariesWithSameContentsDifferentOrder()
    {
        Dictionary<string, bool> d1 = new Dictionary<string, bool> { { "Hello", true }, { "Goodbye", false } };
        Dictionary<string, bool> d2 = new Dictionary<string, bool> { { "Goodbye", false }, { "Hello", true } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality<Dictionary<string, bool>, Dictionary<string, bool>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    #endregion

    #region Test Object Types (records)

    [Test]
    public void CompareIntRecordDictionariesWithSameContents()
    {
        Dictionary<int, TestRecord> d1 = new Dictionary<int, TestRecord> {
            { 1, new TestRecord(1, 2) } };
        Dictionary<int, TestRecord> d2 = new Dictionary<int, TestRecord> {
            { 1, new TestRecord(1, 2) } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality
            <Dictionary<int, TestRecord>, Dictionary<int, TestRecord>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareIntRecordDictionariesWithDifferentContents()
    {
        Dictionary<int, TestRecord> d1 = new Dictionary<int, TestRecord> {
            { 1, new TestRecord(1, 2) } };
        Dictionary<int, TestRecord> d2 = new Dictionary<int, TestRecord> {
            { 2, new TestRecord(1, 2) } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<int, TestRecord>, Dictionary<int, TestRecord>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareIntRecordDictionariesWithDifferentItemCounts()
    {
        Dictionary<int, TestRecord> d1 = new Dictionary<int, TestRecord> {
            { 1, new TestRecord(1, 2) } };
        Dictionary<int, TestRecord> d2 = new Dictionary<int, TestRecord> {
            { 1, new TestRecord(1, 2) }, { 2, new TestRecord(3, 4) } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<int, TestRecord>, Dictionary<int, TestRecord>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentItemCounts;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareRecordRecordDictionariesWithSameContents()
    {
        Dictionary<TestRecord, TestRecord> d1 = new Dictionary<TestRecord, TestRecord> {
            { new TestRecord(100, 1), new TestRecord(1, 2) } };
        Dictionary<TestRecord, TestRecord> d2 = new Dictionary<TestRecord, TestRecord> {
            { new TestRecord(100, 1), new TestRecord(1, 2) } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality
            <Dictionary<TestRecord, TestRecord>, Dictionary<TestRecord, TestRecord>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareRecordRecordDictionariesWithDifferentContents()
    {
        Dictionary<TestRecord, TestRecord> d1 = new Dictionary<TestRecord, TestRecord> {
            { new TestRecord(100, 1), new TestRecord(1, 2) } };
        Dictionary<TestRecord, TestRecord> d2 = new Dictionary<TestRecord, TestRecord> {
            { new TestRecord(101, 1), new TestRecord(5, 2) } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<TestRecord, TestRecord>, Dictionary<TestRecord, TestRecord>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    #endregion

    #region Compare Object Types (custom types implementing IEquatable interface)

    [Test]
    public void CompareIntObjectDictionariesWithSameContents()
    {
        Dictionary<int, EquatableClass> d1 = new Dictionary<int, EquatableClass> {
            { 1, new EquatableClass(1234, 1.23f, "Book", true) } };
        Dictionary<int, EquatableClass> d2 = new Dictionary<int, EquatableClass> {
            { 1, new EquatableClass(1234, 1.23f, "Book", true) } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality
            <Dictionary<int, EquatableClass>, Dictionary<int, EquatableClass>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareIntObjectDictionariesWithDifferentContents()
    {
        Dictionary<int, EquatableClass> d1 = new Dictionary<int, EquatableClass> {
            { 1, new EquatableClass(1234, 1.23f, "Book", true) } };
        Dictionary<int, EquatableClass> d2 = new Dictionary<int, EquatableClass> {
            { 1, new EquatableClass(1235, 1.23f, "Book", true) } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<int, EquatableClass>, Dictionary<int, EquatableClass>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareIntObjectDictionariesWithDifferentItemCounts()
    {
        Dictionary<int, EquatableClass> d1 = new Dictionary<int, EquatableClass> {
            { 1, new EquatableClass(1234, 1.23f, "Book", true) } };
        Dictionary<int, EquatableClass> d2 = new Dictionary<int, EquatableClass> {
            { 1, new EquatableClass(1235, 1.23f, "Book", true) } ,
            { 2, new EquatableClass(1235, 1.23f, "Book", true) } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<int, EquatableClass>, Dictionary<int, EquatableClass>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentItemCounts;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareObjectObjectDictionariesWithSameContents()
    {
        Dictionary<EquatableClass, EquatableClass> d1 = new Dictionary<EquatableClass, EquatableClass> {
            { new EquatableClass(1234, 1.23f, "Book", true), new EquatableClass(1234, 1.23f, "Book", true) } };
        Dictionary<EquatableClass, EquatableClass> d2 = new Dictionary<EquatableClass, EquatableClass> {
            { new EquatableClass(1234, 1.23f, "Book", true), new EquatableClass(1234, 1.23f, "Book", true) } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality
            <Dictionary<EquatableClass, EquatableClass>, Dictionary<EquatableClass, EquatableClass>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareObjectObjectDictionariesWithDifferentContents()
    {
        Dictionary<EquatableClass, EquatableClass> d1 = new Dictionary<EquatableClass, EquatableClass> {
            { new EquatableClass(1234, 1.23f, "Book", true), new EquatableClass(1234, 1.23f, "Book", true) } };
        Dictionary<EquatableClass, EquatableClass> d2 = new Dictionary<EquatableClass, EquatableClass> {
            { new EquatableClass(1234, 1.23f, "Book", true), new EquatableClass(1235, 1.23f, "Book", true) } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<EquatableClass, EquatableClass>, Dictionary<EquatableClass, EquatableClass>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareObjectObjectDictionariesWithDifferentItemCounts()
    {
        Dictionary<EquatableClass, EquatableClass> d1 = new Dictionary<EquatableClass, EquatableClass> {
            { new EquatableClass(1234, 1.23f, "Book", true), new EquatableClass(1234, 1.23f, "Book", true) } };
        Dictionary<EquatableClass, EquatableClass> d2 = new Dictionary<EquatableClass, EquatableClass> {
            { new EquatableClass(1234, 1.23f, "Book", true), new EquatableClass(1235, 1.23f, "Book", true) } ,
            { new EquatableClass(1236, 1.23f, "Book", true), new EquatableClass(1237, 1.23f, "Book", true) } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality<Dictionary
            <EquatableClass, EquatableClass>, Dictionary<EquatableClass, EquatableClass>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    #endregion

    [Test]
    public void CompareIntIntDictionariesWithDifferentContents()
    {
        Dictionary<int, int> d1 = new Dictionary<int, int> { { 1, 1 } };
        Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 2 } };

        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<int, int>, Dictionary<int, int>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentContents;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareIntIntDictionariesWithDifferentItemCounts()
    {
        Dictionary<int, int> d1 = new Dictionary<int, int> { { 1, 1 } };
        Dictionary<int, int> d2 = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality
            <Dictionary<int, int>, Dictionary<int, int>>(d1, d2, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.DifferentItemCounts;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareSameInstance()
    {
        Dictionary<int, string> d1 = new Dictionary<int, string> { { 1, "1" } };
        ReturnInfo returnCode;

        // Expect equality
        bool isCorrectEqualityResult = DictionaryComparer.CheckEquality
            <Dictionary<int, string>, Dictionary<int, string>>(d1, d1, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.SameInstance;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }

    [Test]
    public void CompareNullDictionary()
    {
        ReturnInfo returnCode;

        // Expect INequality
        bool isCorrectEqualityResult = !DictionaryComparer.CheckEquality<Dictionary
            <int, string>, Dictionary<int, string>>(null, null, out returnCode);
        bool isCorrectReturnCode = returnCode == ReturnCodes.NullDictionary;

        Assert.IsTrue(isCorrectEqualityResult && isCorrectReturnCode, returnCode.ReturnMessage);
    }
}