using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Uber.Core;
using Uber.Data;

namespace Uber.API.Controllers
{
    public class ProductTypesController : ApiController
    {
        private UberContext data = new UberContext();

        // GET api/ProductTypes
        public IEnumerable<ProductType> GetProductTypes()
        {
            return data.ProductTypes.AsEnumerable();
        }

        // GET api/ProductTypes/5
        public ProductType GetProductType(int id)
        {
            ProductType producttype = data.ProductTypes.Find(id);
            if (producttype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return producttype;
        }

        // PUT api/ProductTypes/5
        public HttpResponseMessage PutProductType(int id, ProductType producttype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != producttype.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            data.Entry(producttype).State = EntityState.Modified;

            try
            {
                data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/ProductTypes
        public HttpResponseMessage PostProductType(ProductType producttype)
        {
            if (ModelState.IsValid)
            {
                data.ProductTypes.Add(producttype);
                data.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, producttype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = producttype.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ProductTypes/5
        public HttpResponseMessage DeleteProductType(int id)
        {
            ProductType producttype = data.ProductTypes.Find(id);
            if (producttype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            data.ProductTypes.Remove(producttype);

            try
            {
                data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, producttype);
        }

        protected override void Dispose(bool disposing)
        {
            data.Dispose();
            base.Dispose(disposing);
        }
    }
}