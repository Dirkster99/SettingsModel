namespace SettingsModelWPFDemo.ViewModels.Base.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class ViewModelBase : INotifyPropertyChanged
    {
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            // Cache the handler to make this thread safe
            var handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Tell bound controls (via WPF binding) to refresh their display.
        /// 
        /// Sample call: this.NotifyPropertyChanged(() => this.IsSelected);
        /// where 'this' is derived from <seealso cref="BaseViewModel"/>
        /// and IsSelected is a property.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        public void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
                memberExpression = (MemberExpression)lambda.Body;

            RaisePropertyChanged(memberExpression.Member.Name);
        }

        /// <summary>
        /// Gets an instance of the service container and retrieves the requested service coponent.
        /// </summary>
        /// <typeparam name="TServiceContract"></typeparam>
        /// <returns></returns>
        public TServiceContract GetService<TServiceContract>() where TServiceContract : class
        {
            return ServiceLocator.ServiceContainer.Instance.GetService<TServiceContract>();
        }
    }
}
