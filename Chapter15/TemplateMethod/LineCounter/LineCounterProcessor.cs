using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor{
        private int _count;
        private string _search = string.Empty;

        protected override void Initialize(string fname) {
            _count = 0;
            Console.Write("単語数検索：");
            _search = Console.ReadLine() ?? string.Empty;
        }
        protected override void Execute(string line) {
            _count += line.Split(_search).Length - 1;
        }

        protected override void Terminate() => Console.WriteLine("{0}の個数：{1}", _search, _count);
            
    }
}
