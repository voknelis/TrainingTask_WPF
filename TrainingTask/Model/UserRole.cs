using System;

namespace TrainingTask.Model
{
    [Flags]
    public enum UserRole
    {
        Owner = 0,
        Editor = 1,
        Reader = 2,
        Guest = 4
    }
}
