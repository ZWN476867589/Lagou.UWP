using Lagou.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;

//http://stackoverflow.com/questions/34362838/storing-a-bitmapimage-in-localfolder-uwp

namespace Lagou.UWP.Common {

    public class ThumbImageSource : BitmapSource, IDisposable {

        public static readonly string IMAGE_DIR = "Images";
        public static readonly string THUMB_DIR = "Images/Thumbs";

        private Lazy<HttpClient> Client = new Lazy<HttpClient>(() => new HttpClient());



        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register(
                "Width",
                typeof(int),
                typeof(ThumbImageSource),
                new PropertyMetadata(30, SizeChanged));

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register(
                "Height",
                typeof(int),
                typeof(ThumbImageSource),
                new PropertyMetadata(30, SizeChanged));

        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.Register(
                "UriSource",
                typeof(Uri),
                typeof(ThumbImageSource),
                new PropertyMetadata(null, UriChanged));

        public int Width {
            get {
                return (int)this.GetValue(WidthProperty);
            }
            set {
                this.SetValue(WidthProperty, value);
            }
        }

        public int Height {
            get {
                return (int)this.GetValue(HeightProperty);
            }
            set {
                this.SetValue(HeightProperty, value);
            }
        }

        public Uri UriSource {
            get {
                return (Uri)this.GetValue(UriSourceProperty);
            }
            set {
                this.SetValue(UriSourceProperty, value);
            }
        }

        private static async void SizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if ((int)e.NewValue <= 0)
                throw new ArgumentException("ThumbImageSource width/height 必须大于0");

            var thumb = (ThumbImageSource)d;
            //await thumb.Deal();
        }

        private static async void UriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var thumb = (ThumbImageSource)d;
            try {
                await thumb.Deal();
            } catch (Exception ex) {

            }
        }

        public async Task Deal() {
            var stm = await this.GetThumb(this.UriSource, this.Width, this.Height);
            if (stm != null) {
                await this.SetSourceAsync(stm.AsRandomAccessStream());
            }
        }

        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private async Task<Stream> GetThumb(Uri uri, int width, int height) {
            var hash = MD5.GetHashString(uri.AbsolutePath);

            var file = Path.Combine(IMAGE_DIR, hash);
            var thumb = $"{hash}_{width}_{height}";

            thumb = Path.Combine(THUMB_DIR, thumb);
            var stm = await FileManager.Instance.Value.GetStream(thumb);
            if (stm != null)
                return stm;
            else {
                var orgBytes = await this.GetOrg(uri);
                if (orgBytes != null) {
                    stm = await this.CreateThumb(orgBytes, this.Width, this.Height, thumb);
                    //await FileManager.Instance.Value.Save(stm, thumb);
                    return stm;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取原图
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<Stream> GetOrg(Uri uri) {
            var hash = MD5.GetHashString(uri.AbsolutePath);
            var file = Path.Combine(IMAGE_DIR, hash);
            var stm = await FileManager.Instance.Value.GetStream(file);
            if (stm != null) {
                return stm;
            } else {
                var bytes = await this.Download(uri);
                if (bytes != null) {
                    await FileManager.Instance.Value.Save(bytes, file);
                    return new MemoryStream(bytes);
                }
            }

            return null;
        }

        private async Task<byte[]> Download(Uri uri) {
            this.Client.Value.CancelPendingRequests();
            try {
                //return await this.Client.Value.GetStreamAsync(uri);
                return await this.Client.Value.GetByteArrayAsync(uri);
            } catch {
                return null;
            }
        }

        private async Task<Stream> CreateThumb(Stream orgStm, int width, int height, string filePath) {

            var ras = orgStm.AsRandomAccessStream();
            var wb = new WriteableBitmap(width, height);
            ras.Seek(0);
            await wb.SetSourceAsync(ras);

            var stm = wb.PixelBuffer.AsStream();
            var bytes = await stm.GetBytes();

            using (var fs = await FileManager.Instance.Value.OpenFile(filePath)) {
                //BitmapEncoder 无法写入 MemoryStream
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(
                    BitmapEncoder.JpegEncoderId,
                    fs);

                encoder.BitmapTransform.ScaledWidth = (uint)width;
                encoder.BitmapTransform.ScaledHeight = (uint)height;
                //encoder.BitmapTransform.Rotation = BitmapRotation.Clockwise90Degrees;

                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Straight,
                    (uint)wb.PixelWidth,
                    (uint)wb.PixelHeight,
                    96.0,
                    96.0,
                    bytes);

                await encoder.FlushAsync();
                return fs.CloneStream().AsStream();
            }
        }

        ~ThumbImageSource() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (this.Client.Value != null) {
                    this.Client.Value.Dispose();
                }
            }
        }
    }
}
