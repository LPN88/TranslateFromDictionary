using Engine.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Translator
{
    public abstract class TreeTranslator: ITranslator
    {
        protected readonly TextReader _reader;

        protected readonly ITreeStorage _storage;

        public TreeTranslator(TextReader reader, ITreeStorage storage)
        {           
            _reader = reader;
            _storage = storage;           
        }
        public virtual void Translate(TextReader reader, TextWriter writer)
        {

        }
    }

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
