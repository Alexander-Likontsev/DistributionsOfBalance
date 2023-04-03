namespace DistributionOfBalance
{
    partial class EDGF
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_VendorsCodesOriginal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.but_PathOfFile = new System.Windows.Forms.Button();
            this.but_LoadAndOutPut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.OFDVendorsCodes = new System.Windows.Forms.OpenFileDialog();
            this.FBDSaveFile = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // tb_VendorsCodesOriginal
            // 
            this.tb_VendorsCodesOriginal.Location = new System.Drawing.Point(40, 35);
            this.tb_VendorsCodesOriginal.Margin = new System.Windows.Forms.Padding(2);
            this.tb_VendorsCodesOriginal.Name = "tb_VendorsCodesOriginal";
            this.tb_VendorsCodesOriginal.Size = new System.Drawing.Size(143, 20);
            this.tb_VendorsCodesOriginal.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь к файлу настроек";
            // 
            // but_PathOfFile
            // 
            this.but_PathOfFile.Location = new System.Drawing.Point(201, 31);
            this.but_PathOfFile.Margin = new System.Windows.Forms.Padding(2);
            this.but_PathOfFile.Name = "but_PathOfFile";
            this.but_PathOfFile.Size = new System.Drawing.Size(73, 22);
            this.but_PathOfFile.TabIndex = 2;
            this.but_PathOfFile.Text = "Выбрать";
            this.but_PathOfFile.UseVisualStyleBackColor = true;
            this.but_PathOfFile.Click += new System.EventHandler(this.but_PathOfFile_Click);
            // 
            // but_LoadAndOutPut
            // 
            this.but_LoadAndOutPut.Location = new System.Drawing.Point(157, 157);
            this.but_LoadAndOutPut.Margin = new System.Windows.Forms.Padding(2);
            this.but_LoadAndOutPut.Name = "but_LoadAndOutPut";
            this.but_LoadAndOutPut.Size = new System.Drawing.Size(117, 52);
            this.but_LoadAndOutPut.TabIndex = 3;
            this.but_LoadAndOutPut.Text = "Загрузить остатки и распределить";
            this.but_LoadAndOutPut.UseVisualStyleBackColor = true;
            this.but_LoadAndOutPut.Click += new System.EventHandler(this.but_LoadAndOutPut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 4;
            // 
            // OFDVendorsCodes
            // 
            this.OFDVendorsCodes.FileName = "openFileDialog1";
            // 
            // EDGF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 219);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.but_LoadAndOutPut);
            this.Controls.Add(this.but_PathOfFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_VendorsCodesOriginal);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EDGF";
            this.Text = "Распределение остатков";
            this.Load += new System.EventHandler(this.EDGF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_VendorsCodesOriginal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button but_PathOfFile;
        private System.Windows.Forms.Button but_LoadAndOutPut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog OFDVendorsCodes;
        private System.Windows.Forms.FolderBrowserDialog FBDSaveFile;
    }
}

