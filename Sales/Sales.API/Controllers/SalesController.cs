using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sales.API.Models;

namespace Sales.API.Controllers
{
    public class SalesController : ApiController
    {
        private SalesEntities db = new SalesEntities();

        // GET: api/Sales
        public IEnumerable<Sale> GetSales()
        {
            return db.Sales.ToList();
        }

        // GET: api/Sales?client=5
        public IEnumerable<Sale> GetSales([FromUri]int client)
        {
            List<spSelectSaleByClient_Result> result = db.spSelectSaleByClient(client).ToList();
            return result.Select(s => db.Sales.Find(s.Id));
        }

        // GET: api/Sales?startDate=2015-02-01&endDate=2016-02-01
        public IEnumerable<Sale> GetSales([FromUri]DateTime startDate, [FromUri]DateTime endDate)
        {
            List<spSelectSaleByDate_Result> result = db.spSelectSaleByDate(startDate, endDate).ToList();
            return result.Select(s => db.Sales.Find(s.Id));
        }

        // GET: api/Sales/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetSale(int id)
        {
            Sale sale = db.Sales.Find(id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        // PUT: api/Sales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSale(int id, Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sale.Id)
            {
                return BadRequest();
            }

            Sale s = db.Sales.Find(id);

            if (s == null)
            {
                return NotFound();
            }

            s.SaleProducts.Clear();

            db.SaveChanges();

            foreach (SaleProduct saleProduct in sale.SaleProducts)
            {
                db.spInsertSaleProduct(s.Id, saleProduct.ProductId, saleProduct.Quantity);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sales
        [ResponseType(typeof(Sale))]
        public IHttpActionResult PostSale(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ObjectParameter saleId = new ObjectParameter("SaleId", typeof(int));
            db.spInsertSale(sale.ClientId, sale.DateSale, saleId);

            foreach (SaleProduct saleProduct in sale.SaleProducts)
            {
                db.spInsertSaleProduct((int)saleId.Value, saleProduct.ProductId, saleProduct.Quantity);
            }

            db.SaveChanges();

            sale = db.Sales.Find((int)saleId.Value);
            return CreatedAtRoute("DefaultApi", new { id = sale.Id }, sale);
        }

        // DELETE: api/Sales/5
        [ResponseType(typeof(Sale))]
        public IHttpActionResult DeleteSale(int id)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            sale.SaleProducts.Clear();
            db.SaveChanges();

            db.Sales.Remove(sale);
            db.SaveChanges();

            return Ok(sale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
