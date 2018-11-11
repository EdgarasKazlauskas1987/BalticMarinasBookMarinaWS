﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalticMarinasBookMarinaWS.Models
{
    public class MarinaContext
    {
        public string ConnectionString { get; set; }

        public MarinaContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Marina> GetAllMarinas()
        {
            List<Marina> list = new List<Marina>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select marina.MarinaId, marina.MarinaName, marina.Phone, marina.Email, marina.Depth, city.CityName, city.Country, zipcode.ZipCodeNumber, marina.TotalBerths, marina.IsToilet, marina.IsShower, marina.IsInternet\n" +
                    "from marina\n" +
                    "JOIN cityzipcode ON marina.CityZipCodeId=cityzipcode.CityZipCodeId\n" +
                    "JOIN city ON city.CityId=cityzipcode.CityId\n" +
                    "JOIN zipcode ON zipcode.ZipCodeId=cityzipcode.ZipCodeId;", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Marina()
                        {
                            MarinaId = Convert.ToInt32(reader["MarinaId"]),
                            MarinaName = reader["MarinaName"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Depth = Convert.ToDouble(reader["Depth"]),
                            CityName = reader["CityName"].ToString(),
                            Country = reader["Country"].ToString(),
                            ZipCodeNumber = reader["ZipCodeNumber"].ToString(),
                            TotalBerths = Convert.ToInt32(reader["TotalBerths"]),
                            IsToilet = Convert.ToInt32(reader["IsToilet"]),
                            IsShower = Convert.ToInt32(reader["IsShower"]),
                            IsInternet = Convert.ToInt32(reader["IsInternet"])
                        });
                    }
                }
            }
            return list;
        }
    }
}