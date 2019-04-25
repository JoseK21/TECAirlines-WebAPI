﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace TECAirlines_WebAPI.Classes
{
    public class SQLHelper
    {
        public static bool UsernameExists(string username, string table, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select username from " + table + " where username = @user";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("user", username));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public static bool AirplaneExists(string model, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select model from AIRPLANES where model = @model";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("model", model));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public static bool AirportExists(string ap, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select ap_name from AIRPORT where ap_name = @name";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("name", ap));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public static Tuple<int, int, int> GetPlaneDetails(string model, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select plane_id, capacity, fc_capacity from AIRPLANES where model = @model";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("model", model));

            Tuple<int, int, int> result = null;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new Tuple<int, int, int>(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                    }
                    connection.Close();
                    return result;
                }
            }
            return result;
        }

        public static string CheckFlightState(string flight_id, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select status from FLIGHT where flight_id = @id";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("id", flight_id));

            string status = "";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        status = reader.GetString(0);
                    }
                }
            }

            connection.Close();

            return status;
        }

        public static int GetSeatsLeft(string seat_type, string flight_id, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select " + seat_type + " from FLIGHT where flight_id = @id";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("id", flight_id));

            int result = -1;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
                    }
                }
            }
            connection.Close();
            return result;
        }

        public static bool IsFlightFull(string flight_id, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select seats_left, fc_seats_left from FLIGHT where flight_id = @id";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("id", flight_id));

            bool result = false;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if ((int)reader[0] == 0 && (int)reader[1] == 0) result = true;
                        else result = false;
                    }
                }
            }
            connection.Close();
            return result;
        }

        public static float CheckFlightDiscount(string flight, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select exp_date, discount from SALE where flight_id = @id";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("id", flight));

            DateTime today = DateTime.Today;

            int result = 0;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateTime sale_exp = reader.GetDateTime(0);
                        if (today <= sale_exp)
                        {
                            result = reader.GetInt32(1);
                        }
                    }
                }
            }
            connection.Close();
            return result;
        }

        public static bool UniExists(string uni, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select * from UNIVERSITY where uni_name = @name";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("name", uni));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public static int GetStudentMiles(string username, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "select st_miles from STUDENTS where username = @name";

            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("name", username));

            int result = 0;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                    }
                }
            }
            connection.Close();
            return result;
        }

        public static void AddStudentMiles(string username, int miles, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();
            string req = "update STUDENTS set st_miles = @miles where username = @user";

            SqlCommand cmd = new SqlCommand(req, connection);

            int curr_miles = GetStudentMiles(username, connect_str);
            int new_miles = curr_miles + miles;

            cmd.Parameters.Add(new SqlParameter("miles", new_miles));
            cmd.Parameters.Add(new SqlParameter("user", username));

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public static List<string> GetUserFlightID(string user, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();

            string req = "select flight_id from RESERVATION where username = @user";
            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("user", user));

            List<string> fl_ids = new List<string>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fl_ids.Add(reader.GetString(0));
                    }
                }
            }
            connection.Close();
            return fl_ids;
        }

        public static string GetAirportFlightData(string flight_id, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();

            string req = "select depart_ap, arrival_ap from FLIGHT where flight_id = @fl";
            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("fl", flight_id));

            string data = "";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data = JSONHandler.BuildUserFlightResult(flight_id, reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            connection.Close();
            return data;
        }

        public static int GetPreCheckId(string username, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();

            string req = "select id_prechecking from PRE_CHECKING where username = @user";
            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("user", username));

            int id = 0;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }
            connection.Close();
            return id;
        }

        public static void AddCustomerSeat(int precheck, string seat, string connect_str)
        {
            SqlConnection connection = new SqlConnection(connect_str);
            connection.Open();

            string req = "insert into PRE_CHECKING_SEATS VALUES(@id, @seat)";
            SqlCommand cmd = new SqlCommand(req, connection);

            cmd.Parameters.Add(new SqlParameter("id", precheck));
            cmd.Parameters.Add(new SqlParameter("seat", seat));

            cmd.ExecuteNonQuery();

            connection.Close();
        }

    }
}