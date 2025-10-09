using Prism.Commands;
using Prism.Mvvm;
using SampleUnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace SampleUnitConverter {
    internal class MainWindowViewModel : BindableBase {
        //フィールド
        private double _metricValue;
        private double _imperialValue;
        private MetricUnit _currentMetricUnit;
        private ImperialUnit _currentImperialUnit;

        //▲で呼ばれるコマンド
        public DelegateCommand ImperialUnitToMetric { get; private set; }

        //▼で呼ばれるコマンド
        public DelegateCommand MetricToImperialUnit { get; private set; }

        //上のComboBoxで選択されている値
        public MetricUnit CurrentMetricUnit {
            get => _currentMetricUnit;
            set => SetProperty(ref _currentMetricUnit, value);
        }

        //下のComboBoxで選択されている値
        public ImperialUnit CurrentImperialUnit {
            get => _currentImperialUnit;
            set => SetProperty(ref _currentImperialUnit, value);
        }

        #region プロパティ
        public double MetricValue {
            get => _metricValue;
            set => SetProperty(ref _metricValue, value);
        }

        public double ImperialValue {
            get => _imperialValue;
            set => SetProperty(ref _imperialValue, value);
        }
        #endregion

        public MainWindowViewModel() {
            CurrentMetricUnit = MetricUnit.Units.First();
            CurrentImperialUnit = ImperialUnit.Units.First();

            ImperialUnitToMetric = new DelegateCommand(
                () => MetricValue =
                CurrentMetricUnit.FromImperialUnit(CurrentImperialUnit, ImperialValue));

            MetricToImperialUnit = new DelegateCommand(
                () => ImperialValue =
                CurrentImperialUnit.FromMetricUnit(CurrentMetricUnit, MetricValue));
        }
    }
}