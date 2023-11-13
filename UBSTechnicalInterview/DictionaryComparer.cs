using System;
using System.Runtime.CompilerServices;

namespace GenericsAndLINQ
{
    public static class DictionaryComparer
	{
        public record ReturnInfo(int ReturnCode, string ReturnMessage);

        public static class ReturnCodes
        {
            public static ReturnInfo SameInstance { get { return new ReturnInfo(2, "Same Instance"); } }
            public static ReturnInfo SameContents { get { return new ReturnInfo(1, "Same Contents"); } }
            public static ReturnInfo NullDictionary { get { return new ReturnInfo(-1, "Null Dictionary"); } }
            public static ReturnInfo DifferentDictionaryTypes { get { return new ReturnInfo(-2, "Different Dictionary Types"); } }
            public static ReturnInfo DifferentItemCounts { get { return new ReturnInfo(-3, "Different Item Counts"); } }
            public static ReturnInfo DifferentContents { get { return new ReturnInfo(-4, "Different Contents"); } }
            public static ReturnInfo NonEquitableClass { get { return new ReturnInfo(-5, "Non-Equitable Object Type"); } }
        }

        public static bool CheckEquality<T1, T2>(T1 dict1, T2 dict2, out ReturnInfo returnCode)
        {
            // Check whether either dictionary is null
            if (dict1 == null || dict2 == null)
            {
                returnCode = ReturnCodes.NullDictionary;
                return false;
            }

            // Check whether dictionaries are the same instance
            if (dict1.Equals(dict2))
            {
                returnCode = ReturnCodes.SameInstance;
                return true;
            }

            // Check whether dictionary key and value types are the same
            Type[] dict1KeyValueTypes = dict1.GetType().GetGenericArguments();
            Type[] dict2KeyValueTypes = dict2.GetType().GetGenericArguments();

            if (dict1KeyValueTypes[0] != dict2KeyValueTypes[0] || dict1KeyValueTypes[1] != dict2KeyValueTypes[1])
            {
                returnCode = ReturnCodes.DifferentDictionaryTypes;
                return false;
            }

            // Check whether key and value types are equitable
            // i.e. are either built-in (System) classes OR implement IEquitable interface
            bool isKeyEquitable = dict1KeyValueTypes[0].GetInterfaces().Any(x =>
                  x.IsGenericType &&
                  x.GetGenericTypeDefinition() == typeof(IEquatable<>));

            bool isValueEquitable = dict1KeyValueTypes[1].GetInterfaces().Any(x =>
                  x.IsGenericType &&
                  x.GetGenericTypeDefinition() == typeof(IEquatable<>));

            if(!(isKeyEquitable && isValueEquitable))
            {
                returnCode = ReturnCodes.NonEquitableClass;
                return false;
            }

            // Create concrete dictionary types for further processing
            Type dictType = typeof(Dictionary<,>).MakeGenericType(dict1KeyValueTypes);
            var concreteDict1 = Convert.ChangeType(dict1, dictType);
            var concreteDict2 = Convert.ChangeType(dict2, dictType);

            // Check whether dictionaries are the same size
            int dict1Count = (int)concreteDict1.GetType().GetProperty("Count").GetValue(concreteDict1);
            int dict2Count = (int)concreteDict2.GetType().GetProperty("Count").GetValue(concreteDict2);

            if (dict1Count != dict2Count)
            {
                returnCode = ReturnCodes.DifferentItemCounts;
                return false;
            }

            Type kvpType = typeof(KeyValuePair<,>).MakeGenericType(dict1KeyValueTypes);
            var kvp2Value = Int32.MinValue;

            // Check whether dictionaries have the same contents
            foreach (var kvp1 in (System.Collections.IEnumerable)concreteDict1)
            {
                var concreteKvp1 = Convert.ChangeType(kvp1, kvpType);
                var kvp1Key = concreteKvp1.GetType().GetProperty("Key").GetValue(concreteKvp1);
                var kvp1Value = concreteKvp1.GetType().GetProperty("Value").GetValue(concreteKvp1);

                object[] kvp2SearchParams = new object[] { kvp1Key, null };
                bool result = (bool)concreteDict2.GetType().GetMethod("TryGetValue").Invoke(concreteDict2, kvp2SearchParams);

                if (!result || !kvp1Value.Equals(kvp2SearchParams[1]))
                {
                    returnCode = ReturnCodes.DifferentContents;
                    return false;
                }
            }

            returnCode = ReturnCodes.SameContents;
            return true;
        }

        public static bool CheckEquality(Dictionary<int, int> dictionary1, Dictionary<int, int> dictionary2)
        {
            if (dictionary1.Equals(dictionary2))
            {
                return true;
            }
            else if (dictionary1.Count != dictionary2.Count)
            {
                return false;
            }
            else
            {
                foreach (KeyValuePair<int, int> kvp1 in dictionary1)
                {
                    int kvp2Value = Int32.MinValue;

                    if (!dictionary2.TryGetValue(kvp1.Key, out kvp2Value) || kvp2Value != kvp1.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}