using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SrtTranslator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "字幕文件（*.srt）|*.srt|全部文件（*.*）|*.*";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                string filePath = ofd.FileName;
                TranslateSrt(filePath);
            }
        }

        /// <summary>
        /// 翻译字幕文件
        /// </summary>
        /// <param name="filePath"></param>
        private void TranslateSrt(string filePath)
        {
            Srt srt = new Srt(filePath);
            string subtitleAll = srt.GetAllSubtitle();
            string[] rawTexts = subtitleAll.Split('.');
            string translateText = YouDaoTranslator.Translate(subtitleAll); //Translater.Translate(subtitleAll);
            //textBoxResult.Text = translateText.Replace("。", Environment.NewLine);
            string[] translateTexts = translateText.Split('。');
            int insertId = 0;
            int tId = 0;
            for (int i = 0; i < srt.Count(); i++)
            {
                if (srt[i].Subtitles.Any(x => x.Contains(".")) && tId < translateTexts.Length)
                {
                    srt[insertId].TranslateText = new List<string>() { translateTexts[tId] };
                    if (srt[i].Subtitles.Any(x => x.EndsWith(".")))
                        insertId = i + 1;
                    else
                        insertId = i;
                    tId++;
                }
            }
            textBox1.Clear();
            for (int i = 0; i < srt.Count(); i++)
            {
                textBox1.AppendText(srt[i].Index + Environment.NewLine);
                textBox1.AppendText(srt[i].Time + Environment.NewLine);
                textBox1.AppendText(String.Join(Environment.NewLine, srt[i].Subtitles.ToArray()) + Environment.NewLine);
                if (srt[i].TranslateText?.Count > 0) textBox1.AppendText(String.Join(Environment.NewLine, srt[i].TranslateText.ToArray()) + Environment.NewLine);
                textBox1.AppendText(Environment.NewLine);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型
            sfd.Filter = "字幕文件（*.srt）|*.srt|全部文件（*.*）|*.*";

            //保存对话框是否记忆上次打开的目录
            //sfd.RestoreDirectory = true;

            //点了保存按钮进入
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd.FileName.ToString(); //获得文件路径
                StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
                sw.Write(textBox1.Text);
                sw.Close();
                MessageBox.Show("保存成功", "提示");
            }
        }
    }
}
