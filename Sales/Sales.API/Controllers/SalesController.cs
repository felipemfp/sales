﻿using System;
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
        public IEnumerable<spSelectSaleByClient_Result> GetSales([FromUri]int client)
        {
            return db.spSelectSaleByClient(client).ToList();
        }

        // GET: api/Sales?startDate=2015-02-01&endDate=2016-02-01
        public IEnumerable<spSelectSaleByDate_Result> GetSales([FromUri]DateTime startDate, [FromUri]DateTime endDate)
        {
            return db.spSelectSaleByDate(startDate, endDate).ToList();
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
        public IHttpActionResult PutSale(int id, List<SaleProduct> saleProducts, [FromUri]int client, [FromUri]DateTime date)
        {
            if (client <= 0)
            {
                return BadRequest();
            }

            Sale sale = db.Sales.Find(id);

            if (sale == null)
            {
                return NotFound();
            }

            foreach (SaleProduct saleProduct in sale.SaleProducts)
            {
                db.SaleProducts.Remove(saleProduct);
            }

            sale.SaleProducts.Clear();

            foreach (SaleProduct saleProduct in saleProducts)
            {
                db.spInsertSaleProduct(sale.Id, saleProduct.ProductId, saleProduct.Quantity);
            }

            db.Entry(sale).State = EntityState.Modified;

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
        public IHttpActionResult PostSale(List<SaleProduct> saleProducts, [FromUri]int client, [FromUri]DateTime date)
        {
            if (client <= 0)
            {
                return BadRequest();
            }

            ObjectParameter saleId = new ObjectParameter("SaleId", typeof(int));
            db.spInsertSale(client, date, saleId);

            foreach (SaleProduct saleProduct in saleProducts)
            {
                db.spInsertSaleProduct((int)saleId.Value, saleProduct.ProductId, saleProduct.Quantity);
            }

            db.SaveChanges();

            Sale sale = db.Sales.Find((int)saleId.Value);
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

            foreach (SaleProduct saleProduct in sale.SaleProducts)
            {
                db.SaleProducts.Remove(saleProduct);
            }

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
