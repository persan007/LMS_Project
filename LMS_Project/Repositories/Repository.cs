using LMS_Project.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LMS_Project.Repositories
{
    public class Repository
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns the filenames of all files currently in the database
        /// </summary>
        /// <returns></returns>
        public string[] GetAllFilenames()
        {
            List<FileObjectModels> files = db.FilesObjects.ToList();
            string[] filenames = new string[files.Count()];
            int index = 0;
            foreach (var file in files)
            {
                filenames[index] = file.Filename;
                index++;
            }
            return filenames;
        }

        /// <summary>
        /// Returns a URL to the file with corresponding filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetUrlByFilename(string fileName = "ppap.png")
        {
            FileObjectModels file = db.FilesObjects.Single(f => f.Filename == fileName);
            if (file != null)
                return string.Format("data:{0};base64,{1}", file.ContentType, Convert.ToBase64String(file.Data));
            return null;
        }

        /// <summary>
        /// Converts byte[] bytes to a readable string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string DeBlobber(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Generate blob from file and return said value
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public byte[] FileToBlob(string filepath)
        {
            //A stream of bytes that represents the binary file
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            //The reader reads the binary data from the file stream
            BinaryReader reader = new BinaryReader(fs);

            //Bytes from the binary reader stored in BlobValue array
            byte[] BlobValue = reader.ReadBytes((int)fs.Length);

            //Close stream
            fs.Close();
            //Close reader
            reader.Close();

            //Return Blob
            return BlobValue;
        }

        /// <summary>
        /// Convert file to blob, generate a FileObjectmodel and add to database
        /// </summary>
        /// <param name="files"></param>
        /// <param name="mapPath"></param>
        public void UploadFiles(HttpFileCollectionBase files, string mapPath)
        {
            for (int i = 0; i < files.Count; i++)
            {
                //Get file, filename and path
                var file = files[i];
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(mapPath, fileName);
                file.SaveAs(path);

                //Get Blob value from file via filepath
                byte[] bytes = FileToBlob(path);

                //Check if file exists in database
                DataExists(bytes, fileName, file.ContentType);
                
                //If there is a file in the temp folder, remove it
                DeleteFile(mapPath + fileName);
            }
        }

        /// <summary>
        /// Creates and returns a new FileObjectModel based on input
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="bytes"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private FileObjectModels CreateModel(string fileName, byte[] bytes, string contentType)
        {
            //Create relevant model
            FileObjectModels model  = new FileObjectModels();
            model.Filename          = fileName;
            model.Data              = bytes;
            model.ContentType       = contentType;
            return model;
        }

        /// <summary>
        /// Check and Delete file based on filepath
        /// </summary>
        /// <param name="fullPath"></param>
        private bool DeleteFile(string filepath)
        {
            //Check if file exists, if it does delete it
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
                return true;
            }
            return false;  
        }

        /// <summary>
        /// Check if file already exists in database. If not, add to database
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        private bool DataExists(byte[] bytes, string fileName, string contentType)
        {
            //Check if model already exists
            bool dataExists = db.FilesObjects.Any(m => m.Data.Equals(bytes));
            if (!dataExists)
            {
                db.FilesObjects.Add(CreateModel(fileName, bytes, contentType));
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns an array of all LessonModels in the database
        /// </summary>
        /// <returns></returns>
        public LessonModels[] GetAllLessons()
        {
            return db.Lessons.ToArray();
        }

        /// <summary>
        /// Returns an array with the names of the roles in the database
        /// </summary>
        /// <returns></returns>
        public string[] GetAllRoleNames()
        {
            var roles = db.Roles.ToList();
            string[] roleNames = new string[roles.Count()];
            for (int i = 0; i < roles.Count(); i++)
            {
                roleNames[i] = roles[i].Name;
            }
            return roleNames;
        }
    }
}