using ClassroomBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;

namespace ClassroomBackend
{
    public class SqlHelper
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        MySqlConnection db = new MySqlConnection(connectionString);

        public List<Student> FindStudent(uint org, string namepart)
        {
            var students = new List<Student>();
            var cmd = db.CreateCommand();
            cmd.CommandText = "select stid, lname, fname, email, " +
                "target, parent, teacher, note, username, password, " +
                "male, trial, liturgy, torah, haftara from student " +
                "where org = @org and " +
                "lname like @namepart";
            cmd.Parameters.AddWithValue("@org", org);
            cmd.Parameters.AddWithValue("@namepart", namepart + "%");

            db.Open();
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Student
                        {
                            stid = SafeUInt(reader, "stid"),                            
                            fname = SafeString(reader, "fname"),
                            lname = SafeString(reader, "lname"),
                            email = SafeString(reader, "email"),
                            target = reader.GetDateTime("target"),
                            parent = SafeUInt(reader, "parent"),
                            teacher = SafeUInt(reader, "teacher"),
                            note = SafeString(reader, "note"),
                            username = SafeString(reader, "username"),
                            password = SafeString(reader, "password"),
                            male = reader.GetBoolean("male"),
                            trial = reader.GetBoolean("trial"),
                            group = SafeInt(reader, "liturgy"),
                            torah = SafeString(reader, "torah"),
                            haftara = SafeString(reader, "haftara")
                        };
                        students.Add(p);
                    }
                }
            }
            catch (Exception e)
            {
                // log exception
                return null;
            }
            return students;
        }


        public uint UpdateParent(Parent parent)
        {
            var cmd = db.CreateCommand();
            try
            {
                db.Open();
                if (parent.pid > 0)
                {
                    // update
                    cmd.CommandText = "update parent set org = @org, title1 = @title1, " +
                        "lname1 = @lname1, fname1 = @fname1, email1 = @email1, " +
                        "title2 = @title2, lname2 = @lname2, fname2 = @fname2, email2 = @email2, " +
                        "address1 = @address1, address2 = @address2, city = @city, " +
                        "state = @state, zip = @zip, comment = @comment " +
                        "where pid = @pid;select ROW_COUNT()";
                    cmd.Parameters.AddWithValue("@pid", parent.pid);
                }
                else
                {
                    // insert
                    cmd.CommandText = "insert into parent(org, title1, lname1, fname1, email1, " +
                       "title2, lname2, fname2, email2, address1, address2, city, state, zip, comment) " +
                        "values (@org, @title1, @lname1, @fname1, @email1, @title2, @lname2, @fname2, " +
                        "@email2, @address1, @address2, @city, @state, @zip, @comment)" +
                        "; select last_insert_id()";
                }
                cmd.Parameters.AddWithValue("@org", parent.org);
                cmd.Parameters.AddWithValue("@title1", parent.title1);
                cmd.Parameters.AddWithValue("@lname1", parent.lname1);
                cmd.Parameters.AddWithValue("@fname1", parent.fname1);
                cmd.Parameters.AddWithValue("@email1", parent.email1);
                cmd.Parameters.AddWithValue("@title2", parent.title2);
                cmd.Parameters.AddWithValue("@lname2", parent.lname2);
                cmd.Parameters.AddWithValue("@fname2", parent.fname2);
                cmd.Parameters.AddWithValue("@email2", parent.email2);
                cmd.Parameters.AddWithValue("@address1", parent.address1);
                cmd.Parameters.AddWithValue("@address2", parent.address2);
                cmd.Parameters.AddWithValue("@city", parent.city);
                cmd.Parameters.AddWithValue("@state", parent.state);
                cmd.Parameters.AddWithValue("@zip", parent.zip);
                cmd.Parameters.AddWithValue("@comment", parent.comment);

                var result = cmd.ExecuteScalar();
                if (parent.pid == 0) return uint.Parse(result.ToString());
                return parent.pid;
            } catch (Exception e)
            {
                return 0;
            }
        }

        public List<Parent> FindParent(uint org, string namepart)
        {
            var parents = new List<Parent>();
            var cmd = db.CreateCommand();
            cmd.CommandText = "select pid, title1, lname1, fname1, email1, " +
                "title2, lname2, fname2, email2, address1, address2, " +
                "city, state, zip, comment from parent " +
                "where org = @org and " +
                "lname1 like @namepart or lname2 like @namepart";
            cmd.Parameters.AddWithValue("@org", org);
            cmd.Parameters.AddWithValue("@namepart", namepart + "%");

            db.Open();
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Parent
                        {
                            pid = SafeUInt(reader, "pid"),
                            title1 = SafeString(reader, "title1"),
                            fname1 = SafeString(reader, "fname1"),
                            lname1 = SafeString(reader, "lname1"),
                            email1 = SafeString(reader, "email1"),
                            title2 = SafeString(reader, "title2"),
                            fname2 = SafeString(reader, "fname2"),
                            lname2 = SafeString(reader, "lname2"),
                            email2 = SafeString(reader, "email2"),
                            address1 = SafeString(reader, "address1"),
                            address2 = SafeString(reader, "address2"),
                            city = SafeString(reader, "city"),
                            state = SafeString(reader, "state"),
                            zip = SafeString(reader, "zip"),
                            comment = SafeString(reader, "comment")
                        };
                        parents.Add(p);
                    }
                }
            }
            catch (Exception e)
            {
                // log exception
                return null;
            }
            return parents;
        }

        public User Authorize(string orgid, string username, string password)
        {
            MySqlCommand cmd = db.CreateCommand();
            cmd.CommandText = ("select tid, org, fname, lname, teacher.email, " +
                "teacher.username, teacher.pwhash from teacher  " +
                "join org on org.id = teacher.org " +
                "where org.orgid = @orgid and username = @username and pwhash = @pwhash");
            try
            {
                cmd.Parameters.AddWithValue("@orgid", orgid);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@pwhash", password); // TODO HASH Password first

                db.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = new User
                        {
                            tid = SafeUInt(reader, "tid"),
                            org = SafeUInt(reader, "org"),
                            fname = SafeString(reader, "fname"),
                            lname = SafeString(reader, "lname"),
                            username = SafeString(reader, "username"),
                            email = SafeString(reader, "email")
                        };
                        return user;
                    }
                }
            }
            catch
            {
                // TODO log exception
            }
            return null;
        }

        public List<Student> StudentList(uint org, uint id = 0) // possibly take dates or months as input parameter
        {
            // string constr = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            var slist = new List<Student>();
            MySqlCommand cmd = db.CreateCommand();
            MySqlCommand acmd = db.CreateCommand();
            // add upper limit:

            if (id == 0)
            {
                cmd.CommandText = "select stid, target, lname, fname, liturgy, torah, haftara " +
                    "from student where target > CURDATE() and org = @org";
                cmd.Parameters.AddWithValue("@org", org);
            }
            else
            {
                cmd.CommandText = "select stid, target, lname, fname, liturgy, torah, haftara " +
                    "from student where stid = @stid";
                cmd.Parameters.AddWithValue("@stid", id);
            }

            //acmd.CommandText = "select stid, checkin, status from attendance  where checkin > CURDATE() and order by stid, checkin";
            acmd.CommandText = "select student.stid, checkin, status from attendance " +
                "join student on student.stid = attendance.stid " +
                "where checkin > CURDATE() and org = @org order by stid, checkin";
            acmd.Parameters.AddWithValue("@org", org);
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
                if (id == 0)
                {
                    using (MySqlDataReader areader = acmd.ExecuteReader())
                    {
                        while (areader.Read())
                        {
                            var kid = slist.Find(s => s.stid == SafeUInt(areader, "stid"));
                            kid.present = areader.GetBoolean("status");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // log exception here.
                return null;
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
    }
}