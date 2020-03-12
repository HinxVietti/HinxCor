using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HinxCor.Serialize
{

    public class Arguments : ICollection<string>
    {
        private Dictionary<string, List<string>> argscollect = new Dictionary<string, List<string>>();
        public List<string> Keys
        {
            get
            {
                List<string> kss = new List<string>();
                foreach (var item in argscollect.Keys)
                    kss.Add(item);
                return kss;
            }
        }

        public int Count { get { return argscollect.Keys.Count; } }

        public bool IsReadOnly { get { return false; } }

        public string[] this[string key]
        {
            get
            {
                if (!HasKey(key))
                    argscollect.Add(key, new List<string>());
                return argscollect[key].ToArray();
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                    AddArgument(key, value[i]);
            }
        }

        public string this[string key, int index]
        {
            get { return argscollect[key][index]; }
            set
            {
                argscollect[key][index] = value;
            }
        }


        public Arguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var kvp = GetKeyValuePare(args[i]);
                AddArgument(kvp);
            }
        }

        /// <summary>
        /// 获取键值对,按照第一个':'切割
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static KeyValuePair<string, string> GetKeyValuePare(string v)
        {
            var index = v.IndexOf(':');
            if (index == -1) return new KeyValuePair<string, string>("unknow", v);
            return new KeyValuePair<string, string>(v.Remove(index, v.Length - index), v.Remove(0, index + 1));
        }

        /// <summary>
        /// 从文本获取args
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Arguments CreateFrom(string v)
        {
            var args = Regex.Split(v, System.Environment.NewLine);
            return new Arguments(args);
        }

        /// <summary>
        /// 包装args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string PackArguments(List<string> args) => PackArguments(args.ToArray());
        /// <summary>
        /// 包装args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string PackArguments(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
                sb.Append(string.Format("\"{0}\" ", args[i])); 
            if (args.Length > 0) sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }


        [Obsolete("", true)]
        public void SetArgumentR(string key, string value)
        {
            //if (!argsDict.ContainsKey(key))
            //    argsDict.Add(key, string.Empty);
            //argsDict[key] = value;
        }
        [Obsolete("", true)]
        public string GetArgumentR(string keyName)
        {
            //if (argsDict.ContainsKey(keyName))
            //    return argsDict[keyName];
            return string.Empty;
        }


        public bool HasKey(string key)
        {
            return argscollect.ContainsKey(key);
        }


        /// <summary>
        /// 0(Normal)  -1(exist)  -2(error)
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        public int AddArgument(KeyValuePair<string, string> kvp)
        {
            return AddArgument(kvp.Key, kvp.Value);
        }

        /// <summary>
        /// 0(Normal)  -1(exist)  -2(error)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int AddArgument(string key, string value)
        {
            try
            {
                if (!argscollect.ContainsKey(key))
                    argscollect.Add(key, new List<string>());
                if (!argscollect[key].Contains(value))
                {
                    argscollect[key].Add(value);
                    return 0;
                }
                return -1;
            }
            catch
            {
                return -2;
            }
        }

        public List<string> GetArgumentList(string key)
        {
            return argscollect[key];
        }

        public string[] GetArguments(string key)
        {
            return this[key];
        }


        public string GetArgument(string key, int index = 0)
        {
            return this[key, index];
        }


        public void Add(string item)
        {
            var kvp = GetKeyValuePare(item);
            AddArgument(kvp);
        }

        public void Clear()
        {
            argscollect = new Dictionary<string, List<string>>();
        }

        public bool Contains(string item)
        {
            return HasKey(item);
        }

        [Obsolete("arguments show not using this method", true)]
        public void CopyTo(string[] array, int arrayIndex)
        {
            throw new InvalidOperationException(" argument show not using this method.");
        }

        public bool Remove(string item)
        {
            if (HasKey(item))
            {
                return
                argscollect.Remove(item);
            }
            return false;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return (IEnumerator<string>)Keys;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)argscollect;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in argscollect)
            {
                sb.Append(item.Key + ",");
                foreach (var lr in item.Value)
                    sb.AppendLine("\t" + lr);
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }
    }
}

