using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sales.API.Models;

namespace Sales.API.Controllers
{
    public class VipsController : ApiController
    {
        private SalesEntities db = new SalesEntities();

        // GET: api/Vips
        public IEnumerable<vClientVIP> GetVips()
        {
            return db.vClientVIPs.ToList();
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
