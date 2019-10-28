using Android.Content;
using Android.Graphics;
using SaveOnDownloads.Droid;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Path = System.IO.Path;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace SaveOnDownloads.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {
            //var localFolder = Android.App.Application.Context.GetExternalFilesDir(null).ToString();
            //var filePath = System.IO.Path.Combine(localFolder, filename);
            //File.WriteAllText(filePath, text);

            var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/download/";


            var save = Path.Combine(path.ToString(), filename + ".txt");

            File.WriteAllText(save, text);
        }

        public string LoadText(string filename)
        {
            var filePath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/download/", filename + ".txt");
            return File.ReadAllText(filePath);
        }

        public void DownloadPdfFile(byte[] bytes, string Name)
        {
            // Write pdf File
            var directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            directory = System.IO.Path.Combine(directory, Android.OS.Environment.DirectoryDownloads);
            string filePath = System.IO.Path.Combine(directory.ToString(), Name);
            File.WriteAllBytes(filePath, bytes);

            //Open the Pdf file with Defualt app.
            Android.Net.Uri pdfPath = Android.Net.Uri.FromFile(new Java.IO.File(filePath));
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(pdfPath, "application/pdf");
            intent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intent);
        }

        public void SaveImage(string fileName, byte[] file)
        {
            try
            {
                var filePath = System.IO.Path.Combine(GetImageFolder(), fileName);
                using (var stream = new MemoryStream(file))
                {
                    using (var bitmap = Android.Graphics.BitmapFactory.DecodeStream(stream))
                    {
                        if (!Directory.Exists(GetImageFolder()))
                            Directory.CreateDirectory(GetImageFolder());

                        using (var fs = new FileStream(filePath, System.IO.FileMode.OpenOrCreate))
                        {
                            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, fs);
                        }
                    }

#if DEBUG
                    var rotate = ChangeOrientation(fileName, -90);
                    var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                    var pictures = dir.AbsolutePath;
                    string filePathGallery = System.IO.Path.Combine(pictures, fileName + ".jpg");
                    System.IO.File.WriteAllBytes(filePathGallery, rotate);
# endif
                }



            }
            catch
            {
                throw;
            }
        }

        public void SaveZip(string fileName, byte[] file)
        {
            try
            {
                // Write pdf File
                var directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                directory = System.IO.Path.Combine(directory, Android.OS.Environment.DirectoryDownloads);
                string filePath = System.IO.Path.Combine(directory.ToString(), fileName);
                File.WriteAllBytes(filePath, file);
            }
            catch
            {
                throw;
            }
        }

        public byte[] LoadImage(string fileName)
        {
            var filePath = System.IO.Path.Combine(GetImageFolder(), fileName);
            var memoryStream = new MemoryStream();

            using (var source = System.IO.File.OpenRead(filePath))
            {
                source.CopyTo(memoryStream);
            }

            return memoryStream.ToArray();
        }

        public byte[] LoadVideo(string fileName)
        {
            var filePath = System.IO.Path.Combine(GetVideoFolder(), fileName);
            var memoryStream = new MemoryStream();

            using (var source = System.IO.File.OpenRead(filePath))
            {
                source.CopyTo(memoryStream);
            }

            return memoryStream.ToArray();
        }

        public string GetImageFolder()
        {
            var localFolder = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures).ToString();
            return System.IO.Path.Combine(localFolder, "Mercantil do Brasil");
        }

        public string GetVideoFolder()
        {
            var localFolder = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryMovies).ToString();
            return System.IO.Path.Combine(localFolder, "DefaultVideos");
        }

        public string GetDocumetsFolder()
        {
            var localFolder = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).ToString();
            return System.IO.Path.Combine(localFolder, "Mercantil do Brasil");
        }

        public byte[] LoadFileFromPath(string path)
        {
            var memoryStream = new MemoryStream();

            using (var source = System.IO.File.OpenRead(path))
            {
                source.CopyTo(memoryStream);
            }

            return memoryStream.ToArray();
        }

        public byte[] ChangeOrientation(byte[] byteArray, float degrees)
        {
            var filename = "Rotate";

            SaveImage(filename, byteArray);

            return ChangeOrientation(filename, degrees);
        }

        public byte[] ChangeOrientation(string fileName, float degrees)
        {
            var filePath = System.IO.Path.Combine(GetImageFolder(), fileName);
            Bitmap bmp;
            using (BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = false })
            {
                var byteArray = File.ReadAllBytes(filePath);
                bmp = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length, options);
                using (Matrix matrix = new Matrix())
                {
                    matrix.PostRotate(degrees);
                    if (bmp != null)
                        using (bmp = Bitmap.CreateBitmap(bmp, 0, 0, bmp.Width, bmp.Height, matrix, true))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bmp.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, ms);


                                return ms.ToArray();

                            }
                        }
                    else
                        return byteArray;
                }
            }



        }

        public byte[] ChangeOrientation(byte[] byteArray)
        {
            throw new System.NotImplementedException();
        }

        public byte[] ChangeOrientation(string filename)
        {
            throw new System.NotImplementedException();
        }

        public string GetDownloadFolder()
        {
            return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        }
    }
}