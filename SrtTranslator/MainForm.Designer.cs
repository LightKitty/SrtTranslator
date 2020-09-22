namespace SrtTranslator
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnOpen = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBoxOriginal = new System.Windows.Forms.TextBox();
            this.textBoxTranslate = new System.Windows.Forms.TextBox();
            this.buttonFormat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.Location = new System.Drawing.Point(637, 41);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResult.Size = new System.Drawing.Size(615, 628);
            this.textBoxResult.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(718, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBoxOriginal
            // 
            this.textBoxOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOriginal.Location = new System.Drawing.Point(12, 41);
            this.textBoxOriginal.Multiline = true;
            this.textBoxOriginal.Name = "textBoxOriginal";
            this.textBoxOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOriginal.Size = new System.Drawing.Size(615, 310);
            this.textBoxOriginal.TabIndex = 4;
            // 
            // textBoxTranslate
            // 
            this.textBoxTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTranslate.Location = new System.Drawing.Point(12, 359);
            this.textBoxTranslate.Multiline = true;
            this.textBoxTranslate.Name = "textBoxTranslate";
            this.textBoxTranslate.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTranslate.Size = new System.Drawing.Size(615, 310);
            this.textBoxTranslate.TabIndex = 5;
            // 
            // buttonFormat
            // 
            this.buttonFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFormat.Location = new System.Drawing.Point(637, 12);
            this.buttonFormat.Name = "buttonFormat";
            this.buttonFormat.Size = new System.Drawing.Size(75, 23);
            this.buttonFormat.TabIndex = 6;
            this.buttonFormat.Text = "格式";
            this.buttonFormat.UseVisualStyleBackColor = true;
            this.buttonFormat.Click += new System.EventHandler(this.buttonFormat_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.buttonFormat);
            this.Controls.Add(this.textBoxTranslate);
            this.Controls.Add(this.textBoxOriginal);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.btnOpen);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字幕翻译助手";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBoxOriginal;
        private System.Windows.Forms.TextBox textBoxTranslate;
        private System.Windows.Forms.Button buttonFormat;
    }
}

