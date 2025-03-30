using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    // Can not use lock object to synchronize the access to the cache because it will block the read operation
    // ReaderWriterLock Sample    
    public class GlobalConfiguraionCache() 
    {
        private Dictionary<int,string> _cache = new Dictionary<int, string>();
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        public void Add(int key, string value)
        {
            bool lockAquired = false;
            try
            {
                _lock.EnterWriteLock();
                lockAquired = true;
                _cache.Add(key, value);
            }
            finally
            {
                if (lockAquired)
                    _lock.ExitWriteLock();
                
            }            
        }
        public string Get(int key)
        {
            bool lockAquired = false;
            try
            {
                _lock.EnterReadLock();
                lockAquired = true;
                return _cache.TryGetValue(key, out string value) ? value : null;
            }
            finally
            {
                if (lockAquired)
                    _lock.ExitReadLock();
            }
        }
    }

}
