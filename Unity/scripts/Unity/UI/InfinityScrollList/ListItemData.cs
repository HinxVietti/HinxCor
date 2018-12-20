using UnityEngine;

namespace HinxCor.Unity.UI.InfinityScrollList
{
    //public abstract class ListItemData
    //{
    //    public object[] Array;

    //    //public static implicit operator ArrayItemData(ListItemData<T> obj)
    //    //{
    //    //    return obj as ArrayItemData;
    //    //}
    //}


    public class ItemData<T>
    {
        public T data;
        public Vector2 position;
    }
    
    public class ArrayItemData<T> /*: ListItemData*//*<ItemData>*/
    {
        public ItemData<T>[] Array;
    }
}
