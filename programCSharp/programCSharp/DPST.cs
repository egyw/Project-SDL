//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace programCSharp
//{
//    class DPST
//    {
//        public class Node
//        {
//            public Node Left, Right;
//            public string Value;
//            public int Version;

//            public Node(string value, int version)
//            {
//                Value = value;
//                Version = version;
//                Left = null;
//                Right = null;
//            }
//        }

//        private List<Node> roots = new List<Node>();
//        private int currentVersion = 0;

//        private Node Build(int start, int end, string text)
//        {
//            if (string.IsNullOrEmpty(text))
//            {
//                throw new ArgumentException("Input text cannot be null or empty.");
//            }

//            // Validasi indeks
//            if (start < 0 || end >= text.Length || start > end)
//            {
//                throw new ArgumentOutOfRangeException("Start or end indices are out of range.");
//            }

//            // Basis rekursi
//            if (start == end)
//            {
//                return new Node(text[start].ToString());
//            }

//            int mid = (start + end) / 2;
//            var leftChild = Build(start, mid, text);
//            var rightChild = Build(mid + 1, end, text);
//            return new Node(leftChild.Value + rightChild.Value) { Left = leftChild, Right = rightChild };
//        }
    

//        private Node Update(Node current, int start, int end, int index, char newValue)
//        {
//            if (start == end)
//                return new Node(newValue.ToString());

//            int mid = (start + end) / 2;
//            var newNode = new Node(current.Value);

//            if (index <= mid)
//            {
//                newNode.Left = Update(current.Left, start, mid, index, newValue);
//                newNode.Right = current.Right;
//            }
//            else
//            {
//                newNode.Left = current.Left;
//                newNode.Right = Update(current.Right, mid + 1, end, index, newValue);
//            }

//            newNode.Value = newNode.Left.Value + newNode.Right.Value;
//            return newNode;
//        }

//        private string Query(Node current, int start, int end)
//        {
//            return current.Value;
//        }

//        public void Initialize(string text)
//        {
//            Versions.Add(Build(0, text.Length - 1, text));
//        }

//        public void Modify(int index, char newValue)
//        {
//            var currentRoot = Versions[Versions.Count - 1];
//            Versions.Add(Update(currentRoot, 0, currentRoot.Value.Length - 1, index, newValue));
//        }

//        public string GetVersion(int versionIndex)
//        {
//            return Query(Versions[versionIndex], 0, Versions[versionIndex].Value.Length - 1);
//        }
//    }
//}
