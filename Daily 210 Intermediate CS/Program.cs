using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Daily_210_Intermediate_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var width = 1000;
            var height = 100;
            var startColor = Color.FromArgb(204, 119, 34);
            var endColor = Color.FromArgb(1, 66, 37);
            var gradient = new HorizontalGradient(width, height, startColor, endColor);

            using (var bitmap = new Bitmap(width, height))
            {
                for (var col = 0; col < width; col++)
                {
                    var pixel = gradient.GetColor(col, 0);
                    for (int row = 0; row < height; row++)
                    {
                        bitmap.SetPixel(col, row, pixel);
                    }
                }
                bitmap.Save("gradient.png", ImageFormat.Png);
            }
        }
    }

    public class HorizontalGradient
    {
        private readonly Color _startColor;
        private readonly Color _endColor;
        private readonly int _maxCols;
        private readonly int _maxRows;
        private readonly int _deltaRed;
        private readonly int _deltaGreen;
        private readonly int _deltaBlue;

        public HorizontalGradient(int width, int height, Color startColor, Color endColor)
        {
            _startColor = startColor;
            _endColor = endColor;
            _maxCols = width;
            _maxRows = height;

            if (width < 1 || height < 1)
            {
                throw new ArgumentException("Width/Height need to be more than 0");
            }

            _deltaRed = _endColor.R - _startColor.R;
            _deltaGreen = _endColor.G - _startColor.G;
            _deltaBlue = _endColor.B - _startColor.B;
        }

        public Color GetColor(int col, int row)
        {
            if (col >= _maxCols || row >= _maxRows || col < 0 || row < 0)
            {
                throw new ArgumentException("row/col out of range");
            }

            var multiplier = (col + 1.0) / _maxCols;
            var red = (int)(_startColor.R + _deltaRed * multiplier);
            var green = (int)(_startColor.G + _deltaGreen * multiplier);
            var blue = (int)(_startColor.B + _deltaBlue * multiplier);

            return Color.FromArgb(red, green, blue);
        }
    }
}
