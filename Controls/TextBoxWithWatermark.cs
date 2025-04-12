using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GuitarWorkshop.Controls
{
    public class WatermarkTextBox : TextBox
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register(
                "Watermark",
                typeof(string),
                typeof(WatermarkTextBox),
                new PropertyMetadata(string.Empty, OnWatermarkChanged));

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WatermarkTextBox textBox)
            {
                textBox.InitializeWatermark();
            }
        }

        public WatermarkTextBox()
        {
            InitializeWatermark();
            GotFocus += (s, e) => RemoveWatermark();
            LostFocus += (s, e) => ShowWatermark();
        }

        private void InitializeWatermark()
        {
            if (string.IsNullOrEmpty(Text))
            {
                ShowWatermark();
            }
        }

        private void ShowWatermark()
        {
            if (string.IsNullOrEmpty(Text))
            {
                Text = Watermark;
                Foreground = Brushes.Gray;
            }
        }

        private void RemoveWatermark()
        {
            if (Text == Watermark)
            {
                Text = string.Empty;
                Foreground = Brushes.Black;
            }
        }
    }
}