using Example.Service;
using Example.WebApp.Models;
using Example.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Example.WebApp.Controllers
{
    public class PhoneStoreController : ApiController
    {
        PhoneStoreService service= new PhoneStoreService();
        private PhoneStoreRest PhoneStoreToRest(PhoneStore store)
        {
            PhoneStoreRest rest= new PhoneStoreRest();
            rest.Id = store.Id;
            rest.Name = store.Name;
            rest.Address = store.Address;
            return rest;
        }

        private PhoneStore RestToPhoneStore(PhoneStoreRest rest)
        {
            PhoneStore store = new PhoneStore();
            store.Id= rest.Id;
            store.Name = rest.Name;
            store.Address = rest.Address;
            return store;
        }

        private PhoneStore RestToPhoneStore(string name, string address)
        {
            PhoneStore store = new PhoneStore();
            store.Name = name;
            store.Address = address;
            return store;
        }

        public async Task<HttpResponseMessage> GetAsync()   
        {
            try
            {
                List<PhoneStore> phoneStores = new List<PhoneStore>();
                List<PhoneStoreRest> rests= new List<PhoneStoreRest>();
                phoneStores = await service.GetAsync();
                foreach(PhoneStore phoneStore in phoneStores)
                {
                    rests.Add(PhoneStoreToRest(phoneStore));
                }
                return Request.CreateResponse(HttpStatusCode.OK,rests);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        // GET api/phone/5
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            try
            {
                PhoneStoreRest rest = PhoneStoreToRest(await service.GetAsync(id));
                return Request.CreateResponse(HttpStatusCode.OK, rest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
                            
        }

        // POST api/phone
        public async Task<HttpResponseMessage> PostAsync([FromBody] PhoneStoreRest rest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                if (await service.PostAsync(RestToPhoneStore(rest)))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Added a phone");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message);
            }           
            
        }

        public async Task<HttpResponseMessage> PostAsync(string name, string address)
        {
            Guid id = Guid.NewGuid();
            try
            {
                if (await service.PostAsync(RestToPhoneStore(name, address)))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "A new phone added");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);                
            }
            
        }

        // PUT api/phone/5
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] PhoneStoreRest phoneStore)
        {            
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid input");
            }
            try
            {
                if (await service.PutAsync(id, RestToPhoneStore(phoneStore)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Store updated");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
            
        }

        // DELETE api/phone/5
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            try
            {
                if (await service.DeleteAsync(id))
                {
                    return Request.CreateResponse(HttpStatusCode.Gone, "Succesfully deleted.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}