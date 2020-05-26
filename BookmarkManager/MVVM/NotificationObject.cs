using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookmarkManager.MVVM
{
    /// <summary>
    /// Wrapper class for INotifyPropertyChanged
    /// </summary>
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        // ReSharper disable once InconsistentNaming
        private event PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                var prevValue = _propertyChanged;
                _propertyChanged += value;
                if (prevValue == null && _propertyChanged != null)
                    OnPropertyChangedSubscribed();
            }
            remove
            {
                var prevValue = _propertyChanged;
                _propertyChanged -= value;
                if (prevValue != null && _propertyChanged == null)
                    OnPropertyChangedUnsubscribed();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnPropertyChangedSubscribed()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnPropertyChangedUnsubscribed()
        {

        }

        /// <summary>
        /// Sets property value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="storage">Value storage</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>true if previous value was different than new</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;
            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {

        }

        /// <summary>
        /// Raises OnPropertyChanged event
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
