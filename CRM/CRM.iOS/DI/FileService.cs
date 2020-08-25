using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CRM.iOS.DI;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace CRM.iOS.DI
{
    public class FileService : IFileService
    {
        public bool checkExists()
        {
            bool exists = false;
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = System.IO.Path.Combine(path, "crmTest" + ".txt");

            if (System.IO.File.Exists(filename))
                exists = true;
            else
                exists = false;

            return exists;
        }

        public void CreateFile(string id, string fullName, string address)
        {
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            if (Directory.Exists(folderPath))
            {
                Random rand = new Random();
                string filename = System.IO.Path.Combine(folderPath, "crmTest" + ".txt");
                using (var streamWriter = new StreamWriter(filename, true))
                {
                    streamWriter.Write(id + "[" + fullName + "[" + address);
                }
            }
        }

        public void deleteFile()
        {
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            if (Directory.Exists(folderPath))
            {
                Random rand = new Random();
                string filename = System.IO.Path.Combine(folderPath, "crmTest" + ".txt");
                File.Delete(filename);
            }
        }

        public void exitApp()
        {
            throw new NotImplementedException();
        }

        public string ReadFiles()
        {
            string content = "";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            if (Directory.Exists(folderPath))
            {

                string filename = System.IO.Path.Combine(folderPath, "crmTest" + ".txt");

                using (var streamReader = new StreamReader(filename))
                {
                    content = streamReader.ReadToEnd();

                }
            }
            return content;
        }
    }
}