using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MallSuite.Models;
using MallSuite.Utils;
using MallSuite.Repositories;

namespace MallSuite.Repositories
{
    public class StoreRestaurantTagRepository : BaseRepository, IStoreRestaurantTagRepository
    {
        public StoreRestaurantTagRepository(IConfiguration configuration) : base(configuration) { }

        public List<StoreRestaurantTag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT srt.Id AS StoreRestaurantTagId, srt.StoreRestaurantId, srt.TagId, 
                               sr.Name AS StoreRestaurantName, t.Name AS TagName
                        FROM StoreRestaurantTag srt
                        JOIN StoreRestaurant sr ON sr.Id = srt.StoreRestaurantId
                        JOIN Tag t ON t.Id = srt.TagId
                        ORDER BY srt.Id
                        ";

                    var reader = cmd.ExecuteReader();

                    var storeRestaurantTags = new List<StoreRestaurantTag>();

                    while (reader.Read())
                    {
                        storeRestaurantTags.Add(new StoreRestaurantTag()
                        {
                            Id = DbUtils.GetInt(reader, "StoreRestaurantTagId"),
                            StoreRestaurantId = DbUtils.GetInt(reader, "StoreRestaurantId"),
                            TagId = DbUtils.GetInt(reader, "TagId"),
                            StoreRestaurant = new StoreRestaurant()
                            {
                                Id = DbUtils.GetInt(reader, "StoreRestaurantId"),
                                Name = DbUtils.GetString(reader, "StoreRestaurantName"),
                            },
                            Tag = new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "TagId"),
                                Name = DbUtils.GetString(reader, "TagName"),
                            }
                        });
                    }
                    reader.Close();
                    return storeRestaurantTags;
                }
            }
        }
    }
}
