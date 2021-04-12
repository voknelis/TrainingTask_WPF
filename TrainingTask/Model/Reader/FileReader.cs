using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTask.Model
{
    public abstract class FileReader<T> : IReader<T>
    {
        public abstract IEnumerable<T> Read();

        public abstract void Write(IEnumerable<T> array);

    }
}
