﻿
namespace undo_redo
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
            this.undo_btn = new System.Windows.Forms.Button();
            this.redo_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 381);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // undo_btn
            // 
            this.undo_btn.Location = new System.Drawing.Point(566, 12);
            this.undo_btn.Name = "undo_btn";
            this.undo_btn.Size = new System.Drawing.Size(108, 39);
            this.undo_btn.TabIndex = 3;
            this.undo_btn.Text = "UNDO";
            this.undo_btn.UseVisualStyleBackColor = true;
            this.undo_btn.Click += new System.EventHandler(this.undo_btn_Click);
            // 
            // redo_btn
            // 
            this.redo_btn.Location = new System.Drawing.Point(680, 12);
            this.redo_btn.Name = "redo_btn";
            this.redo_btn.Size = new System.Drawing.Size(108, 39);
            this.redo_btn.TabIndex = 4;
            this.redo_btn.Text = "REDO";
            this.redo_btn.UseVisualStyleBackColor = true;
            this.redo_btn.Click += new System.EventHandler(this.redo_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.redo_btn);
            this.Controls.Add(this.undo_btn);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Notepad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button undo_btn;
        private System.Windows.Forms.Button redo_btn;
    }
}

