using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HinxCor.Unity.UI
{

    public static class FontUpdateTracker
    {
        static Dictionary<UnityEngine.Font, HashSet<Text>> m_Tracked = new Dictionary<UnityEngine.Font, HashSet<Text>>();

        public static void TrackText(Text t)
        {
            if (t.font == null)
                return;

            HashSet<Text> exists;
            m_Tracked.TryGetValue(t.font, out exists);
            if (exists == null)
            {
                // The textureRebuilt event is global for all fonts, so we add our delegate the first time we register *any* Text
                if (m_Tracked.Count == 0)
                    UnityEngine.Font.textureRebuilt += RebuildForFont;

                exists = new HashSet<Text>();
                m_Tracked.Add(t.font, exists);
            }

            if (!exists.Contains(t))
                exists.Add(t);
        }

        private static void RebuildForFont(UnityEngine.Font f)
        {
            HashSet<Text> texts;
            m_Tracked.TryGetValue(f, out texts);

            if (texts == null)
                return;

            foreach (var text in texts)
                text.FontTextureChanged();
        }

        public static void UntrackText(Text t)
        {
            if (t.font == null)
                return;

            HashSet<Text> texts;
            m_Tracked.TryGetValue(t.font, out texts);

            if (texts == null)
                return;

            texts.Remove(t);

            if (texts.Count == 0)
            {
                m_Tracked.Remove(t.font);

                // There is a global textureRebuilt event for all fonts, so once the last Text reference goes away, remove our delegate
                if (m_Tracked.Count == 0)
                    UnityEngine.Font.textureRebuilt -= RebuildForFont;
            }
        }
    }

}
