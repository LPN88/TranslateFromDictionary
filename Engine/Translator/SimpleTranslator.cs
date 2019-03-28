using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Storage;
using System.IO;

namespace Engine.Translator
{
    public abstract class SimpleTranslator : ITranslator
    {
        protected readonly TextReader _reader;

        protected readonly ISimpleStorage _storage;

        public SimpleTranslator(TextReader reader, ISimpleStorage storage)
        {
            _reader = reader;
            _storage = storage;
        }
        public virtual void Translate(TextReader reader, TextWriter writer)
        {

        }
    }
}
