using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomBackend
{
    public class SqlHelper
    {
        public static string SafeString(MySqlDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
            {
                return string.Empty;
            }
            else
            {
                return reader.GetString(field);
            }
        }
        public static int SafeInt(MySqlDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
            {
                return 0;
            }
            else
            {
                return reader.GetInt32(field);
            }
        }


    }
}