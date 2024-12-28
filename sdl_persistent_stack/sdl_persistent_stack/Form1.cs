using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sdl_persistent_stack
{
    public partial class Form1 : Form
    {
        private PersistentStack<undoRedoOperation> stack;
        private int currentPosition;
        private bool isUndoRedo = false;
        private string lastText = "";
        public Form1()
        {
            stack = new PersistentStack<undoRedoOperation>();
            currentPosition = 0;
            InitializeComponent();
        }

        private class undoRedoOperation
        {
            public readonly string Text;
            public readonly int SelectionStart;
            public readonly int SelectionLength;

            public undoRedoOperation(string text, int selectionStart, int selectionLength)
            {
                Text = text;
                SelectionStart = selectionStart;
                SelectionLength = selectionLength;

            }
        }

        private class PersistentStack<T>
        {
            private class Node
            {
                public readonly T Value;
                public readonly Node Next;
                public readonly int Size;

                public Node(T value, Node next)
                {
                    Value = value;
                    Next = next;
                    Size = (next == null) ? 1 : next.Size + 1;
                }
            }
            private readonly Node head;

            public PersistentStack()
            {
                head = null;
            }

            private PersistentStack(Node head)
            {
                this.head = head;
            }

            public PersistentStack<T> Push(T value)
            {
                return new PersistentStack<T>(new Node(value, head));
            }

            public T Peek(int posistion)
            {
                Node current = head;
                for (int i = 0; i < posistion && current != null; i++)
                {
                    current = current.Next;
                }
                return current != null ? current.Value : default(T);
            }

            public int Count
            {
                get { return head?.Size ?? 0; }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isUndoRedo)
            {
                undoRedoOperation operation = new undoRedoOperation(lastText, textBox1.SelectionStart, textBox1.SelectionLength);
                stack = stack.Push(operation);
                currentPosition = 0;
            }
            lastText = textBox1.Text;
        }

        private void undo()
        {
            if(currentPosition + 1 < stack.Count)
            {
                isUndoRedo = true;

                currentPosition++;
                undoRedoOperation operation = stack.Peek(currentPosition);
                if(operation != null)
                {
                    textBox1.Text = operation.Text;
                    textBox1.SelectionStart = operation.SelectionStart;
                    textBox1.SelectionLength = operation.SelectionLength;
                }
                isUndoRedo = false;
                
            }
        }

        private void redo()
        {
            if(currentPosition > 0)
            {
                isUndoRedo = true;
                currentPosition--;
                undoRedoOperation operation = stack.Peek(currentPosition);
                if(operation != null)
                {
                    textBox1.Text = operation.Text;
                    textBox1.SelectionStart = operation.SelectionStart;
                    textBox1.SelectionLength = operation.SelectionLength;
                }
                isUndoRedo = false;
            }
        }
        private void undoBtn_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void redoBtn_Click(object sender, EventArgs e)
        {
            redo();
        }
    }
}
