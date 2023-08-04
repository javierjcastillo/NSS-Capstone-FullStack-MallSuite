using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MallSuite.Models;
using MallSuite.Utils;
using MallSuite.Repositories;

namespace MallSuite.Repositories
{
    public class StoreRestaurantRepository : BaseRepository, IStoreRestaurantRepository
    {
        public StoreRestaurantRepository(IConfiguration configuration) : base(configuration) { }

        public List<StoreRestaurant> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT sr.Id AS StoreRestaurantId, sr.Type, sr.Name, sr.Description, 
                       sr.CategoryId, sr.Location, sr.ContactInfo, sr.UserId,
                       c.Id AS CategoryId, c.Name AS CategoryName,
                       u.Id AS UserId, u.Name AS UserName
                FROM StoreRestaurant sr
                JOIN Category c ON c.Id = sr.CategoryId
                JOIN [User] u ON u.Id = sr.UserId   
                ORDER BY sr.Name
                ";

                    var reader = cmd.ExecuteReader();

                    var storeRestaurants = new List<StoreRestaurant>();
                    var storeRestaurantDictionary = new Dictionary<int, StoreRestaurant>();

                    while (reader.Read())
                    {
                        var storeRestaurant = new StoreRestaurant()
                        {
                            Id = DbUtils.GetInt(reader, "StoreRestaurantId"),
                            Type = DbUtils.GetString(reader, "Type"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Description = DbUtils.GetString(reader, "Description"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            Location = DbUtils.GetString(reader, "Location"),
                            ContactInfo = DbUtils.GetString(reader, "ContactInfo"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName"),
                            },
                            User = new User()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                Name = DbUtils.GetString(reader, "UserName"),
                            },
                            Tags = new List<Tag>()
                        };

                        storeRestaurants.Add(storeRestaurant);

                        // Add the storeRestaurant to the dictionary with the key as its Id
                        storeRestaurantDictionary.Add(storeRestaurant.Id, storeRestaurant);
                    }

                    reader.Close();

                    cmd.CommandText = @"
                SELECT srt.StoreRestaurantId, t.Id AS TagId, t.Name AS TagName
                FROM StoreRestaurantTag srt
                JOIN Tag t ON srt.TagId = t.Id
                ORDER BY t.Name";

                    var tagReader = cmd.ExecuteReader();

                    while (tagReader.Read())
                    {
                        // Get the StoreRestaurantId of the current Tag
                        var storeRestaurantId = DbUtils.GetInt(tagReader, "StoreRestaurantId");

                        // Check if the storeRestaurantDictionary contains the StoreRestaurantId as a key
                        if (storeRestaurantDictionary.ContainsKey(storeRestaurantId))
                        {
                            // If it does, add the current Tag to its Tags list
                            var storeRestaurant = storeRestaurantDictionary[storeRestaurantId];
                            storeRestaurant.Tags.Add(new Tag()
                            {
                                Id = DbUtils.GetInt(tagReader, "TagId"),
                                Name = DbUtils.GetString(tagReader, "TagName")
                            });
                        }
                    }

                    tagReader.Close();

                    return storeRestaurants;
                }
            }
        }
        public StoreRestaurant GetById(int id)
        {
            StoreRestaurant storeRestaurant = null;

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT sr.Id AS StoreRestaurantId, sr.Type, sr.Name, sr.Description, 
                       sr.CategoryId, sr.Location, sr.ContactInfo, sr.UserId,
                       c.Id AS CategoryId, c.Name AS CategoryName,
                       u.Id AS UserId, u.Name AS UserName
                FROM StoreRestaurant sr
                JOIN Category c ON c.Id = sr.CategoryId
                JOIN [User] u ON u.Id = sr.UserId
                WHERE sr.Id = @Id
                ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        storeRestaurant = new StoreRestaurant()
                        {
                            Id = DbUtils.GetInt(reader, "StoreRestaurantId"),
                            Type = DbUtils.GetString(reader, "Type"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Description = DbUtils.GetString(reader, "Description"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            Location = DbUtils.GetString(reader, "Location"),
                            ContactInfo = DbUtils.GetString(reader, "ContactInfo"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName"),
                            },
                            User = new User()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                Name = DbUtils.GetString(reader, "UserName"),
                            },
                            Tags = new List<Tag>()
                        };
                    }

                    reader.Close();
                }

                if (storeRestaurant != null)
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    SELECT t.Id AS TagId, t.Name AS TagName
                    FROM StoreRestaurantTag srt
                    JOIN Tag t ON t.Id = srt.TagId
                    WHERE srt.StoreRestaurantId = @Id
                    ";

                        DbUtils.AddParameter(cmd, "@Id", id);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            storeRestaurant.Tags.Add(new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "TagId"),
                                Name = DbUtils.GetString(reader, "TagName")
                            });
                        }

                        reader.Close();
                    }
                }
            }

            return storeRestaurant;
        }
  
        public void Add(StoreRestaurant storeRestaurant)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                INSERT INTO StoreRestaurant (Type, Name, Description, CategoryId, 
                                             Location, ContactInfo, UserId)
                OUTPUT INSERTED.ID
                VALUES (@Type, @Name, @Description, @CategoryId, 
                        @Location, @ContactInfo, @UserId)
                ";

                    DbUtils.AddParameter(cmd, "@Type", storeRestaurant.Type);
                    DbUtils.AddParameter(cmd, "@Name", storeRestaurant.Name);
                    DbUtils.AddParameter(cmd, "@Description", storeRestaurant.Description);
                    DbUtils.AddParameter(cmd, "@CategoryId", storeRestaurant.CategoryId);
                    DbUtils.AddParameter(cmd, "@Location", storeRestaurant.Location);
                    DbUtils.AddParameter(cmd, "@ContactInfo", storeRestaurant.ContactInfo);
                    DbUtils.AddParameter(cmd, "@UserId", storeRestaurant.UserId);

                    storeRestaurant.Id = (int)cmd.ExecuteScalar();


                    if (storeRestaurant.Tags != null)
                    {
                        // We add new tags
                        foreach (var tag in storeRestaurant.Tags)
                        {
                            cmd.CommandText = @"
                                INSERT INTO StoreRestaurantTag (StoreRestaurantId, TagId)
                                VALUES (@Id, @TagId)
                                ";

                            cmd.Parameters.Clear();
                            DbUtils.AddParameter(cmd, "@TagId", tag.Id);
                            DbUtils.AddParameter(cmd, "@Id", storeRestaurant.Id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public void Edit(StoreRestaurant storeRestaurant)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE StoreRestaurant
                        SET 
                            Type = @Type,
                            Name = @Name,
                            Description = @Description,
                            CategoryId = @CategoryId,
                            Location = @Location,
                            ContactInfo = @ContactInfo,
                            UserId = @UserId
                        WHERE Id = @Id
                        ";

                    DbUtils.AddParameter(cmd, "@Type", storeRestaurant.Type);
                    DbUtils.AddParameter(cmd, "@Name", storeRestaurant.Name);
                    DbUtils.AddParameter(cmd, "@Description", storeRestaurant.Description);
                    DbUtils.AddParameter(cmd, "@CategoryId", storeRestaurant.CategoryId);
                    DbUtils.AddParameter(cmd, "@Location", storeRestaurant.Location);
                    DbUtils.AddParameter(cmd, "@ContactInfo", storeRestaurant.ContactInfo);
                    DbUtils.AddParameter(cmd, "@UserId", storeRestaurant.UserId);
                    DbUtils.AddParameter(cmd, "@Id", storeRestaurant.Id);

                    cmd.ExecuteNonQuery();

                    // After updating the StoreRestaurant, we update its associated tags.
                    // First, we delete the old ones
                    cmd.CommandText = @"
                        DELETE FROM StoreRestaurantTag 
                        WHERE StoreRestaurantId = @Id
                        ";

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (storeRestaurant.Tags != null)
                    {
                        // Now we add new tags
                        foreach (var tag in storeRestaurant.Tags)
                        {
                            cmd.CommandText = @"
                                INSERT INTO StoreRestaurantTag (StoreRestaurantId, TagId)
                                VALUES (@Id, @TagId)
                                ";

                            cmd.Parameters.Clear();
                            DbUtils.AddParameter(cmd, "@TagId", tag.Id);
                            DbUtils.AddParameter(cmd, "@Id", storeRestaurant.Id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // Begin transaction
                    var transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;

                    try
                    {
                    // First, remove all associations between StoreRestaurant and Tag in the StoreRestaurantTag table.
                        cmd.CommandText = @"
                    DELETE FROM StoreRestaurantTag 
                    WHERE StoreRestaurantId = @Id
                ";
                        DbUtils.AddParameter(cmd, "@Id", id);

                        cmd.ExecuteNonQuery();

                    // Then, delete the StoreRestaurant itself
                        cmd.CommandText = @"
                    DELETE FROM StoreRestaurant 
                    WHERE Id = @Id
                ";

                        cmd.ExecuteNonQuery();

                    // Commit the transaction
                        transaction.Commit();
                    }
                    catch
                    {
                    // Rollback the transaction if any error occurred
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<StoreRestaurant> GetByCategoryId(int categoryId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT sr.Id AS StoreRestaurantId, sr.Type, sr.Name, sr.Description, 
                               sr.CategoryId, sr.Location, sr.ContactInfo, sr.UserId,
                               c.Id AS CategoryId, c.Name AS CategoryName,
                               u.Id AS UserId, u.Name AS UserName
                        FROM StoreRestaurant sr
                        JOIN Category c ON c.Id = sr.CategoryId
                        JOIN [User] u ON u.Id = sr.UserId
                        WHERE sr.CategoryId = @CategoryId
                        ORDER BY sr.Name
                        ";

                    DbUtils.AddParameter(cmd, "@CategoryId", categoryId);

                    var reader = cmd.ExecuteReader();

                    var storeRestaurants = new List<StoreRestaurant>();

                    while (reader.Read())
                    {
                        var storeRestaurant = new StoreRestaurant()
                        {
                            Id = DbUtils.GetInt(reader, "StoreRestaurantId"),
                            Type = DbUtils.GetString(reader, "Type"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Description = DbUtils.GetString(reader, "Description"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            Location = DbUtils.GetString(reader, "Location"),
                            ContactInfo = DbUtils.GetString(reader, "ContactInfo"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName"),
                            },
                            User = new User()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                Name = DbUtils.GetString(reader, "UserName"),
                            },
                            Tags = new List<Tag>()
                        };

                        storeRestaurants.Add(storeRestaurant);
                    }

                    reader.Close();

                    return storeRestaurants;
                }
            }
        }
        public List<StoreRestaurant> GetByTagId(int tagId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT sr.Id AS StoreRestaurantId, sr.Type, sr.Name, sr.Description, 
                               sr.CategoryId, sr.Location, sr.ContactInfo, sr.UserId,
                               c.Id AS CategoryId, c.Name AS CategoryName,
                               u.Id AS UserId, u.Name AS UserName,
                               t.Id as TagId, t.Name AS TagName
                        FROM StoreRestaurant sr
                        JOIN StoreRestaurantTag srt ON sr.Id = srt.StoreRestaurantId
                        JOIN Tag t ON srt.TagId = t.Id
                        JOIN Category c ON c.Id = sr.CategoryId
                        JOIN [User] u ON u.Id = sr.UserId
                        WHERE t.Id = @TagId
                        ";

                    DbUtils.AddParameter(cmd, "@TagId", tagId);

                    var reader = cmd.ExecuteReader();

                    var storeRestaurants = new List<StoreRestaurant>();

                    while (reader.Read())
                    {
                        storeRestaurants.Add(new StoreRestaurant()
                        {
                            Id = DbUtils.GetInt(reader, "StoreRestaurantId"),
                            Type = DbUtils.GetString(reader, "Type"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Description = DbUtils.GetString(reader, "Description"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            Location = DbUtils.GetString(reader, "Location"),
                            ContactInfo = DbUtils.GetString(reader, "ContactInfo"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            Category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName"),
                            },
                            User = new User()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                Name = DbUtils.GetString(reader, "UserName"),
                            },
                            Tags = new List<Tag>() // Add the associated tag
                    {
                        new Tag()
                        {
                            Id = DbUtils.GetInt(reader, "TagId"),
                            Name = DbUtils.GetString(reader, "TagName")
                        }
                    }
                        });
                    }

                    reader.Close();

                    return storeRestaurants;
                }
            }
        }
        public void AddCategory(int storeRestaurantId, int categoryId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE StoreRestaurant
                        SET CategoryId = @CategoryId
                        WHERE Id = @StoreRestaurantId
                        ";

                    DbUtils.AddParameter(cmd, "@CategoryId", categoryId);
                    DbUtils.AddParameter(cmd, "@StoreRestaurantId", storeRestaurantId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateCategory(int storeRestaurantId, int categoryId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE StoreRestaurant
                        SET CategoryId = @CategoryId
                        WHERE Id = @StoreRestaurantId
                        ";

                    DbUtils.AddParameter(cmd, "@CategoryId", categoryId);
                    DbUtils.AddParameter(cmd, "@StoreRestaurantId", storeRestaurantId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void AddTag(int storeRestaurantId, int tagId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO StoreRestaurantTag (StoreRestaurantId, TagId)
                        VALUES (@StoreRestaurantId, @TagId)
                        ";

                    DbUtils.AddParameter(cmd, "@StoreRestaurantId", storeRestaurantId);
                    DbUtils.AddParameter(cmd, "@TagId", tagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void RemoveTag(int storeRestaurantId, int tagId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                DELETE FROM StoreRestaurantTag 
                WHERE StoreRestaurantId = @StoreRestaurantId AND TagId = @TagId
                ";
                    DbUtils.AddParameter(cmd, "@StoreRestaurantId", storeRestaurantId);
                    DbUtils.AddParameter(cmd, "@TagId", tagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
