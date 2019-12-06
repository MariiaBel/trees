using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class AVLTreeNode<TNode> : IComparable<TNode> where TNode : IComparable
    {
        AVLTree<TNode> tree;
        AVLTreeNode<TNode> left,
                           right;

        public  AVLTreeNode(TNode value, AVLTreeNode<TNode> parent, AVLTree<TNode> tree)
        {
            this.Value = value;
            this.Parent = parent;
            this.tree = tree;
        }

        public AVLTreeNode<TNode> Left
        {
            get
            {
                return left;
            }
            internal set
            {
                left = value;
                if (left != null) left.Parent = this;
            }
        }

        public AVLTreeNode<TNode> Right
        {
            get
            {
                return right;
            }
            internal set
            {
                right = value;
                if (right != null) right.Parent = this;
            }

        }

        public AVLTreeNode<TNode> Parent
        {
            get;
            internal set;
        }

        public TNode Value
        {
            get;
            private set;
        }

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

    }
}
