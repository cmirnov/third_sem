namespace Client
{
    partial class Form
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.GoButton = new System.Windows.Forms.Button();
            this.ChoiceButton = new System.Windows.Forms.ComboBox();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(26, 228);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(315, 30);
            this.progressBar.TabIndex = 1;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(26, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(315, 183);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(387, 12);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(114, 36);
            this.LoadButton.TabIndex = 3;
            this.LoadButton.Text = "Load Img";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(387, 228);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(114, 30);
            this.GoButton.TabIndex = 4;
            this.GoButton.TabStop = false;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // ChoiceButton
            // 
            this.ChoiceButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChoiceButton.FormattingEnabled = true;
            this.ChoiceButton.Location = new System.Drawing.Point(387, 95);
            this.ChoiceButton.Name = "ChoiceButton";
            this.ChoiceButton.Size = new System.Drawing.Size(114, 21);
            this.ChoiceButton.TabIndex = 5;
            this.ChoiceButton.Tag = "false";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(387, 157);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(114, 38);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 281);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ChoiceButton);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "instageamm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.ComboBox ChoiceButton;
        private System.Windows.Forms.Button CancelButton;
    }
}

