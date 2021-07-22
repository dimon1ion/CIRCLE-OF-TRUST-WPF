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
        public static Func<bool> defaultCanExecute = () => true;
        private Func<bool> canExecute;
        private Action executeMethod;


        public Command(Action _executeMethod) : this(_executeMethod, defaultCanExecute)
        {

        }

        public Command(Action _executeMethod, Func<bool> _canExecute)
        {
            this.canExecute = _canExecute;
            this.executeMethod = _executeMethod;
        }
        public void Check()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            executeMethod();
        }

        public event EventHandler CanExecuteChanged;
    }
}
