using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using Uber.Core;
using Uber.Data;
    
namespace Uber.API.Controllers
{
    public class ODataProductsController : ODataController
    {
        private UberContext data = new UberContext();

        // GET api/Products
        [Queryable]
        public IQueryable<Product> Get()
        {
            return data.Products;
        }
        
        // GET api/Products/5
        public Product Get([FromODataUri] int key)
        {
            Product product = data.Products.Find(key);

            if (product == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return product;
        }

        // PUT api/Products/5
        public HttpResponseMessage Put([FromODataUri] int key, Product product)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (key != product.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            data.Entry(product).State = EntityState.Modified;

            try
            {
                data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // POST api/Products
        public HttpResponseMessage Post(Product product)
        {
            if (ModelState.IsValid)
            {
                data.Products.Add(product);
                data.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, product);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = product.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Products/5
        public HttpResponseMessage Delete([FromODataUri] int key)
        {
            Product product = data.Products.Find(key);

            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            data.Products.Remove(product);

            try
            {
                data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            data.Dispose();
            base.Dispose(disposing);
        }
    }
}