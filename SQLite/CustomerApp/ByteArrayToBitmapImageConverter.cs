using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CustomerApp {
    /// <summary>
    /// byte[]をBitmapImage(WPFで表示可能な画像)に変換するクラス
    /// </summary>
    public class ByteArrayToBitmapImageConverter : IValueConverter {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            // byte[] から BitmapImage への変換
            if (value is byte[] imageData && imageData.Length > 0) {
                try {
                    var image = new BitmapImage();
                    using (var mem = new MemoryStream(imageData)) {
                        mem.Position = 0;
                        image.BeginInit();
                        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = null;
                        image.StreamSource = mem;
                        image.EndInit();
                    }
                    image.Freeze();
                    return image;
                }
                catch {
                    // 画像データが不正な場合は null を返す
                    return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            // byte[] への逆変換をサポートしない
            throw new NotImplementedException();
        }
    }
}