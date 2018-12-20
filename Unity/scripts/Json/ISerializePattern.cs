using UnityEngine;


public interface ISerializePattern<T>where T:Component
{
    string Serialize(T obj);
}
