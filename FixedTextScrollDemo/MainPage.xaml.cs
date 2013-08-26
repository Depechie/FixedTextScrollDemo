using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using FixedTextScrollDemo.Framework;
using Microsoft.Phone.Controls;

namespace FixedTextScrollDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private double _borderTop;
        private ScrollBar _vBar;

        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Get top of TitleBorder
            _borderTop = this.Image.ActualHeight - this.TitleBorder.ActualHeight + 4;

            //http://social.msdn.microsoft.com/Forums/wpapps/en-US/81fcd34e-6ec9-48d0-891e-c53a53344553/scrollviewer-synchronization
            _vBar = ((FrameworkElement)VisualTreeHelper.GetChild(ScrollViewer, 0)).FindName("VerticalScrollBar") as ScrollBar;
            _vBar.ValueChanged += _vBar_ValueChangedHandler;
        }

        private void _vBar_ValueChangedHandler(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue < _borderTop)
            {
                if (e.NewValue >= 0)
                    this.TitleBorder.SetVerticalOffset(0 - e.NewValue);
            }
            else
                this.TitleBorder.SetVerticalOffset(0 - _borderTop);
        }

        //http://blogs.msdn.com/b/jasongin/archive/2011/04/13/pull-down-to-refresh-a-wp7-listbox-or-scrollviewer.aspx
        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            UIElement scrollContent = (UIElement)this.ScrollViewer.Content;
            CompositeTransform ct = scrollContent.RenderTransform as CompositeTransform;
            if (ct != null && ct.TranslateY > 0)
                this.TitleBorder.SetVerticalOffset(ct.TranslateY);
        }

        private void ScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UIElement scrollContent = (UIElement)this.ScrollViewer.Content;
            CompositeTransform ct = scrollContent.RenderTransform as CompositeTransform;
            if (ct != null)
            {
                if(ct.TranslateY > 0)
                    this.TitleBorder.SetVerticalOffset(0);
            }
        }  
    }
}