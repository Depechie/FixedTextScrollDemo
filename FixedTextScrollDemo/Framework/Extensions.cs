using System.Windows;
using System.Windows.Media;

namespace FixedTextScrollDemo.Framework
{
    public static class Extensions
    {
        public static void SetVerticalOffset(this FrameworkElement fe, double offset)
        {
            var translateTransform = fe.RenderTransform as TranslateTransform;
            if (translateTransform == null)
            {
                // create a new transform if one is not alreayd present
                var trans = new TranslateTransform()
                {
                    Y = offset
                };
                fe.RenderTransform = trans;
            }
            else
            {
                translateTransform.Y = offset;
            }
        }
    }

    public struct Offset
    {
        public double Value { get; set; }
        public TranslateTransform Transform { get; set; }
    }
}
