using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace SrtTranslator
{
    /// <summary>
    /// srt model
    /// </summary>
    public class Srt
    {
        private List<SrtPart> setParts;
        public string[] TranslateTexts { get; set; } //翻译后的文本列表
        public string[] RawTexts { get; set; } //原始文本列表

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"></param>
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

        /// <summary>
        /// 访问器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取所有字幕，拼成字符串
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 字幕条数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return setParts.Count();
        }

        /// <summary>
        /// 设置每一条字幕的翻译
        /// </summary>
        public void SetTranslateText()
        {
            SetTranslateText(TranslateTexts);
        }

        /// <summary>
        /// 设置每一条字幕的翻译
        /// </summary>
        /// <param name="translateTexts"></param>
        public void SetTranslateText(string[] translateTexts)
        {
            //寻找每条翻译字幕合适的位置赋值
            int insertId = 0;
            int tId = 0;
            for (int i = 0; i < setParts.Count(); i++)
            {
                if (setParts[i].Subtitles.Any(x => x.Contains(".")) && tId < translateTexts.Length)
                {
                    setParts[insertId].TranslateText = new List<string>() { translateTexts[tId] };
                    tId++;
                    for (int j = 0; j < setParts[i].Subtitles.Count - 1; j++)
                    {
                        if (setParts[i].Subtitles[j].TrimEnd().EndsWith("."))
                        {
                            insertId = i;
                            setParts[insertId].TranslateText.Add(translateTexts[tId]);
                            tId++;
                        }

                    }

                    if (setParts[i].Subtitles.Last().TrimEnd().EndsWith("."))
                    {
                        insertId = i + 1;
                    }
                    else
                        insertId = i;
                }
                else if (setParts[i].Subtitles.All(x => string.IsNullOrWhiteSpace(x)))
                {
                    insertId++;
                }
            }
        }
    }

    /// <summary>
    /// 每一条字幕，包含序号、时间、字幕、翻译
    /// </summary>
    public class SrtPart
    {
        public string Index { get; set; }
        public string Time { get; set; }
        public List<string> Subtitles { get; set; }
        public List<string> TranslateText { get; set; } = new List<string>();

        public bool IsComplate()
        {
            return (Index != null && Time != null && Subtitles != null);
        }
    }
    
}
