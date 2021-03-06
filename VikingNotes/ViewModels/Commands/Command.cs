﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class Command : ICommand
    {
        Action<object> executeMethod;
        private Predicate<object> canExecutePredicate;

        public Command(Action<object> execute)
            : this(execute, null)
        {
        }

        public Command(Action<object> executeMethod, Predicate<object> canExecute)
        {
            this.executeMethod = executeMethod;
            this.canExecutePredicate = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecutePredicate == null ? true : this.canExecutePredicate(parameter);
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }
    }
}
