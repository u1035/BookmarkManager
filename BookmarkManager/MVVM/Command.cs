using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace BookmarkManager.MVVM
{
    /// <summary>
    /// Provides methods for easy working with command buttons
    /// </summary>
    public class Command : CommandBase
    {
        private readonly Action _executeMethod;
        private readonly Func<bool>? _canExecuteMethod;

        public Command(Action executeMethod, Func<bool>? canExecuteMethod = null, bool forcedUpdateCanExecute = true)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;

            //Additional handler to emulate one eternal client which asks about current CanExecute on each state
            if (forcedUpdateCanExecute)
                this.CanExecuteChanged += delegate { CanExecute(); };
        }

        public bool IsCanExecute
        {
            get => _isCanExecute;
            private set => SetProperty(ref _isCanExecute, value);
        }
        private bool _isCanExecute;

        
        public bool CanExecute()
        {
            IsCanExecute = _canExecuteMethod == null || _canExecuteMethod();
            return IsCanExecute;
        }

        public sealed override bool CanExecute(object? parameter)
        {
            return CanExecute();
        }

        public void Execute()
        {
            if (!CanExecute())
                return;

            _executeMethod.Invoke();
        }

        public sealed override void Execute(object? parameter)
        {
            this.Execute();
        }
    }


    public class Command<T> : CommandBase
    {
        private readonly Action<T?> _executeMethod;
        private readonly Func<T?, bool>? _canExecuteMethod;

        public Command(Action<T?> executeMethod, Func<T?, bool>? canExecuteMethod = null)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(T? parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod(parameter);
        }

        public sealed override bool CanExecute(object? parameter)
        {
            if (_canExecuteMethod == null)
                return true;

            if (parameter is T t)
                return CanExecute(t);

            if (parameter == null)
                return CanExecute(null);

            return false;
        }

        public void Execute(T? parameter)
        {
            if (!CanExecute(parameter))
                return;

            _executeMethod.Invoke(parameter);
        }

        public sealed override void Execute(object? parameter)
        {
            Execute((T?)parameter);
        }
    }


    public abstract class CommandBase : NotificationObject, ICommand
    {
        protected CommandBase()
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
        }

        public abstract bool CanExecute(object? parameter);

        public abstract void Execute(object? parameter);

        public void RaiseCanExecuteChanged()
        {
            //We're working in main UI thread
            if (!UseDispatcherForCanExecuteChanged || Dispatcher.CheckAccess())
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                //We're working in separate thread
                //We use async invocation in order not to pause current running thread by UI refreshes
                Dispatcher.InvokeAsync(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty));
            }
        }

        public Dispatcher Dispatcher { get; }

        public bool UseDispatcherForCanExecuteChanged { get; set; } = true;

        public event EventHandler? CanExecuteChanged;
    }
}
