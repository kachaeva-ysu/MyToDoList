﻿using System.IO;

namespace RestoreLib
{
    public class File : IFile
    {
        public string FullName { get; private set; }

        public File(string path)
        {
            //System.IO.File.Open(path, FileMode.Open);
            FullName = path;
        }

        public IFile CopyTo(string path)
        {
            System.IO.File.Copy(FullName, path, true);
            return new File(path);
        }

        public void MoveTo(string path)
        {
            System.IO.File.Move(FullName, path);
            FullName = path;
        }

        public void Delete()
        {
            System.IO.File.Delete(FullName);
        }
    }
}