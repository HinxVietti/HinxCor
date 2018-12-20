using HinxCor.Unity.UI.InfinityScrollList;
using UnityEngine.UI;

namespace HinxCor.demo.ui
{
    //single binder item.
    public class FruitsDataBinder : DataBinder
    {
        public Text Name, Price, Discount;

        //you must overide this method.
        public override void BindData<T>(ItemData<T> obj, float offset)
        {
            base.BindData(obj, offset);//and you must super this method.
            //your initial
            var fruit = obj as FruitData;
            Name.text = fruit.Name;
            Price.text = fruit.Price;
            Discount.text = fruit.Discount;
            name = fruit.Name + " item";
        }

    }

    //the root data of binder.T is you data Type
    internal class FruitItemData : ArrayItemData<Fruit>
    {
        // your code
    }

    //T is you data Type
    internal class FruitData : ItemData<Fruit>
    {
        // your code

        //sample.
        public FruitData(Fruit f)
        {
            data = f;
        }

        public string Name { get { return data.Name; } }
        public string Price { get { return "$:" + data.Price.ToString("F"); } }
        public string Discount { get { return "NOW " + (int)(data.disCount * 100) + "% OFF"; } }

        public static implicit operator FruitData(Fruit f)
        {
            return new FruitData(f);
        }
    }
}
