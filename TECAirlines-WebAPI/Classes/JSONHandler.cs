﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TECAirlines_WebAPI.Classes
{
    public class JSONHandler
    {
        public static string BuildFlightSearchResult(string fl_id, string fl_date, int price, int fc_price)
        {
            JObject search_result = new JObject();
            search_result["flight_id"] = fl_id;
            search_result["depart_date"] = fl_date;
            search_result["price"] = price;
            search_result["fc_price"] = fc_price;
            return search_result.ToString();
        }

        public static string BuildUserFlightResult(string flight_id, string depart, string arrival)
        {
            JObject search_result = new JObject();
            search_result["flight_id"] = flight_id;
            search_result["depart_ap"] = depart;
            search_result["arrival_ap"] = arrival;
            return search_result.ToString();
        }

        public static string BuildListStrResult(string attribute, List<string> results)
        {
            JArray array = new JArray();
            for(int i = 0; i < results.Count; i++)
            {
                array.Add(results.ElementAt(i));
            }
            JObject result = new JObject();
            result["http_result"] = 1;
            result[attribute] = array;

            return result.ToString();
        }

        public static string BuildMsgJSON(int result, string msg)
        {
            JObject search_result = new JObject();
            search_result["msg"] = msg;
            search_result["http_result"] = result;
            return search_result.ToString();
        }

        public static string BuildFlightDetails(string dep_ap, string arr_ap, int price, int fc_price)
        {
            JObject search_result = new JObject();
            search_result["depart_ap"] = dep_ap;
            search_result["arrival_ap"] = arr_ap;
            search_result["normal_price"] = price;
            search_result["fc_price"] = fc_price;
            search_result["http_result"] = 1;
            return search_result.ToString();
        }

        public static string BuildPair(string key1, string val1, string key2, string val2)
        {
            JObject pair_json = new JObject();
            pair_json[key1] = val1;
            pair_json[key2] = val2;
            pair_json["http_result"] = 1;

            return pair_json.ToString();
        }

        public static string BuildCost(int cost)
        {
            JObject cost_json = new JObject();
            cost_json["cost"] = cost;
            cost_json["http_result"] = 1;
            return cost_json.ToString();
        }

        public static string BuildPeopleFlying(int ppl)
        {
            JObject cost_json = new JObject();
            cost_json["people"] = ppl;
            cost_json["http_result"] = 1;
            return cost_json.ToString();
        }

        public static List<string> JArrayToList(string jarray)
        {
            JArray arr = JArray.Parse(jarray);
            return arr.ToObject<List<string>>();
        }

        public static string FormatAsString(Object obj)
        {
            return String.Format("{0}", obj);
        }

        public static int FormatAsInt(Object obj)
        {
            return Convert.ToInt32(obj);
        }
    }
}