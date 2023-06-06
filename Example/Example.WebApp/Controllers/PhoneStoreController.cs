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
using Microsoft.Graph;
using Example.Common;
using System.Web.UI.WebControls;
using Example.Service.Common;

namespace Example.WebApp.Controllers
{
    public class PhoneStoreController : ApiController
    {
        private IPhoneStoreService Service { get; }
        public PhoneStoreController(IPhoneStoreService service)
        {
            Service = service;
        }
        RestConverter converter = new RestConverter();

        public async Task<HttpResponseMessage> GetAsync(int pageSize= 10, int pageNumber=1,string sortBy = "name",string sortOrder = "asc", string filterString = null, string filterAddress = null, char? firstLetter= null)   
        {
            try
            {
                PagedList<PhoneStore> phoneStores = new PagedList<PhoneStore>();
                PagedList<PhoneStoreRest> rests= new PagedList<PhoneStoreRest>();
                
                Paging paging = new Paging(pageSize,pageNumber);
                Sorting sorting = new Sorting(sortBy, sortOrder);
                Filtering filtering = new Filtering(filterString,firstLetter, filterAddress);

                phoneStores = await Service.GetAsync(paging, sorting, filtering);
                
                foreach(PhoneStore phoneStore in phoneStores)
                {
                    rests.Add(converter.PhoneStoreToRest(phoneStore));
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
                PhoneStoreRest rest = converter.PhoneStoreToRest(await Service.GetAsync(id));
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
                if (await Service.PostAsync(converter.RestToPhoneStore(rest)))
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
            try
            {
                if (await Service.PostAsync(converter.RestToPhoneStore(name, address)))
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
                if (await Service.PutAsync(id, converter.RestToPhoneStore(phoneStore)))
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
                if (await Service.DeleteAsync(id))
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


    public class RestConverter
    {
        public PhoneStoreRest PhoneStoreToRest(PhoneStore store)
        {
            PhoneStoreRest rest = new PhoneStoreRest();
            rest.Id = store.Id;
            rest.Name = store.Name;
            rest.Address = store.Address;
            return rest;
        }

        public PhoneStore RestToPhoneStore(PhoneStoreRest rest)
        {
            PhoneStore store = new PhoneStore();
            store.Id = rest.Id;
            store.Name = rest.Name;
            store.Address = rest.Address;
            return store;
        }

        public PhoneStore RestToPhoneStore(string name, string address)
        {
            PhoneStore store = new PhoneStore();
            store.Name = name;
            store.Address = address;
            return store;
        }
    }
}