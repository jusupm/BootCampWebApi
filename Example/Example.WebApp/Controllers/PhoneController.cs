using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Example.WebApp.Controllers
{
    public class PhoneController : ApiController
    {
        private static List<Phone> phones = new List<Phone>();

        public IEnumerable<Phone> Get()
        {
            return phones;
        }

        // GET api/phone/5
        public HttpResponseMessage Get(int id)
        {
            if (phones!=null)
            {
                Phone phone = phones.FirstOrDefault(p => p.Id == id);
                if(phone!=null)
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, phone);
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.NotFound,"Cannot get a phone by that id.");
        }

        // POST api/phone
        public HttpResponseMessage Post([FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Invalid input");
            }
                if (phones.Count != 0)
                {
                    phone.Id = phones.Max(p => p.Id) + 1;
                }
                else
                {
                    phone.Id = default;
                }
            phones.Add(phone);
            return Request.CreateResponse(HttpStatusCode.Created,"A new phone added");
        }

        public HttpResponseMessage Post(int id, string model, decimal price)
        {
            if (phones.Any(p => p.Id == id))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "A phone already exists with that id.");
            }
            Phone phone = new Phone { Id = id, Model = model, Price = price };
            phones.Add(phone);
            return Request.CreateResponse(HttpStatusCode.Created, "A new phone added");
        }

        // PUT api/phone/5
        public HttpResponseMessage Put(int id, [FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Invalid input");
            }
            Phone existingPhone = phones.FirstOrDefault(p => p.Id == id);
            if (existingPhone != null)
            {
                existingPhone.Model = phone.Model;
                existingPhone.Price = phone.Price;
                return Request.CreateResponse(HttpStatusCode.Accepted, "Phone updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "A phone does not exist.");
            }
        }

        // DELETE api/phone/5
        public HttpResponseMessage Delete(int id)
        {
            Phone phone = phones.FirstOrDefault(p => p.Id == id);
            if (phone == null)
                return Request.CreateResponse(HttpStatusCode.NoContent, "A phone does not exist.");
            phones.Remove(phone);
            return Request.CreateResponse(HttpStatusCode.Gone,"Succesfully deleted.");
        }
    }
}