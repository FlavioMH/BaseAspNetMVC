using System.Collections.Generic;
using System.Data;           
using System.Web.Script.Serialization;  

namespace System.Web
{
    public static class UtilityExtensions
    {
        /// <summary>
        /// Serializes object into string with JavaScriptSerializer class.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// Serializes object into string with JavaScriptSerializer class.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="recursionDepth">The number of object levels to process.</param>
        /// <returns></returns>
        public static string ToJSON(this object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer { RecursionLimit = recursionDepth };
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// Serializes DataTable object into string with JavaScriptSerializer class.
        /// </summary>
        /// <param name="dt">The DataTable object to serialize.</param>
        /// <returns></returns>
        public static string ToJSON(this DataTable dt)
        {
            var serializer = new JavaScriptSerializer();
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        /// <summary>
        /// Serializes DataSet object into string (processing only the first table in it) with JavaScriptSerializer class.
        /// </summary>
        /// <param name="ds">The DataSet with one DataTable object to serialize.</param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToList(this DataSet ds)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                list.Add(dictRow);
            }

            return list;
        }
    }
}
