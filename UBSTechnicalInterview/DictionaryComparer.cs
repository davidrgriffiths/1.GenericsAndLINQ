using System;
using System.Runtime.CompilerServices;

namespace UBSTechnicalInterview
{
	public class DictionaryComparer
	{
		public DictionaryComparer() { }

		public static bool Compare<T1,T2>(T1 dict1, T2 dict2)
        {
			return true;
		}


        public static bool Compare(Dictionary<int, int> dictionary1, Dictionary<int, int> dictionary2)
		{
            bool dictionariesAreTheSame = true;

			// Firstly, check to see if the dictionaries are the same instance

			if (dictionary1.Equals(dictionary2))
			{
				return true;
			}
			else if(dictionary1.Count == dictionary2.Count)
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