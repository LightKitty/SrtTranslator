using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SrtTranslator
{
    public partial class MainForm : Form
    {
        bool isSave = false; //是否已经保存
        Srt srt; //srt文件
        string dir; //srt文件夹
        string fileName; //srt文件名
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.Filter = "字幕文件（*.srt）|*.srt|全部文件（*.*）|*.*";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                string filePath = ofd.FileName; //文件路径
                dir = Path.GetDirectoryName(filePath); //目录
                fileName = Path.GetFileName(filePath); //文件名
                TranslateSrt(filePath); //翻译
            }
        }

        /// <summary>
        /// 翻译字幕文件
        /// </summary>
        /// <param name="filePath"></param>
        private void TranslateSrt(string filePath)
        {
            srt = new Srt(filePath); //生成字幕文件
            string subtitleAll = srt.GetAllSubtitle(); //获取所有字幕，拼成一个string
            textBoxOriginal.Text = subtitleAll;
            //srt.RawTexts = subtitleAll.Split('.');
            string translateText = YouDaoTranslator.Translate(subtitleAll); //获取翻译结果
            textBoxTranslate.Text = translateText;
            //srt.TranslateTexts = translateText.Split('。'); //以句号分隔字符串
            //srt.SetTranslateText(); //设置srt每条字幕的翻译
            ResetTextBoxResult(); //重新更新TextBox
        }

        /// <summary>
        /// 重新设置文本
        /// </summary>
        private void ResetTextBoxResult()
        {
            textBoxResult.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < srt.Count(); i++)
            {
                sb.AppendLine(srt[i].Index);
                sb.AppendLine(srt[i].Time);
                sb.AppendLine(String.Join(Environment.NewLine, srt[i].Subtitles.ToArray()));
                if (srt[i].TranslateText?.Count > 0) sb.AppendLine(string.Join(Environment.NewLine, srt[i].TranslateText.ToArray()));
                sb.AppendLine();
            }
            textBoxResult.Text = sb.ToString().Trim(Environment.NewLine.ToArray());
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型
            sfd.Filter = "字幕文件（*.srt）|*.srt|全部文件（*.*）|*.*";
            sfd.InitialDirectory = dir;
            sfd.FileName = "[翻译]" + fileName;

            //保存对话框是否记忆上次打开的目录
            //sfd.RestoreDirectory = true;

            //点了保存按钮进入
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd.FileName.ToString(); //获得文件路径
                StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
                sw.Write(textBoxResult.Text);
                sw.Close();
                isSave = true;
                MessageBox.Show("保存成功", "提示");
            }
        }

        private void buttonFormat_Click(object sender, EventArgs e)
        {
            string resultText = textBoxResult.Text;
            textBoxResult.Text = ReplaceChineseSymbol(resultText);
        }

        /// <summary>
        /// 替换所有的中文及标点符号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string ReplaceChineseSymbol(string str, string replacement = " ")
        {
            var placeStr = Regex.Replace(str, @"[，。；？~！：‘“”’【】（）]", " ");
            //placeStr = Regex.Replace(placeStr, @"([\u4e00-\u9fa5])", "");
            return placeStr;
        }

        /// <summary>
        /// 全角转半角
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </summary>
        /// <param name="input">输入包含全角字符的字符串</param>
        /// <returns>返回半角字符串</returns>
        public string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32; continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxResult.Text) && !isSave && MessageBox.Show("修改内容未保存，确定退出？", "提示", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Information) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void textBoxResult_TextChanged(object sender, EventArgs e)
        {
            isSave = false;
        }

        private void buttonOpenResult_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.Filter = "字幕文件（*.srt）|*.srt|全部文件（*.*）|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName; //文件路径
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    textBoxResult.Text = sr.ReadToEnd();
                }
            }
        }
    }
}
