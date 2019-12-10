using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SrtTranslator
{
    public partial class MainForm : Form
    {
        Srt srt;
        string dir;
        string fileName;
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
            //srt.RawTexts = subtitleAll.Split('.');
            string translateText = YouDaoTranslator.Translate(subtitleAll); //获取翻译结果
            srt.TranslateTexts = translateText.Split('。'); //以句号分隔字符串
            srt.SetTranslateText(); //设置srt每条字幕的翻译
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
                MessageBox.Show("保存成功", "提示");
            }
        }
    }
}
