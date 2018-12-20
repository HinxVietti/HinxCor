using HinxCor.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace HinxCor.Json
{

    /// <summary>
    /// 每次序列化之前，请先CLC，否则将保存以前已经序列化过的东西（直接返回fID）
    /// </summary>
    public class MiniJsonForUnity
    {
        private static Type _t_u_Gameobject;
        private static Type _t_u_Transform;
        private static Type _t_s_ObsoluteAttribute;
        private static Type t_Unity_GameObject { get { if (_t_u_Gameobject == null) _t_u_Gameobject = typeof(UnityEngine.GameObject); return _t_u_Gameobject; } }
        private static Type t_Unity_Transform { get { if (_t_u_Transform == null) _t_u_Transform = typeof(UnityEngine.Transform); return _t_u_Transform; } }
        private static Type t_System_ObsoluteAttribute { get { if (_t_s_ObsoluteAttribute == null) _t_s_ObsoluteAttribute = typeof(System.ObsoleteAttribute); return _t_s_ObsoluteAttribute; } }

        private Dictionary<int, KeyValuePair<string, string>> components;

        public MiniJsonForUnity()
        {
            components = new Dictionary<int, KeyValuePair<string, string>>();
        }

        public void CLC()
        {
            components = new Dictionary<int, KeyValuePair<string, string>>();
        }

        public string Serialize(object com)
        {
#pragma warning disable 219
            int hash = SerializeComponent(com);
#pragma warning restore 219
            //#if UNITY_EDITOR
            //            UnityEngine.Debug.Log(hash);
            //#endif
            StringBuilder sb = new StringBuilder();
            foreach (var item in components)
            {
                sb.AppendLine("--fID:" + item.Key);
                sb.AppendLine(item.Value.Key + ": ");
                sb.AppendLine(item.Value.Value);
            }
            return sb.ToString();
        }

        public string SerializeObject(object obj)
        {
            return obj.ToString();
        }

        public int SerializeComponent(object ori)
        {
            if (ori == null) return 0;
            int comHash = ori.GetHashCode();

            if (components.ContainsKey(comHash) == false)
            {
                components.Add(comHash, new KeyValuePair<string, string>());

                var t = ori.GetType();
                var allProperties = t.GetProperties();
                var allFields = t.GetFields();
                StringBuilder sb = new StringBuilder();
                if ((ori as Transform) == null)
                {
                    foreach (var property in allProperties)
                    {
                        if (!IsObsolute(property.GetCustomAttributes(true)) && IsGetSetAble(property))
                            sb.AppendLine("\t" + SerializePropertyInfo(property, ori));
                    }
                    foreach (var field in allFields)
                    {
                        if (!IsObsolute(field.GetCustomAttributes(true)))
                            sb.AppendLine("\t" + SerializeFieldInfo(field, ori));
                    }
                }
                else
                {
                    string pattern = new SerializeTransform().Serialize(ori as Transform);
                    sb.Append(pattern);
                }
                components[comHash] = new KeyValuePair<string, string>(t.Name, sb.ToString());
            }
            return comHash;
        }

        private static bool IsGetSetAble(PropertyInfo property)
        {
            return property.CanRead && property.CanRead;
        }
        private static bool IsObsolute(params object[] v)
        {
            foreach (var item in v)
            {
                if (item.GetType().Equals(t_System_ObsoluteAttribute)) return true;
            }
            return false;
        }

        private string SerializeFieldInfo(FieldInfo field, object obj)
        {
            var fieldt = field.FieldType;
            if (fieldt.IsValueType)
            {
                return field.Name + ": " + field.GetValue(obj);
            }
            else if (IsComponent(fieldt))
            {
                return field.Name + ": " + SerializeComponent(field.GetValue(obj));
            }
            return field.Name + ": " + SerializeObject(field.GetValue(obj));
        }

        private string SerializePropertyInfo(PropertyInfo property, object obj)
        {
            var fieldt = property.PropertyType;
            if (fieldt.IsValueType)
            {
                return property.Name + ": " + property.GetValue(obj, null);
            }
            else if (IsComponent(fieldt))
            {
                return property.Name + ": " + SerializeComponent(property.GetValue(obj, null));
            }
            return property.Name + ": " + SerializeObject(property.GetValue(obj, null));
        }

        private static bool IsComponent(Type t)
        {
            //Component
            int weight = 0;
            var properties = t.GetProperties();
            foreach (var item in properties)
            {
                if (item.Name == "gameObject" && item.PropertyType.Equals(t_Unity_GameObject)) weight++;
                if (item.Name == "transform" && item.PropertyType.Equals(t_Unity_Transform)) weight++;
                if (weight >= 2) return true;
            }
            return false;
        }
        public static string SerializeComponentsAppends()
        {
            return "[]";
        }

        public static string SerializeChildrenAppends(Transform ori)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ori.childCount; i++)
            {
                sb.AppendLine("\t---!child fID:" + ori.GetChild(i).GetHashCode());
            }
            if (string.IsNullOrEmpty(sb.ToString()))
                return "[]";
            return "\n" + sb.ToString().TrimEnd();
        }
    }
}
