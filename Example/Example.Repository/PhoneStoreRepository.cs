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
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"DELETE FROM PhoneStore WHERE Id=@id;");
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PhoneStore>> GetAsync()
        {
            List<PhoneStore> phoneStores = new List<PhoneStore>();
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM PhoneStore";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                PhoneStore phoneStore = new PhoneStore();
                                phoneStore.Id = (Guid)reader["Id"];
                                phoneStore.Name = (string)reader["Name"];
                                phoneStore.Address = (string)reader["Address"];
                                phoneStores.Add(phoneStore);
                            }
                        }
                    }
                    await connection.CloseAsync();


                    return phoneStores;
                }
            }
            catch (Exception)
            {
                return phoneStores;
            }
        }

        public async Task<PhoneStore> GetAsync(Guid id)
        {
            PhoneStore phoneStore = new PhoneStore();
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM PhoneStore WHERE id=@Id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                        }
                        else
                        {
                            return phoneStore;
                        }
                        phoneStore.Id = (Guid)reader["Id"];
                        phoneStore.Name = (string)reader["Name"];
                        phoneStore.Address = (string)reader["Address"];

                    }
                    await connection.CloseAsync();
                    return phoneStore;
                }
            }
            catch (Exception)
            {
                return phoneStore;
            }

        }

        public async Task<bool> PostAsync(PhoneStore phoneStore)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                Guid id = Guid.NewGuid();
                await connection.OpenAsync();
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"INSERT INTO PhoneStore (Id, Name, Address) VALUES (@Id, @Name, @Address)");
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", phoneStore.Name);
                    command.Parameters.AddWithValue("@Address", phoneStore.Address);
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PostAsync(string name, string address)
        {
            Guid id = Guid.NewGuid();
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"INSERT INTO PhoneStore (Id, Name, Address) VALUES (@Id, @Name, @Address)");
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PutAsync(Guid id, PhoneStore phoneStore)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    await connection.OpenAsync();
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
                    command.Connection = connection;
                    command.CommandText = stringBuilder.ToString();

                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
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
