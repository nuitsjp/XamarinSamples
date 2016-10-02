using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Reactive.Bindings.Extensions;

namespace PropertyBindingSample
{
    public static class ReactiveBinder
    {
        public static void Bind<T>(Expression<Func<T>> from, Expression<Func<T>> to)
        {
            var name = PropertySupport.ExtractPropertyName(to);
            var memberExpression = to.Body as MemberExpression;
            var constantExpression = memberExpression?.Expression as ConstantExpression;
            var subject = constantExpression?.Value as INotifyPropertyChanged;

            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (subject != null)
            {
                var observable =
                    subject
                        .PropertyChangedAsObservable()
                        .Where(e => e.PropertyName == name)
                        .Select(_ => (T)propertyInfo.GetValue(subject));
                observable.Bind(to);
            }
        }

        public static void Bind<T>(this IObservable<T> observable, Expression<Func<T>> to)
        {
            var name = PropertySupport.ExtractPropertyName(to);
            var memberExpression = to.Body as MemberExpression;
            var constantExpression = memberExpression?.Expression as ConstantExpression;
            if (constantExpression != null)
            {
                var toObject = constantExpression.Value;
                var propertyInfo = toObject.GetType().GetRuntimeProperty(name);
                observable.Subscribe(
                    x =>
                    {
                        propertyInfo.SetValue(toObject, x);
                    });
            }
        }
    }
}
