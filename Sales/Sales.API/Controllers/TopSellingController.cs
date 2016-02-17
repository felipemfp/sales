using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sales.API.Models;

namespace Sales.API.Controllers
{
    public class TopSellingController : ApiController
    {
        private SalesEntities db = new SalesEntities();

        // GET: api/TopSelling
        public IEnumerable<spSelectTopSellingProducts_Result> GetTopSellingProducts([FromUri]int length)
        {
            return db.spSelectTopSellingProducts(length).ToList();
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
