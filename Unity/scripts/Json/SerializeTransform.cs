using UnityEngine;
using HinxCor.Json;
using System.Text;
using System.IO;

public class SerializeTransform : ISerializePattern<Transform>
{
    public string Serialize(Transform obj)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\tlocalPosition: " + obj.localPosition);
            sb.AppendLine("\tlocalRotatoin: " + obj.localRotation);
            sb.AppendLine("\tlocalScale: " + obj.localScale);
            sb.AppendLine("\tFather: " + (obj.parent == null ? 0 : obj.parent.GetHashCode()));
            var childres = MiniJsonForUnity.SerializeChildrenAppends(obj);
            if (childres != "[]")
                sb.AppendLine("\tChildren: ");
            else sb.Append("\tChildren: ");
            sb.AppendLine(childres);
            sb.AppendLine("\tgameObject: " + obj.gameObject.GetHashCode());
            sb.AppendLine("\tobjectHideFlags: " + (int)obj.hideFlags);

            return sb.ToString();
        }
        catch
        {
            return "{}";
        }
    }

    public void Deserialize(ref Transform target, string json)
    {
        StringReader read = new StringReader(json);
        read.ReadLine();
    }

    private void dosreial(string kvp, ref Transform ori)
    {
        kvp = kvp.Trim();
        var args = kvp.Split(':');
        if (args.Length != 2) return;
        switch (args[0])
        {
            case "localPosition":
                ori.localPosition = toVe3(args[1]);
                break;
            case "localRotation":
                ori.localRotation = toQuat(args[1]);
                break;
            case "localScale:":
                ori.localScale = toVe3(args[1]);
                break;
            default:
                break;
        }
    }

    private Vector3 toVe3(string ori)
    {
        return new Vector3();
    }
    private Quaternion toQuat(string ori)
    {
        return new Quaternion();
    }
}



