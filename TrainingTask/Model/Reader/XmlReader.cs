using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TrainingTask.Model
{
    class XmlReader<T> : FileReader<T>
    {
        private XDocument xDoc;

        /// <summary>
        /// Return a list of data readed from the file. If no file or exception is apeeared, return blank list
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<T> Read()
        {
            string filePath = GetFilePath();
            List<T> items;

            XmlSerializer xs = new XmlSerializer(typeof(List<T>));

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                    items = (List<T>)xs.Deserialize(sr);
            }
            catch (Exception)
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
            string filePath = GetFilePath();

            // delete namespase in .xml document
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            // xml don't work with IEnumerable so cast it to Array
            var arr = list.ToList<T>();
            XmlSerializer xs = new XmlSerializer(typeof(List<T>));

            try
            {
                using (StreamWriter sr = new StreamWriter(filePath))
                    xs.Serialize(sr, arr, ns);
            }
            catch (Exception)
            {
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
            string usersFileName = Properties.Settings.Default.UsersFileName;
            return Path.Combine(docPath, usersFileName);
        }

    }
}
