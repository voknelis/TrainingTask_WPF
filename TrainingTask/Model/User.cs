using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace TrainingTask.Model
{
    public class User: ObservableObject
    {
        private string name;
        private uint age;
        private UserRole role;

        [XmlElement("Name")]
        public string Name
        {
            get => name;
            set { Set<string>(() => this.Name, ref name, value); }
        }

        [XmlElement("Age")]
        public uint Age
        {
            get => age;
            set { Set<uint>(() => this.Age, ref age, value); }
        }

        [XmlElement("Role")]
        public UserRole Role
        {
            get => role;
            set { Set<UserRole>(() => this.Role, ref role, value); }
        }
    }


    [XmlRoot("Users")]
    public class RootUsers
    {
        [XmlElement("User")]
        public User[] Users { get; set; }
    }
}
