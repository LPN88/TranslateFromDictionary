using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Tree
{
    internal class TreeLeaf: TreeNode, ITreeNode
    {
        /// <summary>
        /// Слово ключ
        /// </summary>
        public string WordKey { get; set; }

        /// <summary>
        /// Слово значение
        /// </summary>
        public string WordValue { get; set; }

        public TreeLeaf(ITreeNode node, string word,string wordValue) : base(node.Letter, node.Parent)
        {
            WordKey = word;
            WordValue = wordValue;
            this.Children = node.Children;
        }
    }
}
