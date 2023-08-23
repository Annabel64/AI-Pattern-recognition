namespace Mission4_Atari_BreakOut
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.SaveDrawingButton = new System.Windows.Forms.Button();
            this.ErazeButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelsListBox = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.TrainButton = new System.Windows.Forms.Button();
            this.PredictButton = new System.Windows.Forms.Button();
            this.SaveValidationButton = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitle.Font = new System.Drawing.Font("Forte", 25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxTitle.Location = new System.Drawing.Point(77, 43);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(396, 48);
            this.textBoxTitle.TabIndex = 32;
            this.textBoxTitle.Text = "Pattern recognition";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Location = new System.Drawing.Point(77, 127);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(301, 301);
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox2.Location = new System.Drawing.Point(936, 127);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(301, 301);
            this.pictureBox2.TabIndex = 34;
            this.pictureBox2.TabStop = false;
            // 
            // SaveDrawingButton
            // 
            this.SaveDrawingButton.Location = new System.Drawing.Point(77, 454);
            this.SaveDrawingButton.Name = "SaveDrawingButton";
            this.SaveDrawingButton.Size = new System.Drawing.Size(85, 96);
            this.SaveDrawingButton.TabIndex = 35;
            this.SaveDrawingButton.Text = "Save Drawing into training set";
            this.SaveDrawingButton.UseVisualStyleBackColor = true;
            this.SaveDrawingButton.Click += new System.EventHandler(this.SaveDrawingButton_Click);
            // 
            // ErazeButton
            // 
            this.ErazeButton.Location = new System.Drawing.Point(293, 454);
            this.ErazeButton.Name = "ErazeButton";
            this.ErazeButton.Size = new System.Drawing.Size(85, 47);
            this.ErazeButton.TabIndex = 36;
            this.ErazeButton.Text = "Eraze";
            this.ErazeButton.UseVisualStyleBackColor = true;
            this.ErazeButton.Click += new System.EventHandler(this.ErazeButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox1.Location = new System.Drawing.Point(411, 144);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(302, 15);
            this.textBox1.TabIndex = 37;
            this.textBox1.Text = "Enter the label of the drawing:";
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Location = new System.Drawing.Point(411, 165);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(279, 22);
            this.textBoxLabel.TabIndex = 38;
            // 
            // labelsListBox
            // 
            this.labelsListBox.FormattingEnabled = true;
            this.labelsListBox.ItemHeight = 15;
            this.labelsListBox.Location = new System.Drawing.Point(411, 204);
            this.labelsListBox.Name = "labelsListBox";
            this.labelsListBox.Size = new System.Drawing.Size(279, 124);
            this.labelsListBox.TabIndex = 39;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox2.Location = new System.Drawing.Point(411, 362);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(454, 15);
            this.textBox2.TabIndex = 40;
            this.textBox2.Text = "Data";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox3.Location = new System.Drawing.Point(411, 404);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(454, 15);
            this.textBox3.TabIndex = 41;
            this.textBox3.Text = "data";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox4.Location = new System.Drawing.Point(411, 383);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(454, 15);
            this.textBox4.TabIndex = 42;
            this.textBox4.Text = "data";
            // 
            // TrainButton
            // 
            this.TrainButton.Location = new System.Drawing.Point(734, 204);
            this.TrainButton.Name = "TrainButton";
            this.TrainButton.Size = new System.Drawing.Size(85, 47);
            this.TrainButton.TabIndex = 43;
            this.TrainButton.Text = "Train";
            this.TrainButton.UseVisualStyleBackColor = true;
            this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);
            // 
            // PredictButton
            // 
            this.PredictButton.Location = new System.Drawing.Point(734, 257);
            this.PredictButton.Name = "PredictButton";
            this.PredictButton.Size = new System.Drawing.Size(85, 47);
            this.PredictButton.TabIndex = 44;
            this.PredictButton.Text = "Predict";
            this.PredictButton.UseVisualStyleBackColor = true;
            this.PredictButton.Click += new System.EventHandler(this.PredictButton_Click);
            // 
            // SaveValidationButton
            // 
            this.SaveValidationButton.Location = new System.Drawing.Point(187, 454);
            this.SaveValidationButton.Name = "SaveValidationButton";
            this.SaveValidationButton.Size = new System.Drawing.Size(85, 96);
            this.SaveValidationButton.TabIndex = 45;
            this.SaveValidationButton.Text = "Save Drawing into validation set";
            this.SaveValidationButton.UseVisualStyleBackColor = true;
            this.SaveValidationButton.Click += new System.EventHandler(this.SaveValidationButton_Click);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox5.Location = new System.Drawing.Point(734, 313);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(130, 15);
            this.textBox5.TabIndex = 46;
            this.textBox5.Text = "Number of epochs";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(936, 454);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(300, 300);
            this.dataGridView1.TabIndex = 47;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Control;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Forte", 15F, System.Drawing.FontStyle.Underline);
            this.textBox6.Location = new System.Drawing.Point(936, 43);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(396, 48);
            this.textBox6.TabIndex = 48;
            this.textBox6.Text = "Data saved as input";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox7.Location = new System.Drawing.Point(800, 144);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(117, 15);
            this.textBox7.TabIndex = 49;
            this.textBox7.Text = "Drawing croped :";
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox8.Location = new System.Drawing.Point(829, 468);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(88, 15);
            this.textBox8.TabIndex = 50;
            this.textBox8.Text = "Pixels input :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1436, 824);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.SaveValidationButton);
            this.Controls.Add(this.PredictButton);
            this.Controls.Add(this.TrainButton);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelsListBox);
            this.Controls.Add(this.textBoxLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ErazeButton);
            this.Controls.Add(this.SaveDrawingButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button SaveDrawingButton;
        private System.Windows.Forms.Button ErazeButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.ListBox labelsListBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button TrainButton;
        private System.Windows.Forms.Button PredictButton;
        private System.Windows.Forms.Button SaveValidationButton;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
    }
}

