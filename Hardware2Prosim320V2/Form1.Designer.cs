namespace Hardware2Prosim320V2
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_Debug = new System.Windows.Forms.Button();
            this.button_Connect = new System.Windows.Forms.Button();
            this.textBox_YokeR = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_YokeR = new System.Windows.Forms.CheckBox();
            this.textBox_YokeL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox_YokeL = new System.Windows.Forms.CheckBox();
            this.textBox_TQ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_TQ = new System.Windows.Forms.CheckBox();
            this.textBox_Glare = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_Glare = new System.Windows.Forms.CheckBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel1.Controls.Add(this.button_Debug);
            this.splitContainer1.Panel1.Controls.Add(this.button_Connect);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_YokeR);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_YokeR);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_YokeL);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_YokeL);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_TQ);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_TQ);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_Glare);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_Glare);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 170);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(174, 277);
            this.richTextBox1.TabIndex = 40;
            this.richTextBox1.Text = "";
            // 
            // button_Debug
            // 
            this.button_Debug.Location = new System.Drawing.Point(0, 141);
            this.button_Debug.Name = "button_Debug";
            this.button_Debug.Size = new System.Drawing.Size(177, 23);
            this.button_Debug.TabIndex = 39;
            this.button_Debug.Text = "Display";
            this.button_Debug.UseVisualStyleBackColor = true;
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(0, 112);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(177, 23);
            this.button_Connect.TabIndex = 38;
            this.button_Connect.Text = "连接";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // textBox_YokeR
            // 
            this.textBox_YokeR.Location = new System.Drawing.Point(98, 85);
            this.textBox_YokeR.Name = "textBox_YokeR";
            this.textBox_YokeR.Size = new System.Drawing.Size(79, 21);
            this.textBox_YokeR.TabIndex = 37;
            this.textBox_YokeR.Text = "COM5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "串口";
            // 
            // checkBox_YokeR
            // 
            this.checkBox_YokeR.AutoSize = true;
            this.checkBox_YokeR.Location = new System.Drawing.Point(3, 88);
            this.checkBox_YokeR.Name = "checkBox_YokeR";
            this.checkBox_YokeR.Size = new System.Drawing.Size(60, 16);
            this.checkBox_YokeR.TabIndex = 35;
            this.checkBox_YokeR.Text = "StickR";
            this.checkBox_YokeR.UseVisualStyleBackColor = true;
            // 
            // textBox_YokeL
            // 
            this.textBox_YokeL.Location = new System.Drawing.Point(98, 58);
            this.textBox_YokeL.Name = "textBox_YokeL";
            this.textBox_YokeL.Size = new System.Drawing.Size(79, 21);
            this.textBox_YokeL.TabIndex = 34;
            this.textBox_YokeL.Text = "COM6";
            this.textBox_YokeL.TextChanged += new System.EventHandler(this.textBox_YokeL_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "串口";
            // 
            // checkBox_YokeL
            // 
            this.checkBox_YokeL.AutoSize = true;
            this.checkBox_YokeL.Location = new System.Drawing.Point(3, 61);
            this.checkBox_YokeL.Name = "checkBox_YokeL";
            this.checkBox_YokeL.Size = new System.Drawing.Size(60, 16);
            this.checkBox_YokeL.TabIndex = 32;
            this.checkBox_YokeL.Text = "SitckL";
            this.checkBox_YokeL.UseVisualStyleBackColor = true;
            // 
            // textBox_TQ
            // 
            this.textBox_TQ.Location = new System.Drawing.Point(98, 31);
            this.textBox_TQ.Name = "textBox_TQ";
            this.textBox_TQ.Size = new System.Drawing.Size(79, 21);
            this.textBox_TQ.TabIndex = 25;
            this.textBox_TQ.Text = "COM4";
            this.textBox_TQ.TextChanged += new System.EventHandler(this.textBox_TQ_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "串口";
            // 
            // checkBox_TQ
            // 
            this.checkBox_TQ.AutoSize = true;
            this.checkBox_TQ.Location = new System.Drawing.Point(3, 34);
            this.checkBox_TQ.Name = "checkBox_TQ";
            this.checkBox_TQ.Size = new System.Drawing.Size(36, 16);
            this.checkBox_TQ.TabIndex = 23;
            this.checkBox_TQ.Text = "TQ";
            this.checkBox_TQ.UseVisualStyleBackColor = true;
            // 
            // textBox_Glare
            // 
            this.textBox_Glare.Location = new System.Drawing.Point(98, 4);
            this.textBox_Glare.Name = "textBox_Glare";
            this.textBox_Glare.Size = new System.Drawing.Size(79, 21);
            this.textBox_Glare.TabIndex = 22;
            this.textBox_Glare.Text = "COM3";
            this.textBox_Glare.TextChanged += new System.EventHandler(this.textBox_Glare_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "串口";
            // 
            // checkBox_Glare
            // 
            this.checkBox_Glare.AutoSize = true;
            this.checkBox_Glare.Location = new System.Drawing.Point(3, 7);
            this.checkBox_Glare.Name = "checkBox_Glare";
            this.checkBox_Glare.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Glare.TabIndex = 20;
            this.checkBox_Glare.Text = "Glare";
            this.checkBox_Glare.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(3, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(610, 444);
            this.dataGridView.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.TextBox textBox_YokeR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox_YokeR;
        private System.Windows.Forms.TextBox textBox_YokeL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox_YokeL;
        private System.Windows.Forms.TextBox textBox_TQ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_TQ;
        private System.Windows.Forms.TextBox textBox_Glare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_Glare;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_Debug;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}

