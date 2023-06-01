using Example.Model;
using Example.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Example.Repository
{
    public class PhoneStoreRepository : IPhoneStoreRepository
    {
        private readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=PhoneStore;";
        public bool Delete(Guid id)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"DELETE FROM PhoneStore WHERE Id=@id;");
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<PhoneStore> Get()
        {
            List<PhoneStore> phoneStores = new List<PhoneStore>();
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    connection.Open();
                    string query = "SELECT * FROM PhoneStore";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PhoneStore phoneStore = new PhoneStore();
                                phoneStore.Id = (Guid)reader["Id"];
                                phoneStore.Name = (string)reader["Name"];
                                phoneStore.Address = (string)reader["Address"];
                                phoneStores.Add(phoneStore);
                            }
                        }
                    }
                    connection.Close();


                    return phoneStores;
                }
            }
            catch (Exception)
            {
                return phoneStores;
            }
        }

        public PhoneStore Get(Guid id)
        {
            PhoneStore phoneStore = new PhoneStore();
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    connection.Open();
                    string query = "SELECT * FROM PhoneStore WHERE id=@Id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                        }
                        else
                        {
                            return phoneStore;
                        }
                        phoneStore.Id = (Guid)reader["Id"];
                        phoneStore.Name = (string)reader["Name"];
                        phoneStore.Address = (string)reader["Address"];

                    }
                    connection.Close();
                    return phoneStore;
                }
            }
            catch (Exception)
            {
                return phoneStore;
            }

        }

        public bool Post(PhoneStore phoneStore)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                Guid id = Guid.NewGuid();
                connection.Open();
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"INSERT INTO PhoneStore (Id, Name, Address) VALUES (@Id, @Name, @Address)");
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", phoneStore.Name);
                    command.Parameters.AddWithValue("@Address", phoneStore.Address);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Post(string name, string address)
        {
            Guid id = Guid.NewGuid();
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"INSERT INTO PhoneStore (Id, Name, Address) VALUES (@Id, @Name, @Address)");
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Put(Guid id, PhoneStore phoneStore)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    NpgsqlCommand command = new NpgsqlCommand();
                    stringBuilder.Append("UPDATE PhoneStore set ");
                    if (phoneStore.Name != null)
                    {
                        stringBuilder.Append($"Name=@name,");
                        command.Parameters.AddWithValue("@name", phoneStore.Name);
                    }
                    if (phoneStore.Address != null)
                    {
                        stringBuilder.Append($"Address=@address,");
                        command.Parameters.AddWithValue("@address", phoneStore.Address);
                    }

                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    }

                    stringBuilder.Append($" WHERE Id=@id;");
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = stringBuilder.ToString();

                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
