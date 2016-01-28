using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Lagou.UWP.Common {
    public class FileManager : IDisposable {

        public static Lazy<FileManager> Instance = new Lazy<FileManager>(() => new FileManager());

        private IsolatedStorageFile ISF = null;
        private StorageFolder Folder = null;

        private readonly AsyncLock _SaveLock = new AsyncLock();

        private FileManager() {
            this.ISF = IsolatedStorageFile.GetUserStoreForApplication();
            this.Folder = ApplicationData.Current.LocalFolder;
        }

        public async Task<Stream> GetStream(string file) {
            //return await this.Folder.OpenStreamForReadAsync(file);
            using (await this._SaveLock.LockAsync()) {
                if (string.IsNullOrWhiteSpace(file))
                    throw new ArgumentNullException("file");

                if (this.ISF.FileExists(file))
                    return await Task.FromResult(this.ISF.OpenFile(file, FileMode.Open, FileAccess.Read));
                else
                    return null;
            }
        }

        public async Task Save(Stream stm, string file) {
            if (stm == null || string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException("stm or file is null");

            var bytes = await stm.GetBytes();
            await this.Save(bytes, file);
        }

        public async Task Save(byte[] bytes, string file) {
            using (await _SaveLock.LockAsync()) {
                await this.InternalSave(bytes, file);
            }
        }

        private async Task InternalSave(byte[] bytes, string file) {
            if (bytes == null || string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException("stm or file is null");

            if (bytes.Length == 0)
                return;

            var dir = Path.GetDirectoryName(file);
            if (!this.ISF.DirectoryExists(dir))
                this.ISF.CreateDirectory(dir);

            if (this.ISF.FileExists(file))
                this.ISF.DeleteFile(file);

            using (var fs = this.ISF.CreateFile(file)) {
                await fs.WriteAsync(bytes, 0, bytes.Length);
                await fs.FlushAsync();
            }
        }

        public async Task<IRandomAccessStream> OpenFile(string file) {
            using (await this._SaveLock.LockAsync()) {
                var folder = await this.Folder.CreateFolderAsync(Path.GetDirectoryName(file), CreationCollisionOption.OpenIfExists);
                var f = await folder.CreateFileAsync(Path.GetFileName(file), CreationCollisionOption.ReplaceExisting);
                return await f.OpenAsync(FileAccessMode.ReadWrite);
            }
        }









        //public async Task DeleteCacheFile() {
        //    try {
        //        StorageFolder folder = await _local_folder.GetFolderAsync("images_cache");
        //        if (folder != null) {
        //            IReadOnlyCollection<StorageFile> files = await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
        //            //files.ToList().ForEach(async (f) => await f.DeleteAsync(StorageDeleteOption.PermanentDelete));
        //            List<IAsyncAction> list = new List<IAsyncAction>();
        //            foreach (var f in files) {
        //                list.Add(f.DeleteAsync(StorageDeleteOption.PermanentDelete));
        //            }
        //            List<Task> list2 = new List<Task>();
        //            list.ForEach((t) => list2.Add(t.AsTask()));

        //            await Task.Run(() => { Task.WaitAll(list2.ToArray()); });
        //        }
        //    } catch {

        //    }
        //}
        //public async Task<double> GetCacheSize() {
        //    try {
        //        StorageFolder folder = await _local_folder.GetFolderAsync("images_cache");
        //        var files = await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
        //        double size = 0; BasicProperties p;
        //        foreach (var f in files) {
        //            p = await f.GetBasicPropertiesAsync();
        //            size += p.Size;
        //        }
        //        return size;
        //    } catch {
        //        return 0;
        //    }
        //}


        ~FileManager() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (this.ISF != null) {
                    this.ISF.Dispose();
                }
            }
        }
    }
}
