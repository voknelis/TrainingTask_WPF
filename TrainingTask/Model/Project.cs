using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTask.Model
{

    public class Project: ObservableObject
    {

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                Set(() => Name, ref name, Name);
            }
        }

        private ProjectType? type;
        public ProjectType? Type
        {
            get => type;
            set
            {
                type = value;
                Set(() => Type, ref type, Type);
            }
        }

        public bool Visible
        {
            get
            {
                if (this.Type == ProjectType.External)
                    return true;
                return false;
            }
        }

        public KnownProjectType KnownProject
        {
            get
            {
                if (this.Type == ProjectType.Unknown)
                    return KnownProjectType.No;
                return KnownProjectType.Yes;
            }
        }

        public List<UserRole> PossibleRoles
        {
            get
            {
                switch (this.Type)
                {
                    case ProjectType.Intern:
                        return new List<UserRole>() { UserRole.Owner };

                    case ProjectType.External:
                        return new List<UserRole>() { UserRole.Owner, UserRole.Editor, UserRole.Reader };

                    case ProjectType.Unknown:
                        return new List<UserRole>() { UserRole.Owner, UserRole.Guest };

                    default:
                        return null;
                }
            }
        }
    }

    public class RootJson
    {
        public List<Project> Projects { get; set; }
    }
}
