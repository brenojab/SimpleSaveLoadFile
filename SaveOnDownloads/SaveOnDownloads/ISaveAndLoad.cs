using System;
using System.Collections.Generic;
using System.Text;

namespace SaveOnDownloads
{
    public interface ISaveAndLoad
    {
        void SaveText(string filename, string text);

        string LoadText(string filename);

        void DownloadPdfFile(byte[] bytes, string Name);

        void SaveImage(string name, byte[] file);

        void SaveZip(string fileName, byte[] file);

        byte[] LoadImage(string name);

        byte[] LoadVideo(string name);
        byte[] LoadFileFromPath(string path);

        string GetImageFolder();

        string GetDocumetsFolder();
        byte[] ChangeOrientation(byte[] byteArray);
        byte[] ChangeOrientation(string filename);

        byte[] ChangeOrientation(byte[] byteArray, float degrees);
        byte[] ChangeOrientation(string filename, float degrees);

        string GetDownloadFolder();

    }
}
