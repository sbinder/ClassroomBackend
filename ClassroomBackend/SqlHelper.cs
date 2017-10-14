using ClassroomBackend.Models;
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

        public static uint SafeUInt(MySqlDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
            {
                return 0;
            }
            else
            {
                return reader.GetUInt32(field);
            }
        }

        private static string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        MySqlConnection db = new MySqlConnection(connectionString);


        public List<Student> StudentList() // possibly take dates or months as input parameter
        {
            var slist = new List<Student>();
            MySqlCommand cmd = db.CreateCommand();
            MySqlCommand acmd = db.CreateCommand();
            // add upper limit:
            cmd.CommandText = "select stid, target, fname, lname, liturgy, torah, haftara from student where target > CURDATE()";
            acmd.CommandText = "select stid, checkin, status from attendance where checkin > CURDATE() order by stid, checkin";
            db.Open();
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student
                        {
                            stid = SafeUInt(reader, "stid"),
                            target = reader.GetDateTime("target"),
                            fname = SafeString(reader, "fname"),
                            lname = SafeString(reader, "lname"),
                            group = SafeInt(reader, "liturgy"),
                            torah = SafeString(reader, "torah"),
                            haftara = SafeString(reader, "haftara")
                        };
                        slist.Add(student);
                    }
                }
                using (MySqlDataReader reader = acmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var kid = slist.Find(s => s.stid == SafeUInt(reader, "stid"));
                        kid.present = reader.GetBoolean("status");
                    }
                }
            } catch (Exception e)
            {
                // log exception here.
            }
            return slist;
        }

        public string UpdateProgress(Progress progress)
        {
            if (progress == null)
            {
                return "progress is null";
            }

            progress.tid = 1;   // replace with real tid!

            try
            {
                var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = @"select count(stid) from progress where stid = @stid and taskid = @taskid and date = @today";
                cmd.Parameters.AddWithValue("@stid", progress.stid);
                cmd.Parameters.AddWithValue("@taskid", progress.taskid);
                cmd.Parameters.AddWithValue("@today", today);

                db.Open();
                var rowCount = int.Parse(cmd.ExecuteScalar().ToString());
                if (rowCount > 0)
                {
                    // already present: update
                    var ucmd = db.CreateCommand();
                    ucmd.CommandText = @"update progress set rating=@rating, scomment=@scomment, tcomment=@tcomment where stid=@stid and taskid=@taskid and date=@date";
                    ucmd.Parameters.AddWithValue("@stid", progress.stid);
                    ucmd.Parameters.AddWithValue("@taskid", progress.taskid);
                    ucmd.Parameters.AddWithValue("@date", today);
                    ucmd.Parameters.AddWithValue("@tid", progress.tid);
                    ucmd.Parameters.AddWithValue("@rating", progress.rating);
                    ucmd.Parameters.AddWithValue("@scomment", progress.scomment);
                    ucmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
                    ucmd.ExecuteNonQuery();
                    return string.Empty;
                }
                else
                {
                    // not present: create
                    var icmd = db.CreateCommand();
                    icmd.CommandText = @"insert into progress (stid, taskid, date, tid, rating, scomment, tcomment) values (@stid, @taskid, @date, @tid, @rating, @scomment, @tcomment)";
                    icmd.Parameters.AddWithValue("@stid", progress.stid);
                    icmd.Parameters.AddWithValue("@taskid", progress.taskid);
                    icmd.Parameters.AddWithValue("@date", today);
                    icmd.Parameters.AddWithValue("@tid", progress.tid);
                    icmd.Parameters.AddWithValue("@rating", progress.rating);
                    icmd.Parameters.AddWithValue("@scomment", progress.scomment);
                    icmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
                    icmd.ExecuteNonQuery();
                    return string.Empty;
                }

            }
            catch (Exception exo)
            {
                // LOG ERRORS in production!
                return exo.ToString().Substring(0, 255).Replace('\n', ' ').Replace('\r', ' ');
            }
        }
    }
}