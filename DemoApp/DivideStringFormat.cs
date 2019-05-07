namespace Test
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class TMP_InputField
    {
        public int selectionStringFocusPosition;
        public int selectionStringAnchorPosition;
        public string text;
    }

    public class DivideStringFormat
    {
        internal const string EmptyStringPattern = @"^[ ]*$";
        internal const string tagPattern = @"<[.a-zA-Z0-9_=/]*>";

        public enum TagType
        {
            Head,
            Full,
            Foot
        }

        public class Tag
        {
            public TagType Type { get; set; }

            public string TagName { get; set; }

            public object Value { get; set; }

            public Tag()
            {
                Type = TagType.Full;
                TagName = string.Empty;
                Value = string.Empty;
            }

            public Tag(string tgname)
            {
                Type = TagType.Full;
                TagName = tgname;
                Value = string.Empty;
            }
            public Tag(string tgname, object tgvalue)
            {
                Type = TagType.Full;
                TagName = tgname;
                Value = tgvalue;
            }

            public Tag Clone()
            {
                return new Tag()
                {
                    TagName = TagName,
                    Type = Type,
                    Value = Value
                };
            }

            public string GetHeader()
            {
                return string.Format("<{0}{1}>", TagName, GetValueString(Value));
            }

            public string GetFooter()
            {
                return string.Format("</{0}>", TagName);
            }

            public static string GetValueString(object value)
            {
                return value == null ?
                    string.Empty :
                    (string.IsNullOrEmpty(value.ToString()) ?
                        string.Empty :
                        ("=" + value.ToString()));
            }

            public static Tag From(string v)
            {
                Tag tag = new Tag();

                if (v.Contains("="))
                {
                    tag.Type = TagType.Head;
                    int stIndex = v.IndexOf('=');
                    var tmp = v;
                    v = v.Remove(stIndex, v.Length - stIndex);
                    v = v.Remove(0, 1);
                    tag.TagName = v;
                    tmp = tmp.Remove(0, stIndex + 1);
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                    tag.Value = tmp;
                    return tag;
                }
                if (v[1] == '/')
                {
                    tag.Type = TagType.Foot;
                    v = v.Remove(0, 2);
                    v = v.Remove(v.Length - 1, 1);
                    tag.TagName = v;
                    return tag;
                }
                v = v.Remove(v.Length - 1, 1);
                tag.TagName = v.Remove(0, 1);
                tag.Type = TagType.Head;
                return tag;
            }


            public static Tag Combine(Tag head, Tag foot)
            {
                if (head.Type != TagType.Head) return null;
                if (foot.Type != TagType.Foot) return null;
                if (head.TagName.Equals(foot.TagName) == false) return null;
                return new Tag()
                {
                    TagName = head.TagName,
                    Type = TagType.Full,
                    Value = head.Value
                };
            }
        }



        public static void Apply(string tag, TMP_InputField tMP_InputField, object value = null)
        {
            int start = tMP_InputField.selectionStringFocusPosition;
            int end = tMP_InputField.selectionStringAnchorPosition;
            if (start > end)
            {
                //交换位置
                start = start + end;
                end = start - end;
                start = start - end;
            }


            if (string.IsNullOrEmpty(tag))
                return;
            var splits = Split(tMP_InputField);
            if (splits[1].Length == 0 || Regex.IsMatch(splits[1], EmptyStringPattern))
                return;
            int oriLength = splits[1].Length;

            var tags = ScanTags(splits[1]/*, out bool special, out Tag fullTag1*/);

            var xmlTag = new Tag(tag, value);

            StringBuilder part2 = new StringBuilder();
            for (int i = 0; i < tags.Length; i++)
                if (tags[i].Type == TagType.Foot)
                {
                    part2.Append(string.Format(tags[i].GetFooter()));
                    part2.Append(string.Format(tags[i].GetHeader()));
                }

            //重新计算是否存在特殊标签
            bool special = false;
            special = IsStringSpecialAFullTag(splits[1], out Tag fullTag1);

            //若查询到的第一个tag和需要操作的tag一样,执行特殊处理
            if (special && fullTag1.TagName == tag)
                xmlTag = SpecialHandle(fullTag1, value);

            string plain = splits[1];
            if (special)
                plain = RemoveSpecialTag(splits[1]);

            if (xmlTag != null)
            {
                part2.Append(xmlTag.GetHeader());
                part2.Append(plain);
                part2.Append(xmlTag.GetFooter());
            }
            else
                //移除前后俩Tag
                //考虑到有空格,重整字符串
                part2.Append(plain);

            for (int i = 0; i < tags.Length; i++)
                if (tags[i].Type == TagType.Head)
                {
                    part2.Append(string.Format(tags[i].GetFooter()));
                    part2.Append(string.Format(tags[i].GetHeader()));
                }

            splits[1] = part2.ToString();

            tMP_InputField.selectionStringFocusPosition = start;
            tMP_InputField.selectionStringAnchorPosition = end + (splits[1].Length - oriLength);

            tMP_InputField.text = splits[0] + splits[1] + splits[2];
        }


        /// <summary>
        /// 移除文本前后的XML标签
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static string RemoveSpecialTag(string txt)
        {
            int lastIndex = txt.LastIndexOf("</");
            txt = txt.Remove(lastIndex, txt.Length - lastIndex);
            int findex = txt.IndexOf('>');
            txt = txt.Remove(0, findex + 1);
            return txt;
        }


        /// <summary>
        /// 如果文首文尾包含了特殊的Tag,返回一个 tag 和 true ,否则false 和nul
        /// </summary>
        /// <param name="v"></param>
        /// <param name="fullTag1"></param>
        /// <returns></returns>
        public static bool IsStringSpecialAFullTag(string v, out Tag fullTag1)
        {
            string HP = @"^<[.a-zA-Z0-9=]*>";
            Regex Header = new Regex(HP);
            string FP = @"<[/a-zA-Z]*>$";
            Regex Footer = new Regex(FP);
            fullTag1 = null;

            if (Header.IsMatch(v) && Footer.IsMatch(v))
            {
                var hM = Tag.From(Header.Match(v).ToString());
                var fM = Tag.From(Footer.Match(v).ToString());
                if (hM.TagName.Equals(fM.TagName))
                {
                    fullTag1 = hM;
                    return true;
                }
                return false;
            }
            return false;
        }

        private static Tag SpecialHandle(Tag tag, object value)
        {
            if (value == null) return tag;
            if (tag.Value == null) return null;//此类类似于<b></b>Tag
            if (tag.Value.ToString() == "false")
                tag.Value = true;
            if (tag.Value.ToString() == "true")
                tag.Value = false;
            tag.Value = value;
            return tag;//对tag重新赋值
        }


        /// <summary>
        /// 筛选出所有文本以内的标签
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static Tag[] ScanTags(string v/*, out bool isSpecial, out Tag fullTag1*/)
        {
            var Colle = Regex.Matches(v, tagPattern);
            var tags = new Tag[Colle.Count];
            for (int i = 0; i < tags.Length; i++)
                tags[i] = Tag.From(Colle[i].ToString());

            var restags = new List<Tag>();
            List<Tag> fullTag = new List<Tag>();
            for (int i = 0; i < tags.Length; i++)
                if (tags[i].Type == TagType.Head)
                    for (int j = i + 1; j < tags.Length; j++)//要从头部以后开始扫描
                        if (tags[j].Type == TagType.Foot)
                            if (tags[i].TagName.Equals(tags[j].TagName))
                                fullTag.Add(Tag.Combine(tags[i], tags[j]));
                            else
                                if (restags.Contains(tags[i]) == false) restags.Add(tags[i]);//添加未被匹配到的 head 标签加入

            bool found = false;
            for (int i = 0; i < tags.Length; i++)
                if (tags[i].Type == TagType.Foot)
                {
                    found = false;
                    for (int j = 0; j < fullTag.Count; j++)
                        if (fullTag[j].TagName.Equals(tags[i].TagName))
                        {
                            found = true;
                            break;
                        }
                    if (found == false) restags.Add(tags[i]);//全标签以外的foot标签加入
                }

            //restags.AddRange(fullTag);//全标签加入
            //try
            //{
            //    fullTag1 = fullTag[0];
            //}
            //catch
            //{
            //    fullTag1 = null;
            //}

            //try
            //{
            //    if (tags[0].Type == TagType.Head && tags[tags.Length - 1].Type == TagType.Foot)
            //        if (tags[0].TagName.Equals(fullTag[0].TagName) && tags[tags.Length - 1].TagName.Equals(fullTag[0].TagName))
            //            isSpecial = true;
            //        else isSpecial = false;
            //    else isSpecial = false;
            //}
            //catch
            //{
            //    isSpecial = false;
            //}

            return restags.ToArray();
        }

        internal static string[] Split(TMP_InputField tMP_InputField)
        {
            int start = tMP_InputField.selectionStringFocusPosition;
            int end = tMP_InputField.selectionStringAnchorPosition;
            if (start > end)
            {
                //交换位置
                start = start + end;
                end = start - end;
                start = start - end;
            }

            string istr = tMP_InputField.text;
            var F1 = istr.Remove(start, istr.Length - start);
            var F3 = istr.Remove(0, end);
            var F2_1 = istr.Remove(end, istr.Length - end);
            var F2 = F2_1.Remove(0, start);

            return new[] { F1, F2, F3 };
        }
    }


}

