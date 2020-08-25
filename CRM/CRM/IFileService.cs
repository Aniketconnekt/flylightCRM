using System;
using System.Collections.Generic;
using System.Text;

namespace CRM
{
   public interface IFileService
    {
        void CreateFile(string id, string fullName,string address);
        string ReadFiles();
        void deleteFile();
        void exitApp();
        bool checkExists();
    }
}
