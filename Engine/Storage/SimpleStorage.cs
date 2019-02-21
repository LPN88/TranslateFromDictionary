using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Storage
{
    //Хранилище для простого переводчика
    public class SimpleStorage : ISimpleStorage
    {
        private readonly SortedDictionary<string,string> _dict = new SortedDictionary<string, string>(new ReverseComparer());

        public IDictionary<string, string> MainDictionary { get { return _dict; } }

        public void Add(string wordKey, string wordValue)
        {
            _dict[wordKey]=wordValue;
        }

    }

    //Компаратор для хранилища для простого переводчика
    public sealed class ReverseComparer : IComparer<string>
    {      
        public int Compare(string x, string y)
        {                      
            var equal = y.Length.CompareTo(x.Length);
            if (equal==0)
            {
                return x.CompareTo(y);
            }
            else
            {
                return equal;
            }
        }
    }
}
