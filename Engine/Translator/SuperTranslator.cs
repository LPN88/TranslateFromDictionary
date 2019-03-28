using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Engine.Storage;
using Engine.Tree;

namespace Engine.Translator
{
    //Хороший переводчик
    public class SuperTranslator: TreeTranslator, ITranslator
    {
        //список задач для парал. выполнения
        private List<Task> _tasks = new List<Task>();

        private TaskFactory _factory;

        public SuperTranslator(TextReader reader, ITreeStorage storage, int maxThreadCount=0): base(reader,storage)
        {           
            if (maxThreadCount==0)
            {
                _factory = new TaskFactory();
            }
            else
            {
                //если maxThreadCount!=0, то создаем планировщик для управления кол-ом параллельно создаваемых потоков
                LimitedConcurrencyLevelTaskScheduler lcts = new LimitedConcurrencyLevelTaskScheduler(maxThreadCount);
                _factory = new TaskFactory(lcts);
            }         
            string line = null;
            while ((line = _reader.ReadLine()) != null)
            {
                _storage.Add(line.Split('\t')[0], line.Split('\t')[1]);
            }           
        }

        //перевод всего текста
        public override void Translate(TextReader reader, TextWriter writer)
        {           
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {               
                TranslateLine(line);
            }
            Task.WhenAll(_tasks);
            foreach (Task<string> task in _tasks)
            {
                writer.WriteLine(task.Result);
            }
            writer.Flush();
        }

        //перевод строки
        private void TranslateLine(string line)
        {          
            _tasks.Add(_factory.StartNew(() => Replace(line)));
            //var task = _factory.StartNew(() => _storage.Replace(line));
            //var ww = task.Result;
        }

        //замена строки на переведенную
        public string Replace(string oldLine)
        {
            var strBuilder = new StringBuilder(oldLine.Length);
            ContinuousReplace(_storage.RootNode, oldLine, ref strBuilder, 0);
            return strBuilder.ToString();
        }

        //Непрерывная замена с учетом самого длинного слова в строке
        private void ContinuousReplace(ITreeNode prevNode, string oldLine, ref StringBuilder strBuilder, int curIndex)
        {
            if (curIndex < oldLine.Length)
            {
                var newNode = prevNode[oldLine[curIndex]];
                if (newNode == null)
                {
                    if (prevNode.Parent == null)
                    {
                        strBuilder.Append(oldLine[curIndex]);
                        ContinuousReplace(_storage.RootNode, oldLine, ref strBuilder, ++curIndex);
                    }
                    else
                    {
                        strBuilder.Append(RecursiveReturnBack(prevNode, new StringBuilder(), ref curIndex));
                        ContinuousReplace(_storage.RootNode, oldLine, ref strBuilder, curIndex);
                    }
                }
                else
                {
                    if (curIndex == oldLine.Length - 1)
                    {
                        curIndex++;
                        strBuilder.Append(RecursiveReturnBack(newNode, new StringBuilder(), ref curIndex));
                        ContinuousReplace(_storage.RootNode, oldLine, ref strBuilder, curIndex);
                    }
                    else
                    {
                        ContinuousReplace(newNode, oldLine, ref strBuilder, ++curIndex);
                    }
                }
            }

        }

        //Возврат назад после отсутсвия найденной подстроки в дереве и нахождения подходящего более короткого слова из словаря
        private StringBuilder RecursiveReturnBack(ITreeNode prevNode, StringBuilder str, ref int curIndex)
        {
            if (prevNode is TreeLeaf)
            {
                str.Append((prevNode as TreeLeaf).WordValue);
                return str;
            }
            else
            {
                if (prevNode.Parent != null && prevNode.Parent.Parent != null)
                {
                    curIndex--;
                    if (prevNode.Parent is TreeLeaf)
                    {
                        str.Append((prevNode.Parent as TreeLeaf).WordValue);
                    }
                    else
                    {
                        RecursiveReturnBack(prevNode.Parent, str, ref curIndex);
                    }
                }
                else
                {                   
                    str.Insert(0, prevNode.Letter);                    
                }
                return str;
            }
        }
    }
}
