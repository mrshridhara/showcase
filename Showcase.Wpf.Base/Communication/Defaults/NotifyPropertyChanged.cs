using Showcase.Wpf.Base.Extensions;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Showcase.Wpf.Base.Communication.Defaults
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected async Task RaisePropertyChangedAsync<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                string propertyName = propertyExpression.ToPropertyName();

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }, DispatcherPriority.DataBind);
        }
    }
}
