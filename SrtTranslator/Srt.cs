using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace SrtTranslator
{
    public class Srt
    {
        private List<SrtPart> setParts;

        public Srt(string path)
        {
            setParts = new List<SrtPart>();
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string line = sr.ReadLine();
            SrtPart srtPart = new SrtPart();
            while (line != null)
            {
                if (line != "")
                {
                    srtPart.Index = line;
                    srtPart.Time = sr.ReadLine();
                    srtPart.Subtitles = new List<string>();
                    line = sr.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        srtPart.Subtitles.Add(line);
                        line = sr.ReadLine();
                    }
                    if (srtPart.IsComplate())
                    {
                        setParts.Add(srtPart);
                    }
                    srtPart = new SrtPart();
                }
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public SrtPart this[int id]
        {
            get
            {
                return setParts[id];
            }
            set
            {
                setParts[id] = value;
            }
        }

        internal string GetAllSubtitle()
        {
            string result = string.Empty;
            foreach(SrtPart srtPart in setParts)
            {
                foreach(string line in srtPart.Subtitles)
                {
                    result += line + " ";
                }
            }
            return result.Trim().Replace("  ", " "); //替换双空格 去除头尾空格
        }

        public int Count()
        {
            return setParts.Count();
        }
    }

    public class SrtPart
    {
        public string Index { get; set; }
        public string Time { get; set; }
        public List<string> Subtitles { get; set; }
        public List<string> TranslateText { get; set; }

        public bool IsComplate()
        {
            return (Index != null && Time != null && Subtitles != null);
        }
    }
    
}
