using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld {

    class ViewModel : BindableBase {
        public ViewModel() {
            ChangeMessageCommand = new DelegateCommand(
                () => GreetingMessage = "See you next time");
        }

        private string _greetingMessage = "Hello World!";
        public string GreetingMessage {
            get => _greetingMessage;
            set => SetProperty(ref _greetingMessage, value);
        }

        public DelegateCommand ChangeMessageCommand { get; }

    }
}
