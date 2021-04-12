using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTask.Model
{
    public interface IReader<T>
    {
        IEnumerable<T> Read();

        void Write(IEnumerable<T> array);
    }
}
