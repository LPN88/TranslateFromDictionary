using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Storage;

namespace Engine.Translator
{
    public interface ITranslator
    {
        void Translate(TextReader reader,TextWriter writer);
    }
}
