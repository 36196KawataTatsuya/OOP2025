using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor {
        private int _count = 0;
        private string _searchWord = "";

        protected override void Initialize(string fname) {
            _count = 0;
            Console.Write("カウントしたい単語：");
            _searchWord = Console.ReadLine();
        }
        protected override void Execute(string line) {
            _count += line.Split(_searchWord).Length - 1;
        }

        protected override void Terminate() => Console.WriteLine("{0}の個数：{1}", _searchWord, _count);

    }
}