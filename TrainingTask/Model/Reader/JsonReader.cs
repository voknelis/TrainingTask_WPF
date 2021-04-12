using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTask.Model
{
    public class JsonReader<T> : FileReader<T>
    {

        /// <summary>
        /// Return a list of data readed from the file. If no file or exception is apeeared, return blank list
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<T> Read()
        {
            string filePath = GetFilePath();

            string readedItems;
            List<T> items;
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    readedItems = sr.ReadToEnd();
                }
                items = JsonConvert.DeserializeObject<List<T>>(readedItems);
            }
            catch (FileNotFoundException)
            {
                return new List<T>();
            }
            return items;
        }

        /// <summary>
        /// Write list to the file
        /// </summary>
        /// <param name="list"></param>
        public override void Write(IEnumerable<T> list)
        {
            var jsonProjs = JsonConvert.SerializeObject((IEnumerable<Project>)list,Formatting.Indented);

            string filePath = GetFilePath();
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(jsonProjs);
            }
        }

        /// <summary>
        /// Return a string with full path to the read/write file
        /// </summary>
        /// <returns></returns>
        private string GetFilePath()
        {
            //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string docPath = Properties.Settings.Default.ResoursesPath;
            string usersFileName = Properties.Settings.Default.ProjectsFileName;
            return Path.Combine(docPath, usersFileName);
        }
    }
}
