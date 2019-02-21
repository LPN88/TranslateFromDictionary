using Engine.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Storage
{
    public interface IStorage
    {
        void Add(string wordKey, string wordValue);       

    }

    public interface ITreeStorage: IStorage
    {
        ITreeNode RootNode { get; }
    }

    public interface ISimpleStorage : IStorage
    {
        IDictionary<string,string> MainDictionary { get; }
    }
}
