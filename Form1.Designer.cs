namespace Частицы
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbDirection = new System.Windows.Forms.TrackBar();
            this.lblDirection = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDirection1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDirection1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbGravition = new System.Windows.Forms.TrackBar();
            this.tbGravition2 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDirection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition2)).BeginInit();
            this.SuspendLayout();
            // 
            // picDisplay
            // 
            this.picDisplay.Location = new System.Drawing.Point(0, 0);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(1000, 568);
            this.picDisplay.TabIndex = 0;
            this.picDisplay.TabStop = false;
            this.picDisplay.Click += new System.EventHandler(this.picDisplay_Click);
            this.picDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseClick);
            this.picDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbDirection
            // 
            this.tbDirection.Location = new System.Drawing.Point(1, 589);
            this.tbDirection.Maximum = 359;
            this.tbDirection.Name = "tbDirection";
            this.tbDirection.Size = new System.Drawing.Size(193, 45);
            this.tbDirection.TabIndex = 1;
            this.tbDirection.Scroll += new System.EventHandler(this.tbDirection_Scroll);
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Location = new System.Drawing.Point(200, 601);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(17, 13);
            this.lblDirection.TabIndex = 2;
            this.lblDirection.Text = "0°";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 573);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Направление";
            // 
            // tbDirection1
            // 
            this.tbDirection1.Location = new System.Drawing.Point(234, 589);
            this.tbDirection1.Maximum = 359;
            this.tbDirection1.Name = "tbDirection1";
            this.tbDirection1.Size = new System.Drawing.Size(193, 45);
            this.tbDirection1.TabIndex = 4;
            this.tbDirection1.Scroll += new System.EventHandler(this.tbDirection1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 571);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Разброс";
            // 
            // lblDirection1
            // 
            this.lblDirection1.AutoSize = true;
            this.lblDirection1.Location = new System.Drawing.Point(433, 601);
            this.lblDirection1.Name = "lblDirection1";
            this.lblDirection1.Size = new System.Drawing.Size(17, 13);
            this.lblDirection1.TabIndex = 6;
            this.lblDirection1.Text = "0°";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(474, 571);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Сила притяжения 1ой т.";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // tbGravition
            // 
            this.tbGravition.Location = new System.Drawing.Point(466, 589);
            this.tbGravition.Maximum = 100;
            this.tbGravition.Name = "tbGravition";
            this.tbGravition.Size = new System.Drawing.Size(136, 45);
            this.tbGravition.TabIndex = 7;
            this.tbGravition.Scroll += new System.EventHandler(this.tbGravition_Scroll);
            // 
            // tbGravition2
            // 
            this.tbGravition2.Location = new System.Drawing.Point(629, 589);
            this.tbGravition2.Maximum = 100;
            this.tbGravition2.Name = "tbGravition2";
            this.tbGravition2.Size = new System.Drawing.Size(136, 45);
            this.tbGravition2.TabIndex = 9;
            this.tbGravition2.Scroll += new System.EventHandler(this.tbGravition2_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(637, 571);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Сила притяжения 2ой т.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 633);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbGravition2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbGravition);
            this.Controls.Add(this.lblDirection1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDirection1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDirection);
            this.Controls.Add(this.tbDirection);
            this.Controls.Add(this.picDisplay);
            this.Name = "Form1";
            this.Text = "Частицы";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDirection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar tbDirection;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbDirection1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDirection1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbGravition;
        private System.Windows.Forms.TrackBar tbGravition2;
        private System.Windows.Forms.Label label3;
    }
}

