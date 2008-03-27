using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Woofy
{
    public static class FrameworkElementExtensions
    {
        public static BindingExpressionBase SetBinding(this FrameworkElement frameworkElement, DependencyProperty dependencyProperty, Object source, string propertyPath)
        {
            Binding binding = new Binding { Source = source, Path = new PropertyPath(propertyPath)};
            return frameworkElement.SetBinding(dependencyProperty, binding);
        }
    }
}
