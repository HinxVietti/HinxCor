using UnityEngine;

namespace HinxCor.demo.ui.p_01f0
{
    public class Demo : MonoBehaviour
    {
        private void Start()
        {
            //get component.
            FruitsList ListCOM = GetComponent<FruitsList>();

            //prepare data
            var fs = new Fruit[5];
            fs[0] = new Fruit() { Name = "Apple", Price = 1.25f, disCount = 0.5f };
            fs[1] = new Fruit() { Name = "Orange", Price = 2.25f, disCount = 0.35f };
            fs[2] = new Fruit() { Name = "Watermelon", Price = 3.25f, disCount = 0.5f };
            fs[3] = new Fruit() { Name = "Pineapple", Price = 4.99f, disCount = 0.5f };
            fs[4] = new Fruit() { Name = "Banana", Price = 1.25f, disCount = 0.75f };

            FruitItemData data = new FruitItemData();
            data.Array = new FruitData[5];
            for (int i = 0; i < 5; i++)
                data.Array[i] = new FruitData(fs[i]);

            //get start.
            ListCOM.Init(data);
        }
    }
}
