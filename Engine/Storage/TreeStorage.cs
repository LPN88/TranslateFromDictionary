using Engine.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Storage
{
    //Хранилище-дерево для усовершенств. переводчика
    public class TreeStorage: ITreeStorage
    {
        //корневой узел
        private readonly ITreeNode _root = new TreeNode(' ', null);

        public ITreeNode RootNode { get { return _root; } }

        //добавляем словарь в корень дерева
        public void Add(string wordKey,string wordValue)
        {
            var chars = wordKey.ToCharArray();
            ITreeNode currentNode = _root;
            for (int idx = 0; idx < chars.Length; idx++)
            {
                //получаем дочерний узел или создаем его 
                if (wordKey.StartsWith("i5"))
                {
                    var letter1 = chars[idx];
                }
                var letter = chars[idx];
                var child = currentNode[letter];
                if (child == null)
                    currentNode.Children.Add(child = new TreeNode(letter,currentNode));
                //последнюю букву в слове заменяем листом
                if (idx == chars.Length - 1)
                {                    
                    if (wordKey.Equals("i"))
                    {
                        var letter1 = chars[idx];
                    }
                    var i = currentNode.Children.IndexOf(child);
                    currentNode.Children[i] = new TreeLeaf(child, wordKey, wordValue);
                    //и обязательно обновляем ссылки для дочерних узлов!(долго не мог понять что ошибка из-за этого.....)
                    foreach (var ch in currentNode.Children[i].Children)
                    {
                        ch.Parent = currentNode.Children[i];
                    }
                }              
                //переходим к дочернему узлу
                currentNode = child;               
            }
        }
     
    }
}
