using GenericsAndLINQ;

Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
Dictionary<int, int> dictionary2 = new Dictionary<int, int>();



dictionary1.Add(1, 0);
dictionary1.Add(4, 0);
dictionary1.Add(5, 4);
dictionary1.Add(7, 2);
dictionary1.Add(10, -3);

// Dictionaries are physically the same
Console.WriteLine(DictionaryComparer.CheckEquality(dictionary1, dictionary1));

// Dictionaries have different counts
dictionary2.Add(0, 0);
dictionary2.Add(1, 0);
dictionary2.Add(4, 0);
dictionary2.Add(5, 4);
dictionary2.Add(7, 2);

Console.WriteLine(DictionaryComparer.CheckEquality(dictionary1, dictionary2));

// Dictionaries have the same count and same kvps
dictionary2.Add(10, -3);
Console.WriteLine(DictionaryComparer.CheckEquality(dictionary1, dictionary2));

// Change this entry dictionary1.Add(0, 0); to entry dictionary1.Add(2, 0)
dictionary1.Remove(0);
dictionary1.Add(2,0);

foreach (KeyValuePair<int,int> kvp in dictionary1)
{
    Console.WriteLine($"Key: {kvp.Key} Value: {kvp.Value}");
}

Console.WriteLine(DictionaryComparer.CheckEquality(dictionary1, dictionary2));



//dictionary2.Add(0, 0);
//dictionary2.Add(1, 0);
//dictionary2.Add(2, 3);
//dictionary2.Add(4, 0);
//dictionary2.Add(5, 4);
//dictionary2.Add(7, 2);
//dictionary2.Add(10, -3);