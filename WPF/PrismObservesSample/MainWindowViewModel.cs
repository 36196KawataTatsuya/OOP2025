using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrismObservesSample {
    public class MainWindowViewModel : BindableBase {
        private string _input1 = string.Empty;
        public string Input1 {
            get => _input1;
            set => SetProperty(ref _input1, value);
        }

        private string _input2 = string.Empty;
        public string Input2 {
            get => _input2;
            set => SetProperty(ref _input2, value);
        }

        private string _result = string.Empty;
        public string Result {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        //コンストラクタ
        public MainWindowViewModel() {
            SumCommand = new DelegateCommand(ExecuteSum);
            Clear = new DelegateCommand(ExecuteClear);
        }

        public DelegateCommand Clear { get; }
        public DelegateCommand SumCommand { get; }

        //足し算の処理
        private void ExecuteSum() {
            if (canExecuteSum()) {
                Result = (int.Parse(Input1) + int.Parse(Input2)).ToString();
            } else {
                Result = "Execution Error"; // エラーメッセージを表示
            }
        }

        //足し算可能性の検証
        private bool canExecuteSum() {
            bool isNum1Valid = int.TryParse(Input1, out int num1);
            bool isNum2Valid = int.TryParse(Input2, out int num2);
            if (isNum1Valid && isNum2Valid) { return true; }
            return false;
        }

        //クリアメソッド
        private void ExecuteClear() {
            Input1 = string.Empty;
            Input2 = string.Empty;
            Result = string.Empty;
        }


    }
}
