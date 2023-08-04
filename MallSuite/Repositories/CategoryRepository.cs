using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MallSuite.Models;
using MallSuite.Utils;
using MallSuite.Repositories;

namespace MallSuite.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Category> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.Name
                        FROM Category c
                        ORDER BY Name
                        ";

                    var reader = cmd.ExecuteReader();

                    var categories = new List<Category>();

                    while (reader.Read())
                    {
                        categories.Add(new Category()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        });
                    }
                    reader.Close();
                    return categories;
                }
            }
        }

        public Category GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.Name
                        FROM Category c
                        WHERE c.Id = @Id
                        ";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Category category = null;

                    if (reader.Read())
                    {
                        category = new Category()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        };
                    }
                    reader.Close();
                    return category;
                }
            }
        }

        public void Add(Category category)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Category (Name)
                        OUTPUT INSERTED.ID
                        VALUES (@Name)
                        ";

                    DbUtils.AddParameter(cmd, "@Name", category.Name);

                    category.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int categoryId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Category
                        WHERE Id = @Id
                        ";

                    DbUtils.AddParameter(cmd, "@Id", categoryId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Edit(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Category
                        SET Name = @Name
                        WHERE Id = @Id
                        ";
                    DbUtils.AddParameter(cmd, "@Name", category.Name);
                    DbUtils.AddParameter(cmd, "@Id", category.Id);

                    cmd.ExecuteNonQuery();
                }
            }    
        }
    }
}
