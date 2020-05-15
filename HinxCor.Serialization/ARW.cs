using System;
using System.Collections.Generic;
using System.Text;

namespace HinxCor.Serialization
{

    public class ARW
    {


        public static string ToArw(object obj)
        {
            var t = obj.GetType();
            var fields = t.GetFields();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(t.ToString());
            sb.AppendLine(string.Format("hash:{0}", obj.GetHashCode()));
            for (int i = 0; i < fields.Length; i++)
            {
                var attrObjs = fields[i].GetCustomAttributes(false);
                bool HasIgnore = false;
                foreach (var item in attrObjs)
                    if ((item as ARWIgnoreAttribute) != null)
                    {
                        HasIgnore = true;
                        break;
                    }
                if (HasIgnore)
                    continue;
                sb.AppendLine(fields[i].Name + ":");
                sb.AppendLine("\t-" + fields[i].GetValue(obj).ToString());
            }
            sb.Append("-''-o&#r'");

            return sb.ToString();
        }


    }
}
