using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvvm.Commands;

namespace Mvvm.ViewModels
{
     class MainWindowViewMidel:NotificationObject
    {
        private double  input1;
        public double Input1
        {
            get { return input1; }
            set {
                input1 = value;
                Result = input2 + Input1;
                this.RaisePropertyChange("Input1");
            }
        }
        private double input2;
        public double Input2
        {
            get { return input2; }
            set
            {
                input2 = value;
                Result = input2 + Input1;
                this.RaisePropertyChange("Input2");
            }
        }
        private double result;
        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                this.RaisePropertyChange("Result");
            }
        }

        //命令属性
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        private void Add(object paramater)
        {
            this.Result = this.Input1 + this.Input2;
        }

        public MainWindowViewMidel()
        {
            this.AddCommand = new DelegateCommand();
            this.AddCommand.ExecuteAction = new Action<object>(this.Add);
        }
    }
}
