using System;
using System.Windows.Input;
using Apex.MVVM;

namespace SOAPClient
{
    public class Command : ICommand
    {
        public Command(Action action, bool canExecute = true)
        {
            this.action = action;
            this.canExecute = canExecute;
        }
        public Command(Action<object> parameterizedAction, bool canExecute = true)
        {
            this.parameterizedAction = parameterizedAction;
            this.canExecute = canExecute;
        }

        protected Action action = null;
        protected Action<object> parameterizedAction = null;
        private bool canExecute = false;

        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }

        void ICommand.Execute(object parameter)
        {
            this.DoExecute(parameter);

        }

        public event EventHandler CanExecuteChanged;
        public event CancelCommandEventHandler Executing;
        public event CommandEventHandler Executed;

        protected void InvokeAction(object param)
        {
            Action theAction = action;
            Action<object> theParameterizedAction = parameterizedAction;
            if (theAction != null)
                theAction();
            else if (theParameterizedAction != null)
                theParameterizedAction(param);
        }

        protected void InvokeExecuted(CommandEventArgs args)
        {
            Executed?.Invoke(this, args);
        }

        protected void InvokeExecuting(CancelCommandEventArgs args)
        {
            Executing?.Invoke(this, args);
        }

        public virtual void DoExecute(object param)
        {
            //  Вызывает выполнении команды с возможностью отмены
            CancelCommandEventArgs args =
               new CancelCommandEventArgs() { Parameter = param, Cancel = false };
            InvokeExecuting(args);

            //  Если событие было отменено -  останавливаем.
            if (args.Cancel)
                return;

            //  Вызываем действие с / без параметров, в зависимости от того. Какое было устанвленно.
            InvokeAction(param);

            //  Call the executed function.
            InvokeExecuted(new CommandEventArgs() { Parameter = param });
        }
    }
}
