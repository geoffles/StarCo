using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StarCo
{
    public class CommandHandler : ICommand
    {
        private Func<bool> CanExecuteDelegate;
        private Action Action;

        public CommandHandler(Action action)
        {
            Action = action;
        }

        public CommandHandler(Func<bool> canExecute, Action action)
        {
            CanExecuteDelegate = canExecute;
            Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null || CanExecuteDelegate();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (Action != null)
            {
                Action();
            }
        }
    }

    public class CommandHandler<T> : ICommand where T:class
    {
        private Func<T, bool> CanExecuteDelegate;
        private Action<T> Action;

        public CommandHandler(Action<T> action)
        {
            Action = action;
        }

        public CommandHandler(Func<T, bool> canExecute, Action<T> action)
        {
            CanExecuteDelegate = canExecute;
            Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null || CanExecuteDelegate(parameter as T);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (Action != null)
            {
                Action(parameter as T);
            }
        }
    }
}
