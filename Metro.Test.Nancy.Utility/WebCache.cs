using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using Metro.Test.Nancy.Interfaces;

namespace Metro.Test.Nancy.Utility
{
    public class WebCache : ICache
    {
        public WebCache() { }

        protected virtual Cache Cache
        {
            get
            {
                return HttpRuntime.Cache ?? (Cache)null;
            }
        }

        public virtual void Insert<T>(string key, T value, DateTime absoluteExpiration) where T : class
        {
            Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public virtual void Insert<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public virtual T Get<T>(string key) where T : class
        {
            object o = Cache.Get(key);
            if (o is T)
            {
                return (T)o;
            }
            return default(T);
        }

        public virtual void Insert(string key, object value, DateTime absoluteExpiration)
        {
            Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public virtual void Insert(string key, object value, TimeSpan slidingExpiration)
        {
            Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public virtual object Get(string key)
        {
            return Cache.Get(key);
        }

        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }

        public virtual void Clear()
        {
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Cache.Remove(enumerator.Key.ToString());
            }
        }

        public virtual object this[string key]
        {
            get
            {
                return Cache.Get(key);
            }
            set
            {
                Cache.Insert(key, value);
            }
        }


        public T Fetch<T>(string key, Func<T> getData) where T : class
        {
            return Fetch(key, getData, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);   
        }

        public T Fetch<T>(string key, Func<T> getData, DateTime absoluteExpiration) where T : class
        {
            return Fetch(key, getData, absoluteExpiration, Cache.NoSlidingExpiration);   
        }

        public T Fetch<T>(string key, Func<T> getData, TimeSpan slidingExpiration) where T : class
        {
            return Fetch(key, getData, Cache.NoAbsoluteExpiration, slidingExpiration);   
        }

        private T Fetch<T>(string key, Func<T> getData, DateTime absoluteExpiration, TimeSpan slidingExpiration) where T : class
        {
            T value;
            object o = Cache.Get(key);
            if (o == null)
            {
                value = getData();
				if (value == null)
					return default(T);

                Cache.Insert(key, value, null, absoluteExpiration, slidingExpiration);
                return value;
            }
            else if (o is T)
            {
                return (T)o;
            }
            return default(T);  
        }
    }
}
