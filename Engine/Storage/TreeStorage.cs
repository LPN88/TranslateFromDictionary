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

        //public string Replace(string oldLine)
        //{
        //    var strBuilder = new StringBuilder(oldLine.Length);
        //    ContinuousReplace(root,oldLine,ref strBuilder,0);
        //    return strBuilder.ToString();
        //}

        //internal void ContinuousReplace(ITreeNode prevNode, string oldLine,ref StringBuilder strBuilder, int curIndex)
        //{
        //    if (curIndex < oldLine.Length)
        //    {
        //        var newNode = prevNode[oldLine[curIndex]];
        //        if (newNode == null)
        //        {
        //            if (prevNode.Parent == null)
        //            {
        //                strBuilder.Append(oldLine[curIndex]);
        //                ContinuousReplace(root, oldLine, ref strBuilder, ++curIndex);
        //            }
        //            else
        //            {                        
        //                strBuilder.Append(RecursiveReturnBack(prevNode,new StringBuilder(),ref curIndex));
        //                ContinuousReplace(root, oldLine, ref strBuilder, curIndex);
        //            }
        //        }
        //        else
        //        {
        //            if (curIndex == oldLine.Length-1)
        //            {
        //                curIndex++;
        //                strBuilder.Append(RecursiveReturnBack(newNode, new StringBuilder(), ref curIndex));
        //                ContinuousReplace(root, oldLine, ref strBuilder, curIndex);
        //            }
        //            else
        //            {
        //                ContinuousReplace(newNode, oldLine, ref strBuilder, ++curIndex);
        //            }                    
        //        }
        //    }           
            
        //}

        //internal StringBuilder RecursiveReturnBack(ITreeNode prevNode,StringBuilder str, ref int curIndex)
        //{
        //    if (prevNode is TreeLeaf)
        //    {
        //        str.Append((prevNode as TreeLeaf).WordValue);
        //        return str;
        //    }           
        //    else
        //    {
        //        if (prevNode.Parent != null && prevNode.Parent.Parent != null)
        //        {
        //            curIndex--;
        //            if (prevNode.Parent is TreeLeaf)
        //            {
        //                str.Append((prevNode.Parent as TreeLeaf).WordValue);                       
        //            }
        //            else
        //            {                        
        //                RecursiveReturnBack(prevNode.Parent, str, ref curIndex);
        //            }                    
        //        }
        //        else
        //        {
        //            //if (prevNode is TreeLeaf)
        //            //{
        //            //    str.Insert(0, (prevNode as TreeLeaf).WordValue);
        //            //}
        //            //else
        //            //{
        //                str.Insert(0, prevNode.Letter);
        //            //}
                    
        //        }                
        //        return str;
        //    }
        //}
    }
}
