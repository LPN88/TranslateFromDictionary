using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.Storage;
using Engine.Translator;
using Unity;
using Unity.Resolution;

namespace TranslateFromDictionary_ConsoleApp
{
    class Program
    {
        //использовать ли супер транслятор
        static bool isSuperTranslator = true;

        static readonly string _assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        //путь к словарю для перевода
        static string dictPath = _assemblyPath + @"\..\..\dict.txt";

        //входные данные
        static string inputPath = _assemblyPath + @"\..\..\in.txt";

        //выходные данные
        static string outputPath = _assemblyPath + @"\..\..\out2.txt";

        //возможные символы для перевода
        static char[] _array = (new List<int[]> { new[] { 32,32 }, new[] { 48, 57 }, new[] { 97, 122 } })
                    .SelectMany<int[], char>((ar) =>
                    {
                        List<char> dd = new List<char>();
                        for (int ii = ar[0]; ii <= ar[1]; ii++)
                        {
                            dd.Add((char)ii);
                        };
                        return dd;
                    }).ToArray();

        static void Main(string[] args)
        {
            FillDict();
            FillIn();
            int maxThreadCount = 0;
            var readerDict = new StreamReader(dictPath);
            var reader = new StreamReader(inputPath);
            var writer = new StreamWriter(outputPath);
            var container = BuildUnityContainer();            
            var translator = container.Resolve<ITranslator>(new ParameterOverride(typeof(TextReader), readerDict), new ParameterOverride(typeof(int), maxThreadCount));    
            var watcher = Stopwatch.StartNew();
            translator.Translate(reader, writer);
            watcher.Stop();
            reader.Close();
            writer.Close();
            Console.WriteLine("Перевод занял: "+ watcher.ElapsedMilliseconds+"мс.");
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        
        //регистрируем зависимости
        static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ITreeStorage, TreeStorage>();
            currentContainer.RegisterType<ISimpleStorage, SimpleStorage>();
            if (!isSuperTranslator)
            {
                currentContainer.RegisterType<ITranslator, BadTranslator>();
            }
            else
            {
                currentContainer.RegisterType<ITranslator, SuperTranslator>();
            }            
            return currentContainer;
        }

        //заполняем словарь для перевода
        static void FillDict()
        {
            using (var fs = new StreamWriter(dictPath,false))
            {
                var strList = new List<string>(100000);
                StringBuilder builder = new StringBuilder();
                var r = new Random();
                for (int i = 0; i < 100000; i++)
                {                    
                    var length = i % 10000 == 0 ? 1 : i % 5001 == 0 ? 2 : i % 2001 == 0 ? 3 : i % 1001 == 0 ? 5 : i % 501 == 0 ? 6 : i % 101 == 0 ? 7 :  8;       
                    for (int ii = 0; ii < length; ii++)
                    {
                        builder.Append(_array[r.Next(_array.Length)]);
                    }
                    var key = builder.ToString();
                    builder.Append("\t");
                    builder.Append('"'+key+"replaced" + '"');                   
                    strList.Add(builder.ToString());
                    builder.Clear();
                }
                fs.WriteLine(@"red fast ferrari"+'\t'+@"""красная быстрая феррари""");
                fs.WriteLine(@"i5 processor" + '\t' + @"""и5 проц""");
                foreach (var item in strList.OrderBy(l => l.Length))
                {
                    fs.WriteLine(item);
                }                
            }
        }

        //заполняем входные данные
        private static void FillIn()
        {
            var r = new Random();
            using (var fs = new StreamWriter(inputPath, false))
            {
                fs.WriteLine(@"red fast ferrarii5redi5 processor");
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        fs.Write(_array[r.Next(_array.Length)]);
                    }
                    fs.WriteLine();
                }
            }         
        }
    }
}
