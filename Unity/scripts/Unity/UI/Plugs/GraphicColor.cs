using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class GraphicColor : MonoBehaviour
{
    private MaskableGraphic g;

    public void Set(Color color)
    {
        if (g == null) g = GetComponent<MaskableGraphic>();
        g.color = color;
    }
}
