using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForUniversity
{
    interface IExamSystem
    {
        void Add(long studentId, long courseId);
        void Remove(long studentId, long courseId);
        bool Contains(long studentId, long courseId);
    }
}
