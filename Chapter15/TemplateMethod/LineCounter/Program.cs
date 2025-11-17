using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            string path = @"C:\Users\infosys\source\repos\OOP2025\Chapter06\Sectiongram.cs";
            TextProcessor.Run<LineCounterProcessor>(path);
        }
    }
}