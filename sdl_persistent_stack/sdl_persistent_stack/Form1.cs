using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;

namespace sdl_persistent_stack
{
    public partial class Form1 : Form
    {
        private PersistentStack<undoRedoOperation> stack;
        private int currentPosition;
        private bool isUndoRedo = false, isBatchTest = false, isPeeked = false;
        private Stopwatch stopwatch = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            stack = new PersistentStack<undoRedoOperation>();

            undoRedoOperation initialOperation = new undoRedoOperation("", 0, 0);
            stack = stack.Push(initialOperation);
            currentPosition = 0;

            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = stack.Count - 1;

            UpdateStackView(); 
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

            public T PeekFromTop(int positionFromTop)
            {
                Node current = head;

                for (int i = 0; i < positionFromTop && current != null; i++)
                {
                    current = current.Next;
                }

                return current != null ? current.Value : default(T);
            }

            public T PeekFromBottom(int positionFromBottom)
            {
                int positionFromTop = (Count - 1) - positionFromBottom;
                return PeekFromTop(positionFromTop);
            }

            public int Count
            {
                get { return head?.Size ?? 0; }
            }
        }

        private void UpdateStackView()
        {
            listBox1.Items.Clear();

            for (int i = stack.Count - 1; i >= 0; i--)
            {
                undoRedoOperation operation = stack.PeekFromBottom(i);
                if (operation != null)
                {
                    listBox1.Items.Add($"Versi {i}: {operation.Text}");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isUndoRedo)
            {
                if (!isBatchTest && !isPeeked) stopwatch.Restart();

                undoRedoOperation operation = new undoRedoOperation(textBox1.Text, textBox1.SelectionStart, textBox1.SelectionLength);
                stack = stack.Push(operation);
                if (!isBatchTest && !isPeeked)
                {
                    stopwatch.Stop();
                    MessageBox.Show($"Waktu untuk Push ke stack: {stopwatch.ElapsedMilliseconds} ms");
                }
                currentPosition = 0;

                numericUpDown1.Maximum = stack.Count - 1;

                UpdateStackView();
                
            }
        }

        private void undo()
        {
            if (currentPosition + 1 < stack.Count)
            {
                stopwatch.Restart();
                isUndoRedo = true;

                currentPosition++;
                undoRedoOperation operation = stack.PeekFromTop(currentPosition);
                if (operation != null)
                {
                    textBox1.Text = operation.Text;
                    textBox1.SelectionStart = operation.SelectionStart;
                    textBox1.SelectionLength = operation.SelectionLength;
                }

                stopwatch.Stop(); 
                MessageBox.Show($"Waktu undo: {stopwatch.ElapsedMilliseconds} ms");

                isUndoRedo = false;

                UpdateStackView();
            }
        }

        private void redo()
        {
            if (currentPosition > 0)
            {
                stopwatch.Restart();
                isUndoRedo = true;

                currentPosition--;
                undoRedoOperation operation = stack.PeekFromTop(currentPosition);
                if (operation != null)
                {
                    textBox1.Text = operation.Text;
                    textBox1.SelectionStart = operation.SelectionStart;
                    textBox1.SelectionLength = operation.SelectionLength;
                }

                isUndoRedo = false;

                stopwatch.Stop(); 
                MessageBox.Show($"Waktu redo: {stopwatch.ElapsedMilliseconds} ms");

                UpdateStackView();
            }
        }

        private void btnPeek_Click(object sender, EventArgs e)
        {
            isPeeked = true;
            int versionToPeek = (int)numericUpDown1.Value;
            if (versionToPeek >= 0 && versionToPeek < stack.Count)
            {
                stopwatch.Restart();
                undoRedoOperation operation = stack.PeekFromBottom(versionToPeek);
                if (operation != null)
                {
                    textBox1.Text = operation.Text;
                    textBox1.SelectionStart = operation.SelectionStart;
                    textBox1.SelectionLength = operation.SelectionLength;
                }
                else
                {
                    MessageBox.Show("Versi tidak ditemukan di stack.");
                }
                stopwatch.Stop();
                MessageBox.Show($"Waktu peek versi {versionToPeek}: {stopwatch.ElapsedMilliseconds} ms");
            }
            else
            {
                MessageBox.Show("Pilih versi yang valid.");
            }

            UpdateStackView();
            isPeeked = false;
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void redoBtn_Click(object sender, EventArgs e)
        {
            redo();
        }




        //untuk pengujian
        private void btnTes_Click(object sender, EventArgs e)
        {
            int chara = 1000;
            stopwatch.Restart();
            for (int i = 0; i < chara; i++)
            {
                undoRedoOperation operation = new undoRedoOperation($"Text {i}", i, 0);
                stack = stack.Push(operation);
            }
            stopwatch.Stop();
            MessageBox.Show($"Waktu untuk push {chara} operasi: {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Restart();
            for (int i = 0; i < chara; i++)
            {
                stack.PeekFromBottom(i % stack.Count);
            }
            stopwatch.Stop();
            MessageBox.Show($"Waktu untuk peek {chara} operasi: {stopwatch.ElapsedMilliseconds} ms");
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            isBatchTest = true;
            int total = 1000;
            stopwatch.Restart();
            for (int i = 0; i < total; i++)
            {
                char rand = GetRandomCharacter();
                textBox1.Text += rand;
            }
            stopwatch.Stop();
            MessageBox.Show($"Waktu total untuk insert {total} karakter: {stopwatch.ElapsedMilliseconds} ms");
            isBatchTest = false;
        }

        private char GetRandomCharacter()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return chars[random.Next(chars.Length)];
        }
    }
}
