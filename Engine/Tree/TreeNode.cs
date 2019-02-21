using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Tree
{
    /// <summary>
    /// Класс, определяющий узел дерева
    /// </summary>
    internal class TreeNode: ITreeNode
    {
        /// <summary>
        /// Потомки
        /// </summary>
        public List<ITreeNode> Children { get; set; }

        public ITreeNode Parent { get; set; }

        /// <summary>
        /// Символ узла
        /// </summary>
        public char Letter { get; set; }

        public TreeNode(char letter, ITreeNode parent)
        {
            Letter = letter;
            Children = new List<ITreeNode>();
            Parent = parent;
        }

        /// <summary>
        /// Потомок, соответствующий заданному символу
        /// </summary>
        /// <returns>Узел-потомок либо null, если такого узла нет</returns>
        public ITreeNode this[char letter]
        {
            get
            {
                return Children.SingleOrDefault(node => node.Letter == letter);
            }
        }
    }
}
