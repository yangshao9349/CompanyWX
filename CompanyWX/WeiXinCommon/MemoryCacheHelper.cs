using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SRX.WeiXinCommon
{
    public enum MyCachePriority
    {
        Default,
        NotRemovable
    }

    public class MemoryCacheHelper
    {
        static readonly ObjectCache cache = MemoryCache.Default;

        public static T AddOrGetExisting<T>(string key, Func<T> createNew)
        {

            return AddOrGetExisting<T>(key, new TimeSpan(0, 0, 30, 0), createNew);

        }

        public static T AddOrGetExisting<T>(string key, TimeSpan cacheDuration, Func<T> createNew)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (createNew == null) throw new ArgumentNullException("createNew");
            if (!cache.Contains(key))
            {
                cache.Add(key, createNew(), DateTime.Now.Add(cacheDuration));
            }
            return (T)cache[key];
        }
    }
}
