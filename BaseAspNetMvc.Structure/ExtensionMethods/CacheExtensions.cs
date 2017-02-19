using System.Collections;
using System.Text;
using System.Web.Caching;

namespace System.Web
{
    public static class CacheExtensions
    {
        //private static readonly object Sync = new object();
        public const int DefaultCacheExpiration = 20;

        /// <summary>
        /// Allows Caching of typed data
        /// </summary>
        /// <example><![CDATA[
        /// var user = HttpRuntime
        ///   .Cache
        ///   .GetOrStore<User>(
        ///      string.Format("User{0}", _userId), 
        ///      () => Repository.GetUser(_userId));
        /// ]]></example>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cache">calling object</param>
        /// <param name="key">Cache key</param>
        /// <param name="generator">Func that returns the object to store in cache</param>
        /// <returns></returns>
        /// <remarks>Uses a default cache expiration period as defined in <see cref="CacheExtensions.DefaultCacheExpiration"/></remarks>
        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<TResult> generator)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator() : default(TResult), DefaultCacheExpiration);
        }

        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<int, TResult> generator, int generatorParam1)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator(generatorParam1) : default(TResult), DefaultCacheExpiration);
        }

        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<string, TResult> generator, string generatorParam1)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator(generatorParam1) : default(TResult), DefaultCacheExpiration);
        }

        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<int, string, TResult> generator, int generatorParam1, string generatorParam2)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator(generatorParam1, generatorParam2) : default(TResult), DefaultCacheExpiration);
        }

        /// <summary>
        /// Allows Caching of typed data
        /// </summary>
        /// <example><![CDATA[
        /// var user = HttpRuntime
        ///   .Cache
        ///   .GetOrStore<User>(
        ///      string.Format("User{0}", _userId), 
        ///      () => Repository.GetUser(_userId));
        ///
        /// ]]></example>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cache">calling object</param>
        /// <param name="key">Cache key</param>
        /// <param name="generator">Func that returns the object to store in cache</param>
        /// <param name="expireInMinutes">Time to expire cache in minutes</param>
        /// <returns></returns>
        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<TResult> generator, double expireInMinutes)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator() : default(TResult), expireInMinutes);
        }

        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<int, TResult> generator, int generatorParam1, double expireInMinutes)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator(generatorParam1) : default(TResult), expireInMinutes);
        }

        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<string, TResult> generator, string generatorParam1, double expireInMinutes)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator(generatorParam1) : default(TResult), expireInMinutes);
        }

        public static TResult GetOrStore<TResult>(this Cache cache, string key, Func<int, string, TResult> generator, int generatorParam1, string generatorParam2, double expireInMinutes)
        {
            return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator(generatorParam1, generatorParam2) : default(TResult), expireInMinutes);
        }

        /// <summary>
        /// Allows Caching of typed data
        /// </summary>
        /// <example><![CDATA[
        /// var user = HttpRuntime
        ///   .Cache
        ///   .GetOrStore<User>(
        ///      string.Format("User{0}", _userId),_userId));
        ///
        /// ]]></example>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">calling object</param>
        /// <param name="key">Cache key</param>
        /// <param name="obj">Object to store in cache</param>
        /// <returns></returns>
        /// <remarks>Uses a default cache expiration period as defined in <see cref="CacheExtensions.DefaultCacheExpiration"/></remarks>
        public static T GetOrStore<T>(this Cache cache, string key, T obj)
        {
            return cache.GetOrStore(key, obj, DefaultCacheExpiration);
        }

        /// <summary>
        /// Allows Caching of typed data
        /// </summary>
        /// <example><![CDATA[
        /// var user = HttpRuntime
        ///   .Cache
        ///   .GetOrStore<User>(
        ///      string.Format("User{0}", _userId), 
        ///      () => Repository.GetUser(_userId));
        ///
        /// ]]></example>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">calling object</param>
        /// <param name="key">Cache key</param>
        /// <param name="obj">Object to store in cache</param>
        /// <param name="expireInMinutes">Time to expire cache in minutes</param>
        /// <returns></returns>
        public static T GetOrStore<T>(this Cache cache, string key, T obj, double expireInMinutes)
        {
            var result = cache[key];

            if (result == null && expireInMinutes > 0)
            {
                lock (typeof(CacheExtensions))
                {
                    // Double check in case another thread added it while we were acquiring the lock
                    if (result == null && expireInMinutes > 0)
                    {
                        result = obj != null ? obj : default(T);
                        if (result == null)
                        {
                            throw new ArgumentNullException("Null value for key : " + key + " in GetOrStore");
                        }

                        cache.Insert(key, result, null, DateTime.Now.AddMinutes(expireInMinutes), Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)result;
        }

        /// <summary>
        /// Allow Remove Single Item by Key
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        public static void RemoveItem(this Cache cache, string key)
        {
            if (HttpContext.Current != null)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// Allows deleting cache content.
        /// </summary>
        /// <param name="cache">calling object</param>
        /// <returns></returns>
        public static void ClearCache(this Cache cache)
        {
            if (HttpContext.Current != null)
            {
                foreach (DictionaryEntry entry in cache)
                {
                    cache.Remove((string)entry.Key);
                }
            }
        }

        /// <summary>
        /// Allows printing cache content.
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public static string PrintCacheContent(this Cache cache)
        {
            var s = new StringBuilder();
            s.Append("<table>");
            s.Append("<tr>");
            s.Append("<td><b>Key</b></td>");
            s.Append("<td><b>Value</b></td>");
            s.Append("</tr>");

            if (HttpContext.Current != null)
            {
                foreach (DictionaryEntry entry in cache)
                {
                    s.Append("<tr>");

                    s.Append("<td>");
                    s.Append((string)entry.Key);
                    s.Append("</td>");

                    s.Append("<td>");
                    s.Append(cache[(string)entry.Key]);
                    s.Append("</td>");

                    s.Append("</tr>");
                }
            }
            s.Append("</table>");

            return s.ToString();
        }
    }
}
