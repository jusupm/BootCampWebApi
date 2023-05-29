using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml.Linq;


namespace Example.WebApp.Controllers
{
    public class PhoneStoreController : ApiController
    {
        
        public List<PhoneStore> Get()
        {
            List<PhoneStore> phoneStores = new List<PhoneStore>();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=PhoneStore;");
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

        // GET api/phone/5
        public HttpResponseMessage Get(Guid id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=PhoneStore;");
            try
            {
                connection.Open();
                string query = "SELECT * FROM PhoneStore WHERE id=@Id";
                PhoneStore phoneStore = new PhoneStore();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) { reader.Read(); }
                    else { return Request.CreateResponse(HttpStatusCode.NotFound, "Store does not exist."); }
                    phoneStore.Id = (Guid)reader["Id"];
                    phoneStore.Name = (string)reader["Name"];
                    phoneStore.Address = (string)reader["Address"];
                    
                }
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, phoneStore);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
                            
        }

        // POST api/phone
        public HttpResponseMessage Post([FromBody] PhoneStore phoneStore)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=PhoneStore;");

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                Guid id = Guid.NewGuid();
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = ($"INSERT INTO PhoneStore (Id, Name, Address) VALUES (@Id, @Name, @Address)");
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", phoneStore.Name);
                command.Parameters.AddWithValue("@Address", phoneStore.Address);
                command.ExecuteNonQuery();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.Created, phoneStore);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message);
            }           
            
        }

        public HttpResponseMessage Post(string name, string address)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=PhoneStore;");

            Guid id = Guid.NewGuid();
            try
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
                return Request.CreateResponse(HttpStatusCode.Created, "A new phone added");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);                
            }
            
        }

        // PUT api/phone/5
        public HttpResponseMessage Put(Guid id, [FromBody] PhoneStore phoneStore)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=PhoneStore;");

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                NpgsqlCommand command = new NpgsqlCommand();
                stringBuilder.Append("UPDATE PhoneStore set ");
                if (phoneStore.Name != null)
                {
                    stringBuilder.Append($"Name=@name, ");
                    command.Parameters.AddWithValue("@name", phoneStore.Name);
                }
                if (phoneStore.Address != null)
                {
                    stringBuilder.Append($"Address=@address ");
                    command.Parameters.AddWithValue("address", phoneStore.Address);
                }
                stringBuilder.Append($"WHERE Id=@id;");
                command.Parameters.AddWithValue("id", phoneStore.Id);
                connection.Open();
                command.Connection = connection;
                command.CommandText = stringBuilder.ToString();

                command.ExecuteNonQuery();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Store updated");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
            
        }

        // DELETE api/phone/5
        public HttpResponseMessage Delete(Guid id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=PhoneStore;");
            try
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = ($"DELETE FROM PhoneStore WHERE Id=@id';");
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.Gone, "Succesfully deleted.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message);
            }
            
        }
    }
}