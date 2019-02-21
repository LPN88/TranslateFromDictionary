using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Storage;

namespace Engine.Translator
{
    //Плохой переводчик(через стандартный Replace)
    public class BadTranslator: SimpleTranslator, ITranslator
    {
        //список задач для парал. выполнения
        private List<Task> _tasks = new List<Task>();
      
        private TaskFactory _factory;

        public BadTranslator(TextReader reader, ISimpleStorage storage, int maxThreadCount = 0) : base(reader, storage)
        {
            if (maxThreadCount == 0)
            {
                _factory = new TaskFactory();
            }
            else
            {
                //если maxThreadCount!=0, то создаем планировщик для управления кол-ом одновременно создаваемых потоков
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
            _tasks.Add(_factory.StartNew(() =>
            {
                foreach (var pair in _storage.MainDictionary)
                {
                    line = line.Replace(pair.Key, pair.Value);
                }
                return line;
            }));           
        }
    }
}
