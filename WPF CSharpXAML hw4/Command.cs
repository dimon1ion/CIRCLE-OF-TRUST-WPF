using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_CSharpXAML_hw4
{
    class Command : ICommand
    {
        private Action executeMethod;

        public Command(Action executeMethod)
        {
            this.executeMethod = executeMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeMethod();
        }

        public event EventHandler CanExecuteChanged;
    }
}
