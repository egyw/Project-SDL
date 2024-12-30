
namespace sdl_persistent_stack
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.undoBtn = new System.Windows.Forms.Button();
            this.redoBtn = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnPeek = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnTes = new System.Windows.Forms.Button();
            this.btnBatch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 114);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1066, 541);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // undoBtn
            // 
            this.undoBtn.Location = new System.Drawing.Point(790, 18);
            this.undoBtn.Margin = new System.Windows.Forms.Padding(4);
            this.undoBtn.Name = "undoBtn";
            this.undoBtn.Size = new System.Drawing.Size(127, 87);
            this.undoBtn.TabIndex = 1;
            this.undoBtn.Text = "UNDO";
            this.undoBtn.UseVisualStyleBackColor = true;
            this.undoBtn.Click += new System.EventHandler(this.undoBtn_Click);
            // 
            // redoBtn
            // 
            this.redoBtn.Location = new System.Drawing.Point(955, 19);
            this.redoBtn.Margin = new System.Windows.Forms.Padding(4);
            this.redoBtn.Name = "redoBtn";
            this.redoBtn.Size = new System.Drawing.Size(127, 87);
            this.redoBtn.TabIndex = 2;
            this.redoBtn.Text = "REDO";
            this.redoBtn.UseVisualStyleBackColor = true;
            this.redoBtn.Click += new System.EventHandler(this.redoBtn_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(24, 42);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(6);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(220, 29);
            this.numericUpDown1.TabIndex = 3;
            // 
            // btnPeek
            // 
            this.btnPeek.Location = new System.Drawing.Point(254, 13);
            this.btnPeek.Margin = new System.Windows.Forms.Padding(4);
            this.btnPeek.Name = "btnPeek";
            this.btnPeek.Size = new System.Drawing.Size(127, 87);
            this.btnPeek.TabIndex = 4;
            this.btnPeek.Text = "PEEK";
            this.btnPeek.UseVisualStyleBackColor = true;
            this.btnPeek.Click += new System.EventHandler(this.btnPeek_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(1090, 123);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(403, 532);
            this.listBox1.TabIndex = 5;
            // 
            // btnTes
            // 
            this.btnTes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTes.Location = new System.Drawing.Point(1090, 18);
            this.btnTes.Margin = new System.Windows.Forms.Padding(4);
            this.btnTes.Name = "btnTes";
            this.btnTes.Size = new System.Drawing.Size(403, 40);
            this.btnTes.TabIndex = 6;
            this.btnTes.Text = "TEST";
            this.btnTes.UseVisualStyleBackColor = true;
            this.btnTes.Click += new System.EventHandler(this.btnTes_Click);
            // 
            // btnBatch
            // 
            this.btnBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatch.Location = new System.Drawing.Point(1090, 66);
            this.btnBatch.Margin = new System.Windows.Forms.Padding(4);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(403, 40);
            this.btnBatch.TabIndex = 7;
            this.btnBatch.Text = "BATCH INSERT";
            this.btnBatch.UseVisualStyleBackColor = true;
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 676);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.btnTes);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnPeek);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.redoBtn);
            this.Controls.Add(this.undoBtn);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button undoBtn;
        private System.Windows.Forms.Button redoBtn;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnPeek;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnTes;
        private System.Windows.Forms.Button btnBatch;
    }
}

