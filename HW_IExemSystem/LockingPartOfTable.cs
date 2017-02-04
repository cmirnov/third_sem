using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ForUniversity
{
    class LockingPartOfTable : IExamSystem
    {
        List<KeyValuePair<long, long>>[] _table;
        private volatile List<int> _studetsToChange = new List<int>();
        private Mutex _lock = new Mutex();
        private int _copacity;
        private long _numOfRecords;
        private int _mod = 1024; //num of locking elem.

        public LockingPartOfTable ()
        {
            _copacity = _mod;
            _table = new List<KeyValuePair<long, long>>[_copacity];
            for (int i = 0; i < _copacity; i++)
            {
                _table[i] = new List<KeyValuePair<long, long>>();
            }
            _numOfRecords = 0;
        }

        private int GetHash(long studentId, long courseId)
        {
            return (int)(studentId * 2047 + courseId) % ((int)1e9 + 9);
        }

        private void LockStudent(int studentId)
        {
            while (true)
            {
                lock (_studetsToChange)
                {
                    if (_studetsToChange.Contains(studentId))
                    {
                        Monitor.Wait(_studetsToChange);
                    }
                    else
                    {
                        _studetsToChange.Add(studentId);
                        Monitor.PulseAll(_studetsToChange);
                        return;
                    }
                }
            }
        }

        private void UnlockStudent(int studentId)
        {
            lock (_studetsToChange)
            {
                _studetsToChange.Remove(studentId);
                Monitor.PulseAll(_studetsToChange);
            }
        }

        private void Resize(long studentId, int oldCopacity)
        {
            for (int i = 0; i < _mod; i++)
            {
                if (i != studentId)
                    LockStudent(i);
            }

            if (_copacity != oldCopacity)
            {
                for (int i = 0; i < _mod; i++)
                {
                    if (i != studentId)
                        UnlockStudent(i);
                }
                return;
            }

            _copacity = 2 * _copacity;
            List<KeyValuePair<long, long>>[] oldTable = _table;
            _table = new List<KeyValuePair<long, long>>[_copacity];
            for (int i = 0; i < _copacity; i++)
            {
                _table[i] = new List<KeyValuePair<long, long>>();
            }
            for (int i = 0; i < oldCopacity; i++)
            {
                List<KeyValuePair<long, long>> record = oldTable[i];
                for (int j = 0; j < record.Count; j++)
                {
                    int hash = GetHash(record[j].Key, record[j].Value) % _copacity;
                    _table[hash].Add(record[j]);
                }
            }
            for (int i = 0; i <  _mod; i++)
            {
                if (i != studentId)
                    UnlockStudent(i);
            }
            return;
        }

        public void Add(long studentId, long courseId)
        {
            int hash = GetHash(studentId, courseId) % _mod;

            LockStudent(hash);

            hash = GetHash(studentId, courseId) % _copacity;
            
            
            if (_table[hash].Count == 0)
            {
                if (Interlocked.Read(ref _numOfRecords) + 1 == _copacity)
                {
                    Resize(hash, _copacity);
                    hash = GetHash(studentId, courseId) % _copacity;
                }
                Interlocked.Increment(ref _numOfRecords);
                _table[hash].Add(new KeyValuePair<long, long>(studentId, courseId));
                hash = GetHash(studentId, courseId) % _mod;
                UnlockStudent(hash);
                return;
            }

            if (!_table[hash].Contains(new KeyValuePair<long, long>(studentId, courseId)))
            {
                _table[hash].Add(new KeyValuePair<long, long>(studentId, courseId));
                hash = GetHash(studentId, courseId) % _mod;
                UnlockStudent(hash);
                return;
            }
            hash = GetHash(studentId, courseId) % _mod;
            UnlockStudent(hash);
        }

        public void Remove(long studentId, long courseId)
        {
            int hash = GetHash(studentId, courseId) % _mod;
            
            LockStudent(hash);

            hash = GetHash(studentId, courseId) % _copacity; //protect from resize

            if (_table[hash].Contains(new KeyValuePair<long, long>(studentId, courseId)))
            {
                _table[hash].Remove(new KeyValuePair<long, long>(studentId, courseId));

                hash = GetHash(studentId, courseId) % _mod;

                UnlockStudent(hash);
                return;
            }

            hash = GetHash(studentId, courseId) % _mod;

            UnlockStudent(hash);
        }

        public bool Contains(long studentId, long courseId)
        {
          //  Console.WriteLine("here");
            int hash = GetHash(studentId, courseId) % _mod;

            LockStudent(hash);

            hash = GetHash(studentId, courseId) % _copacity; //protect from resize

            if (_table[hash].Contains(new KeyValuePair<long, long>(studentId, courseId)))
            {
                bool res = _table[hash].Contains(new KeyValuePair<long, long>(studentId, courseId));
                hash = GetHash(studentId, courseId) % _mod;
                UnlockStudent(hash);
                return res;
            }
            hash = GetHash(studentId, courseId) % _mod;
            UnlockStudent(hash);
            return false;
        }
    }
}
