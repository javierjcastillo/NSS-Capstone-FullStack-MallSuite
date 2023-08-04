using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MallSuite.Models;
using MallSuite.Utils;
using MallSuite.Repositories;

namespace MallSuite.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration configuration) : base(configuration) { }

        public List<Tag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT t.Id, t.Name
                        FROM Tag t
                        ORDER BY Name
                        ";

                    var reader = cmd.ExecuteReader();

                    var tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(new Tag()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        });
                    }
                    reader.Close();
                    return tags;
                }
            }
        }

        public Tag GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT t.Id, t.Name
                        FROM Tag t
                        WHERE t.Id = @Id
                        ";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Tag tag = null;

                    if (reader.Read())
                    {
                        tag = new Tag()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        };
                    }
                    reader.Close();
                    return tag;
                }
            }
        }

        public void Add(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Tag (Name)
                        OUTPUT INSERTED.ID
                        VALUES (@Name)
                        ";
                    
                    DbUtils.AddParameter(cmd, "@Name", tag.Name);

                    tag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int tagId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Tag
                        WHERE Id = @Id
                        ";
                    DbUtils.AddParameter(cmd, "@Id", tagId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Edit(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Tag
                        SET Name = @Name
                        WHERE Id = @Id
                        ";

                    DbUtils.AddParameter(cmd, "@Name", tag.Name);
                    DbUtils.AddParameter(cmd, "@Id", tag.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //public void AddTagToStoreRestaurant(int storeRestaurantId, int tagId)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        INSERT INTO StoreRestaurantTag (StoreRestaurantId, TagId)
        //        VALUES (@StoreRestaurantId, @TagId)";

        //            DbUtils.AddParameter(cmd, "@StoreRestaurantId", storeRestaurantId);
        //            DbUtils.AddParameter(cmd, "@TagId", tagId);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        //public void RemoveTagFromStoreRestaurant(int storeRestaurantId, int tagId)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        DELETE FROM StoreRestaurantTag 
        //        WHERE StoreRestaurantId = @StoreRestaurantId AND TagId = @TagId";

        //            DbUtils.AddParameter(cmd, "@StoreRestaurantId", storeRestaurantId);
        //            DbUtils.AddParameter(cmd, "@TagId", tagId);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
