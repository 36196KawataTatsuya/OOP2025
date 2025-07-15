using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            //問題12.1.1
            var emp = new Employee {
                Id = 123,
                Name = "山田太郎",
                HireDate = new DateTime(2018, 10, 1),
            };
            var jsonString = Serialize(emp);
            Console.WriteLine("【問題12.1.1】シリアル化");
            Console.WriteLine(jsonString);

            var obj = Deserialize(jsonString);
            Console.WriteLine("【問題12.1.1】デシリアル化");
            Console.WriteLine(obj);
            Console.WriteLine("--------------------");


            //問題12.1.2
            Employee[] employees = [
                new () {
                    Id = 123,
                    Name = "山田太郎",
                    HireDate = new DateTime(2018, 10, 1),
                },
                new () {
                    Id = 198,
                    Name = "田中華子",
                    HireDate = new DateTime(2020, 4, 1),
                },
            ];
            // "employees.json" という名前でファイルが出力されます
            Serialize("employees.json", employees);
            Console.WriteLine("【問題12.1.2】employees.json にシリアル化して出力しました。");
            Console.WriteLine("--------------------");


            //問題12.1.3
            Console.WriteLine("【問題12.1.3】employees.json からデシリアル化");
            var empdata = Deserialize_f("employees.json");
            foreach (var empd in empdata)
                Console.WriteLine(empd);
        }

        //問題12.1.1
        static string Serialize(Employee emp) {
            var options = new JsonSerializerOptions {
                WriteIndented = true, // インデントを付けて出力
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), // 日本語をエンコードしない
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // プロパティ名をキャメルケースにする
            };
            string jsonString = JsonSerializer.Serialize(emp, options);
            return jsonString;
        }

        static Employee? Deserialize(string text) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // キャメルケースのJSONプロパティ名をデシリアル化
            };
            return JsonSerializer.Deserialize<Employee>(text, options);
        }

        //問題12.1.2
        //シリアル化してファイルへ出力する
        static void Serialize(string filePath, IEnumerable<Employee> employees) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            var jsonString = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(filePath, jsonString);
        }

        //問題12.1.3
        //デシリアル化+return
        static Employee[] Deserialize_f(string filePath) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            var text = File.ReadAllText(filePath);
            var employees = JsonSerializer.Deserialize<Employee[]>(text, options);
            return employees ?? [];

        }

    }
    public record Employee {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
    }
}