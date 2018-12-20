using HinxCor.Unity.UI;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using HinxCor.Common;

public class UserNumberBox : UIComponent
{
    private UnityEngine.UI.Text Text;
    public StringNumberType NumberType = StringNumberType.Integer;
    public Action<float> OnValueChanged;
    public float Value
    {
        get { return value; }
        set
        {
            if (this.value != value)
            {
                this.value = value;
                Display();
            }
        }
    }
    [FormerlySerializedAs("m_RValue")]
    [SerializeField] private float value;
    [SerializeField] private float Step;

    protected override void Start()
    {
        var add = this.Get<Button>("add");
        var reduce = this.Get<Button>("reduce");
        Text = GetComponentInChildren<UnityEngine.UI.Text>();
        add.onClick.AddListener(StepUp);
        reduce.onClick.AddListener(StepDown);
        Display();
    }

    public void StepUp()
    {
        Value += Step;
        Display();
    }

    public void StepDown()
    {
        Value -= Step;
        Display();
    }

    private void Display()
    {
        OnValueChanged.TryInvoke(value);
        Text.text = this.ToString();
        //switch (NumberType)
        //{
        //    case StringNumberType.Default:
        //        Text.text = Value.ToString();
        //        break;
        //    case StringNumberType.Percentage:
        //        Text.text = (int)(Value * 100f) + "%";
        //        break;
        //    case StringNumberType.Integer:
        //        Text.text = (int)(Value) + "";
        //        break;
        //    case StringNumberType.Decimal:
        //        Text.text = Value.ToString();
        //        break;
        //    case StringNumberType.Decimal_2F:
        //        Text.text = Value.ToString("F2");
        //        break;
        //    case StringNumberType.Hexadecimal:
        //        Text.text = Value.ToString("2X");
        //        break;
        //    case StringNumberType.Degree:
        //        if (Value < 0) Value += 360;
        //        Value = Value % 360;
        //        Text.text = Value.ToString("F1") + "°";
        //        break;
        //    default:
        //        Text.text = Value.ToString();
        //        break;
        //}
    }

    public override string ToString()
    {
        switch (NumberType)
        {
            case StringNumberType.Default:
                return Value.ToString();
            case StringNumberType.Percentage:
                return (int)(Value * 100f) + "%";
            case StringNumberType.Integer:
                return (int)(Value) + "";
            case StringNumberType.Decimal:
                return Value.ToString();
            case StringNumberType.Decimal_2F:
                return Value.ToString("F2");
            case StringNumberType.Hexadecimal:
                return Value.ToString("2X");
            case StringNumberType.Degree:
                if (Value < 0) Value += 360;
                Value = Value % 360;
                return Value.ToString("F1") + "°";
            default:
                return Value.ToString();
        }
    }

    public enum StringNumberType
    {
        Default,
        Percentage,
        Integer,
        Decimal,
        Decimal_2F,
        Hexadecimal,
        Degree
    }

}
