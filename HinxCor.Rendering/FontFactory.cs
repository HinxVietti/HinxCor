using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace HinxCor.Rendering
{
    /// <summary>
    /// 字体工厂
    /// </summary>
    public class FontFactory
    {
        /// <summary>
        /// ~构造
        /// </summary>
        public FontFactory()
        { }

        /// <summary>
        /// 用户字体集合
        /// </summary>
        public static PrivateFontCollection UserFontCollection { get; set; }
        /// <summary>
        /// 用户字体表
        /// </summary>
        public static Dictionary<string, FontFamily> LocalFontMap { get; set; }
        /// <summary>
        /// 系统字体表
        /// </summary>
        public static Dictionary<string, FontFamily> SystemFontMap { get; set; }

        private static Dictionary<string, Font> m_fontCollect { get; set; }

        private static string userFontDir = @"UserData\Fonts";
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            GeneratePrivateFontCollection(userFontDir);
            SystemFontMap = new Dictionary<string, FontFamily>();
            m_fontCollect = new Dictionary<string, Font>();

            InstalledFontCollection systemfonts = new InstalledFontCollection();
            var allsystemfonts = systemfonts.Families;
            for (int i = 0; i < allsystemfonts.Length; i++)
                SystemFontMap.Add(allsystemfonts[i].Name, allsystemfonts[i]);

        }

        /// <summary>
        /// 刷新本地字体列表
        /// </summary>
        /// <param name="path"></param>
        public static void GeneratePrivateFontCollection(string path)
        {
            userFontDir = path;
            UserFontCollection = new PrivateFontCollection();
            LocalFontMap = new Dictionary<string, FontFamily>();
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                var files = dir.GetFiles();
                foreach (var file in files)
                {
                    if (file.Extension == ".ttf" || file.Extension == ".ttc")
                        UserFontCollection.AddFontFile(file.FullName);
                }
            }
        }

        private static bool isFamilyInRecord(string familyName)
        {
            return SystemFontMap.ContainsKey(familyName) || LocalFontMap.ContainsKey(familyName);
        }

        private static Font getDefaultFont()
        {
            return new Font("微软雅黑", 24);
        }

        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="familyName">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">绘制样式</param>
        /// <param name="gdiCharSet">gdi char set</param>
        /// <param name="gdiVerticalFont">是否垂直排版</param>
        /// <returns></returns>
        public static Font GetFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            Font font;
            if (m_fontCollect.ContainsKey(familyName))
            {
                font = m_fontCollect[familyName];
                if (font.Size == emSize && font.Style == style && font.Unit == unit && font.GdiVerticalFont == gdiVerticalFont) return font;
            }
            if (isFamilyInRecord(familyName))
                font = new Font(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="family">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">绘制样式</param>
        /// <param name="gdiCharSet">gdi char set</param>
        /// <param name="gdiVerticalFont">是否垂直排版</param>
        /// <returns></returns>
        public static Font GetFont(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            Font font;
            if (m_fontCollect.ContainsKey(family.Name))
            {
                font = m_fontCollect[family.Name];
                if (font.Size == emSize && font.Style == style && font.Unit == unit && font.GdiVerticalFont == gdiVerticalFont) return font;
            }
            if (isFamilyInRecord(family.Name))
                font = new Font(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="family">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">绘制样式</param>
        /// <param name="gdiCharSet">gdi char set</param>
        /// <returns></returns>
        public static Font GetFont(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            Font font;
            if (m_fontCollect.ContainsKey(family.Name))
            {
                font = m_fontCollect[family.Name];
                if (font.Size == emSize && font.Style == style && font.Unit == unit && font.GdiCharSet == gdiCharSet) return font;
            }
            if (isFamilyInRecord(family.Name))
                font = new Font(family, emSize, style, unit, gdiCharSet);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="familyName">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">绘制样式</param>
        /// <param name="gdiCharSet">gdi char set</param>
        /// <returns></returns>
        public static Font GetFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            //return default(Font);
            Font font;
            if (m_fontCollect.ContainsKey(familyName))
            {
                font = m_fontCollect[familyName];
                if (font.Size == emSize && font.Style == style && font.Unit == unit && font.GdiCharSet == gdiCharSet) return font;
            }
            if (isFamilyInRecord(familyName))
                font = new Font(familyName, emSize, style, unit, gdiCharSet);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="family">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">绘制样式</param>
        /// <returns></returns>
        public static Font GetFont(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
        {
            Font font;
            if (m_fontCollect.ContainsKey(family.Name))
            {
                font = m_fontCollect[family.Name];
                if (font.Size == emSize && font.Style == style && font.Unit == unit) return font;
            }
            if (isFamilyInRecord(family.Name))
                font = new Font(family, emSize, style, unit);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary> 
        /// <param name="family">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <returns></returns>
        public static Font GetFont(FontFamily family, float emSize, FontStyle style)
        {

            Font font;
            if (m_fontCollect.ContainsKey(family.Name))
            {
                font = m_fontCollect[family.Name];
                if (font.Size == emSize && font.Style == style) return font;
            }
            if (isFamilyInRecord(family.Name))
                font = new Font(family, emSize, style);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="family">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="unit">绘制样式</param>
        /// <returns></returns>
        public static Font GetFont(FontFamily family, float emSize, GraphicsUnit unit)
        {

            Font font;
            if (m_fontCollect.ContainsKey(family.Name))
            {
                font = m_fontCollect[family.Name];
                if (font.Size == emSize && font.Unit == unit) return font;
            }
            if (isFamilyInRecord(family.Name))
                font = new Font(family, emSize, unit);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="familyName">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <returns></returns>
        public static Font GetFont(string familyName, float emSize, FontStyle style)
        {

            Font font;
            if (m_fontCollect.ContainsKey(familyName))
            {
                font = m_fontCollect[familyName];
                if (font.Size == emSize && font.Style == style) return font;
            }
            if (isFamilyInRecord(familyName))
                font = new Font(familyName, emSize, style);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="familyName">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="unit">绘制样式</param>
        /// <returns></returns>
        public static Font GetFont(string familyName, float emSize, GraphicsUnit unit)
        {
            Font font;
            if (m_fontCollect.ContainsKey(familyName))
            {
                font = m_fontCollect[familyName];
                if (font.Size == emSize && font.Unit == unit) return font;
            }
            if (isFamilyInRecord(familyName))
                font = new Font(familyName, emSize, unit);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="family">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <returns></returns>
        public static Font GetFont(FontFamily family, float emSize)
        {
            Font font;
            if (m_fontCollect.ContainsKey(family.Name))
            {
                font = m_fontCollect[family.Name];
                if (font.Size == emSize) return font;
            }
            if (isFamilyInRecord(family.Name))
                font = new Font(family.Name, emSize);
            else font = getDefaultFont();
            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="prototype">原有字体</param>
        /// <param name="newStyle">新样式</param>
        /// <returns></returns>
        public static Font GetFont(Font prototype, FontStyle newStyle)
        {
            Font font;
            if (m_fontCollect.ContainsKey(prototype.Name))
            {
                font = m_fontCollect[prototype.Name];
                if (font.Size == prototype.Size && font.Style == newStyle) return font;
            }
            if (isFamilyInRecord(prototype.Name))
                font = new Font(prototype, newStyle);
            else font = getDefaultFont();

            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns></returns>
        public static Font GetFont(string fontName, int fontSize)
        {
            Font font;
            if (m_fontCollect.ContainsKey(fontName))
            {
                font = m_fontCollect[fontName];
                if (font.Size == fontSize) return font;
            }
            if (isFamilyInRecord(fontName))
                font = new Font(fontName, fontSize);
            else font = getDefaultFont();

            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="familyName">字体名称</param>
        /// <param name="emSize">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">绘制样式</param>
        /// <returns></returns>
        public static Font GetFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
        {
            Font font;
            if (m_fontCollect.ContainsKey(familyName))
            {
                font = m_fontCollect[familyName];
                if (font.Size == emSize && font.Style == style && font.Unit == unit) return font;
            }
            if (isFamilyInRecord(familyName))
                font = new Font(familyName, emSize, style, unit);
            else font = getDefaultFont();

            if (!m_fontCollect.ContainsKey(font.Name)) m_fontCollect.Add(font.Name, font); else m_fontCollect[font.Name] = font;
            return font;
        }
        /// <summary>
        /// 获取字体文件
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="multipleSelect"></param>
        /// <returns></returns>
        [Obsolete]
        public static bool GetFontDialogResult(out object arg, bool multipleSelect = false)
        {
            arg = "";
            return false;
        }
        /// <summary>
        /// 导入字体
        /// </summary>
        /// <param name="multipleSelect"></param>
        /// <returns></returns>
        public static bool ImportFont(bool multipleSelect = false)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Multiselect = multipleSelect;
            f.Filter = "(.ttf)字体文件|*.ttf|(.ttc)字体文件|*.ttc|全部文件|*.*";
            f.FilterIndex = 1;
            if (f.ShowDialog() == DialogResult.OK)
            {
                var files = f.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fi = new FileInfo(files[i]);
                    if (fi.Extension == ".ttf" || fi.Extension == ".ttc")
                    {
                        File.Copy(fi.FullName, userFontDir + "/" + fi.Name);
                        UserFontCollection.AddFontFile(userFontDir + "/" + fi.Name);
                    }
                }
                return true;
            }
            return false;
        }
    }
}