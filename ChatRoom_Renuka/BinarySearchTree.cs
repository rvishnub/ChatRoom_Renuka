using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;


namespace ChatRoom_Renuka
{
    class BinarySearchTree
    {
        public BinarySearchTree()
        {
            this.Root = null;
        }

        public Node Root { get; set; }

        public void Insert(string x)
        {
            this.Root = Insert(x, this.Root);
        }

        public void Remove(string x)
        {
            this.Root = Remove(x, this.Root);
        }

        public void RemoveMin()
        {
            this.Root = RemoveMin(this.Root);
        }

        public string FindMin()
        {
            return ElementAt(FindMin(this.Root));
        }

        public string FindMax()
        {
            return ElementAt(FindMax(this.Root));
        }

        public string Find(string x)
        {
            return ElementAt(Find(x, this.Root));
        }

        public void MakeEmpty()
        {
            this.Root = null;
        }

        public bool IsEmpty()
        {
            return this.Root == null;
        }

        private string ElementAt(Node t)
        {
            return t == null ? default(T) : t.Element;
        }

        private Node Find(string x, Node t)
        {
            while (t != null)
            {
                if ((x as IComparable).CompareTo(t.Element) < 0)
                {
                    t = t.Left;
                }
                else if ((x as IComparable).CompareTo(t.Element) > 0)
                {
                    t = t.Right;
                }
                else
                {
                    return t;
                }
            }

            return null;
        }

        private Node FindMin(Node t)
        {
            if (t != null)
            {
                while (t.Left != null)
                {
                    t = t.Left;
                }
            }

            return t;
        }

        private Node FindMax(Node t)
        {
            if (t != null)
            {
                while (t.Right != null)
                {
                    t = t.Right;
                }
            }

            return t;
        }

        protected Node Insert(string x, Node t)
        {
            if (t == null)
            {
                t = new Node(x);
            }
            else if ((x as IComparable).CompareTo(t.Element) < 0)
            {
                t.Left = Insert(x, t.Left);
            }
            else if ((x as IComparable).CompareTo(t.Element) > 0)
            {
                t.Right = Insert(x, t.Right);
            }
            else
            {
                throw new Exception("Duplicate item");
            }

            return t;
        }

        protected Node RemoveMin(Node t)
        {
            if (t == null)
            {
                throw new Exception("Item not found");
            }
            else if (t.Left != null)
            {
                t.Left = RemoveMin(t.Left);
                return t;
            }
            else
            {
                return t.Right;
            }
        }

        protected Node Remove(string x, Node t)
        {
            if (t == null)
            {
                throw new Exception("Item not found");
            }
            else if ((x as IComparable).CompareTo(t.Element) < 0)
            {
                t.Left = Remove(x, t.Left);
            }
            else if ((x as IComparable).CompareTo(t.Element) > 0)
            {
                t.Right = Remove(x, t.Right);
            }
            else if (t.Left != null && t.Right != null)
            {
                t.Element = FindMin(t.Right).Element;
                t.Right = RemoveMin(t.Right);
            }
            else
            {
                t = (t.Left != null) ? t.Left : t.Right;
            }

            return t;
        }

        public override string ToString()
        {
            return this.Root.ToString();
        }
    }
}