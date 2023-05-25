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
            var phone = phones.FirstOrDefault(p => p.Id == id);
            if (phones.Count == 0)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound,phone);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK,phone);
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
                phone.Id = 1;
            }
            phones.Add(phone);
            return Request.CreateResponse(HttpStatusCode.Created,"A new phone added");
        }

        // PUT api/phone/5
        public HttpResponseMessage Put(int id, [FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Invalid input");
            }
            Phone existingPhone = phones.FirstOrDefault(p => p.Id == id);
            existingPhone.Model = phone.Model;
            existingPhone.Price = phone.Price;
            return Request.CreateResponse(HttpStatusCode.Accepted,"Phone updated");
        }

        // DELETE api/phone/5
        public HttpResponseMessage Delete(int id)
        {
            if (id == null)
                return Request.CreateResponse(HttpStatusCode.NoContent, "A phone does not exist.");
            Phone phone = phones.FirstOrDefault(p => p.Id == id);

            phones.Remove(phone);
            return Request.CreateResponse(HttpStatusCode.Gone,"Succesfully deleted.");
        }
    }
}