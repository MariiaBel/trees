using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        public AVLTreeNode<T> Head
        {
            get;
            internal set;
        }

        public int Count
        {
            get;
            private set;
        }

        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new AVLTreeNode<T>(value, null, this);
            }
            else
            {
                AddTo(Head, value);
            }
            Count++;
        }

        public void Add(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Add(arr[i]);
            }
        }

        private void AddTo(AVLTreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null) node.Left = new AVLTreeNode<T>(value, node, this);
                else AddTo(node.Left, value);
            }
            else
            {
                if (node.Right == null) node.Right = new AVLTreeNode<T>(value, node, this);
                else AddTo(node.Right, value);
            }
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        private AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> current = Head;
            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0) current = current.Left;
                else if (result < 0) current = current.Right;
                else break;
            }
            return current;
        }

        public bool Remove(T value)
        {
            AVLTreeNode<T> current = Find(value);
            if (current == null) return false;

            AVLTreeNode<T> treeToBallance = current.Parent;
            Count--;

            if (current.Right == null)
            {
                if (current.Parent == null)
                {
                    Head = current.Left;
                    if (Head != null) Head.Parent = null;
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0) current.Parent.Left = current.Left;
                    else if (result < 0) current.Parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current.Parent == null)
                {
                    Head = current.Right;
                    if (Head != null) Head.Parent = null;
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0) current.Parent.Left = current.Right;
                    else if (result < 0) current.Parent.Right = current.Right;
                }
            }
            else
            {
                AVLTreeNode<T> leftMost = current.Right.Left;
                while (leftMost.Left != null)
                {
                    leftMost = leftMost.Left;
                }

                leftMost.Parent.Left = leftMost.Right;
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (current.Parent == null)
                {
                    Head = leftMost;
                    if (Head != null) Head.Parent = null;
                }
                else
                {
                    int result = leftMost.Parent.CompareTo(leftMost.Value);
                    if (result > 0) current.Parent.Left = leftMost;
                    else if (result < 0) current.Parent.Right = leftMost;
                }
            }

            if (treeToBallance != null) treeToBallance.Balance();
            else
            {
                if (Head != null) Head.Balance(); 
            }

            return true;
        }

        public void Clear()
        {
            Head = null; 
            Count = 0;
        }

        public IEnumerator<T> InOrderTraversal()
        {
            if (Head != null)
            {
                Stack<AVLTreeNode<T>> stack = new Stack<AVLTreeNode<T>>();
                AVLTreeNode<T> current = Head;
                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
