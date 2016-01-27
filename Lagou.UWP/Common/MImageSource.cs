using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

//http://stackoverflow.com/questions/34362838/storing-a-bitmapimage-in-localfolder-uwp
//https://github.com/iFixit/iFixit-WP/blob/master/iFixit7/ImgCache.cs

namespace Lagou.UWP.Common {
    public class ImgCache {
        public const string BASE_PATH = "imageCache";
        public const string IFIXIT_IMG_URL_BASE = "http://guide-images.ifixit.net/igi/";

        public static BitmapImage RetrieveAndCacheByURL(string url) {
            var img = new BitmapImage(new Uri(url, UriKind.Absolute));
            img.CreateOptions = BitmapCreateOptions.None;
            img.ImageOpened += ImageSaveComplete;

            return img;
        }

        private static void ImageSaveComplete(object sender, RoutedEventArgs e) {
            var bm = sender as BitmapImage;

            string src = bm.UriSource.ToString();
            int lastSlash = src.LastIndexOf('/');
            string guid = src.Substring(lastSlash + 1);

            ImgCache.StoreImage(guid, bm);
        }


        public static async void StoreImage(string guid, BitmapImage img) {
            // Create a filename for file in isolated storage.
            var tempJPEG = BASE_PATH + "\\" + guid + ".jpg";

            // Create virtual store and file stream. Check for duplicate files
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication()) {
                if (!storage.DirectoryExists(BASE_PATH))
                    storage.CreateDirectory(BASE_PATH);

                if (storage.FileExists(tempJPEG)) {
                    storage.DeleteFile(tempJPEG);
                }

                using (var fs = storage.CreateFile(tempJPEG)) {
                    var wb = new WriteableBitmap(img.PixelWidth, img.PixelHeight);
                    var encoder = await BitmapEncoder.CreateAsync(
                        BitmapEncoder.JpegEncoderId, 
                        null);
                    //TODO
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)img.PixelWidth, (uint)img.PixelHeight, 96, 96, null);
                }
            }
        }



        /*
         * A wrapper for retreival by raw URL
         */
        public static BitmapImage GetImageByURL(string url) {
            int lastSlash = url.LastIndexOf('/');
            string guid = url.Substring(lastSlash + 1);

            return GetImage(guid);
        }
        public static BitmapImage GetImage(string guidWithSize) {
            var img = new BitmapImage();
            string fullPath = BASE_PATH + "\\" + guidWithSize + ".jpg";

            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication()) {
                if (!iso.FileExists(fullPath)) {
                    //if we cannot find the image, return null
                    return null;
                }
                using (var fileStream = iso.OpenFile(fullPath, FileMode.Open, FileAccess.Read)) {
                    img.SetSource(fileStream.AsRandomAccessStream());
                    //FIXME need to set the URI source! else we cannot open it later for fullscreen view
                    img.UriSource = new Uri(IFIXIT_IMG_URL_BASE + guidWithSize, UriKind.Absolute);
                    return img;
                }
            }
        }

        /*
         * Get the local path to a cached image by URL
         */
        public static string GetLocalCachePathByURL(string url) {
            int lastSlash = url.LastIndexOf('/');
            string guid = url.Substring(lastSlash + 1);

            string fullPath = BASE_PATH + "\\" + guid + ".jpg";

            return fullPath;
        }
    }


    public class ImageCacheConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            BitmapImage outImage = null;
            string url = value as string;
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
                return null;
            }
            outImage = ImgCache.GetImageByURL(url);
            if (outImage == null) {
                outImage = ImgCache.RetrieveAndCacheByURL(url);
            }
            return outImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
