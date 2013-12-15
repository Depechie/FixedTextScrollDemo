using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace FixedTextScrollDemo
{
    /// <summary>
    /// Alternative version with actual removal of the border from the scrollviewer!
    /// </summary>
    public partial class MainPage2 : PhoneApplicationPage
    {
        private bool _borderInsideChild = true;

        private ScrollBar _vBar;

        private GeneralTransform _generalTransform;
        private Point _childToParentCoordinates;

        public MainPage2()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            _vBar = ((FrameworkElement)VisualTreeHelper.GetChild(ScrollViewer, 0)).FindName("VerticalScrollBar") as ScrollBar;
            _vBar.ValueChanged += _vBar_ValueChangedHandler;
        }

        private void _vBar_ValueChangedHandler(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_borderInsideChild)
            {
                _generalTransform = TitleBorder.TransformToVisual(ScrollViewer);
                _childToParentCoordinates = _generalTransform.Transform(new Point(0, 0));

                //Debug.WriteLine(_childToParentCoordinates.Y.ToString());

                //If the top of the title border goes out of view from the ScrollViewer, take it out of the TextGrid and put it in the ContentPanel
                if (_childToParentCoordinates.Y < 0)
                {
                    this.TextGrid.Children.Remove(this.TitleBorder);
                    this.ContentPanel.Children.Add(this.TitleBorder);
                    this.TitleBorder.VerticalAlignment = VerticalAlignment.Top;

                    _borderInsideChild = false;
                }
            }
            else
            {
                _generalTransform = Article.TransformToVisual(ScrollViewer);
                _childToParentCoordinates = _generalTransform.Transform(new Point(0, 0));

                //If the top of the RichTextBox is fully in view, put the title border back in the TextGrid
                if (_childToParentCoordinates.Y > 0)
                {
                    this.ContentPanel.Children.Remove(this.TitleBorder);
                    this.TextGrid.Children.Add(this.TitleBorder);
                    this.TitleBorder.VerticalAlignment = VerticalAlignment.Bottom;

                    _borderInsideChild = true;
                }
            }
        }
    }
}