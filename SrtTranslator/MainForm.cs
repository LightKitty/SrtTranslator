using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                string filePath = ofd.FileName;
                Srt srt = new Srt(filePath);
                string subtitleAll = srt.GetAllSubtitle();
                textBoxOri.Text = subtitleAll;
                string translateText = YouDaoTranslator.Translate(subtitleAll); //Translater.Translate(subtitleAll);
                textBoxResult.Text = translateText.Replace("。", Environment.NewLine);
                string[] translateTexts = translateText.Split('。');
                int insertId = 0;
                int tId = 0;
                for (int i=0; i<srt.Count();i++)
                {
                    if(srt[i].Subtitles.Any(x=>x.Contains("."))&& tId < translateTexts.Length)
                    {
                        srt[insertId].TranslateText = new List<string>() { translateTexts[tId] };
                        if (srt[i].Subtitles.Any(x => x.EndsWith(".")))
                            insertId = i + 1;
                        else
                            insertId = i;
                        tId++;
                    }
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < srt.Count(); i++)
                {
                    sb.AppendLine(srt[i].Index);
                    sb.AppendLine(srt[i].Time);
                    sb.Append(string.Join(Environment.NewLine, srt[i].Subtitles.ToArray()) + Environment.NewLine);
                    if (srt[i].TranslateText?.Count > 0) sb.Append(string.Join(Environment.NewLine, srt[i].TranslateText.ToArray()) + Environment.NewLine);
                    sb.AppendLine();
                }
                textBox1.Text = sb.ToString();
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
