using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ForUniversity
{
    class LockingTable : IExamSystem
    {
        private Dictionary<long, List<long>> _table = new Dictionary<long, List<long>>();
        private Mutex _lock = new Mutex();
        
        public void Add(long studentId, long courseId)
        {
            _lock.WaitOne();

            if (_table.ContainsKey(studentId))
            {
                if (!_table[studentId].Contains(courseId))
                {
                    _table[studentId].Add(courseId);
                }
                _lock.ReleaseMutex();
                return;
            }

            _table.Add(studentId, new List<long> { courseId });
            _lock.ReleaseMutex();
        }

        public void Remove(long studentId, long courseId)
        {

            _lock.WaitOne();

            if (_table.ContainsKey(studentId))
            {
                _table[studentId].Remove(courseId);
                _lock.ReleaseMutex();
                return;
            }
            _lock.ReleaseMutex();
        }

        public bool Contains(long studentId, long courseId)
        {
            _lock.WaitOne();
            if (_table.ContainsKey(studentId))
            {
                bool res = _table[studentId].Contains(courseId);
                _lock.ReleaseMutex();
                return res;
            }
            _lock.ReleaseMutex();
            return false;
        }
    }
}
