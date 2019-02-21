using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Tree
{
    public interface ITreeNode
    {
        List<ITreeNode> Children { get; set; }

        ITreeNode Parent { get; set; }
        
        char Letter { get; set; }

        ITreeNode this[char letter]
        {
            get;          
        }
    }
}
