using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace undo_redo
{
    public partial class Form1 : Form
    {
        private readonly undoRedo<string> undoRedo = new undoRedo<string>();
        private bool textChangedFromUndoRedo = false;
        public Form1()
        {
            InitializeComponent();
            undoRedo.addState(string.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textChangedFromUndoRedo)
            {
                return;
            }
            undoRedo.addState(textBox1.Text);
        }

        private void undo_btn_Click(object sender, EventArgs e)
        {
            if (!undoRedo.canUndo)
            {
                return;
            }
            
            textChangedFromUndoRedo = true;
            textBox1.Text = undoRedo.undo(textBox1.Text);
            textChangedFromUndoRedo = false;
        }

        private void redo_btn_Click(object sender, EventArgs e)
        {
            if (!undoRedo.canRedo)
            {
                return;
            }
            textChangedFromUndoRedo = true;
            textBox1.Text = undoRedo.redo(textBox1.Text);
            textChangedFromUndoRedo = false;
        }
    }
}
