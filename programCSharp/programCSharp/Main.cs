using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace programCSharp
{
    public partial class Main : Form
    {
        public class TreeNode
        {
            public TreeNode Left;  // Cabang ke versi lama (parent)
            public TreeNode Right; // Cabang ke versi baru (percabangan tambahan)
            public string Value;   // Teks yang disimpan di node
            public int Version;    // Nomor versi untuk node ini

            public TreeNode(string value, int version)
            {
                Value = value;
                Version = version;
                Left = null;
                Right = null;
            }
        }

        private TreeNode root; // Root node dari pohon
        private TreeNode currentNode; // Node aktif saat ini
        private List<TreeNode> allNodes = new List<TreeNode>(); // Menyimpan semua node untuk navigasi versi tertentu
        private int currentVersion = 0;

        private bool isProgrammaticChange = false;

        public Main()
        {
            InitializeComponent();
            InitializePersistentTree();
        }

        private void InitializePersistentTree()
        {
            root = new TreeNode("", 0); // Node awal dengan teks kosong
            currentNode = root;
            allNodes.Add(root); // Tambahkan root ke daftar semua node
        }

        // Tambahkan versi baru
        private void AddVersion(string text)
        {
            // Jika teks sama dengan teks node aktif, tidak perlu membuat versi baru
            if (currentNode.Value == text)
            {
                return;
            }

            // Buat node baru
            var newNode = new TreeNode(text, allNodes.Count);

            // Sambungkan node baru ke node aktif
            newNode.Left = currentNode; // Sambungkan ke versi aktif sebagai parent

            // Sambungkan node baru sebagai Right dari node aktif
            currentNode.Right = newNode;

            // Tambahkan node baru ke daftar semua node
            allNodes.Add(newNode);

            // Perbarui node aktif dan versi aktif
            currentNode = newNode;
            currentVersion = newNode.Version;
        }

        // Undo: Kembali ke versi sebelumnya
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (currentNode.Left != null)
            {
                currentNode = currentNode.Left; // Pindah ke parent (versi sebelumnya)
                currentVersion = currentNode.Version;
                SetTextBoxText(currentNode.Value);
            }
            else
            {
                MessageBox.Show("No more undo actions available!");
            }
        }

        // Redo: Pindah ke versi berikutnya
        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (currentNode.Right != null)
            {
                currentNode = currentNode.Right; // Pindah ke versi berikutnya
                currentVersion = currentNode.Version;
                SetTextBoxText(currentNode.Value);
            }
            else
            {
                MessageBox.Show("No more redo actions available!");
            }
        }

        // Navigasi langsung ke versi tertentu
        private void btnVersion_Click(object sender, EventArgs e)
        {
            int version = (int)numericUpDown1.Value;
            if (version >= 0 && version < allNodes.Count)
            {
                currentNode = allNodes[version];
                currentVersion = version;
                SetTextBoxText(currentNode.Value);
            }
            else
            {
                MessageBox.Show("Version does not exist!");
            }
        }

        // Set teks di TextBox tanpa memicu event TextChanged
        private void SetTextBoxText(string text)
        {
            isProgrammaticChange = true;
            textBox1.Text = text;
            isProgrammaticChange = false;
        }

        // Event saat teks diubah di TextBox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isProgrammaticChange)
            {
                AddVersion(textBox1.Text);
            }
        }
    }
}
