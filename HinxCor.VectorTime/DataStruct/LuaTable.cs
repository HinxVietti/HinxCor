using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

using TKey = System.Int32;
//using TValue = System.Collections.Generic.List<System.Action>;

public class LuaTable<TValue> : IDictionary<TKey, TValue> where TValue : new()
{
    private Dictionary<int, TValue> data = new Dictionary<TKey, TValue>();

    public TValue this[int key]
    {
        get
        {
            if (ContainsKey(key) == false)
                data.Add(key, new TValue());
            return data[key];
        }
        set
        {
            if (ContainsKey(key) == false)
                data.Add(key, new TValue());
            data[key] = value;
        }
    }

    public ICollection<int> Keys => data.Keys;

    public ICollection<TValue> Values => data.Values;

    public int Count => data.Count;

    public bool IsReadOnly => false;

    public void Add(int key, TValue value) => data.Add(key, value);

    public void Add(KeyValuePair<int, TValue> item) => Add(item.Key, item.Value);

    public void Clear() => data.Clear();

    public bool Contains(KeyValuePair<int, TValue> item) => data.ContainsKey(item.Key) && data[item.Key].Equals(item.Value);

    public bool ContainsKey(int key) => data.ContainsKey(key);

    public void CopyTo(KeyValuePair<int, TValue>[] array, int arrayIndex = 0) => throw new Exception();

    public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator() => data.GetEnumerator();

    public bool Remove(int key) => data.Remove(key);

    public bool Remove(KeyValuePair<int, TValue> item) => data.Remove(item.Key);

    public bool TryGetValue(int key, out TValue value) => data.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() => data.GetEnumerator();
}

