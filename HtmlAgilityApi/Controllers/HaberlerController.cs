using HtmlAgilityApi.Data.Interfaces;
using HtmlAgilityApi.Data.Models;
using HtmlAgilityApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HtmlAgilityApi.Controllers
{
    public class HaberlerController : ApiController
    {
        //private IHaberRepository haberler = new HaberRepository();
        private IHaberRepository haberler;
        public HaberlerController(IHaberRepository _haberler)
        {
            this.haberler = _haberler;
        }
         //GET api/haberler
        public IEnumerable<Haber> Get()
        {
            return haberler.GetTumHaberler();
        }

        // GET api/Haberler/5
        public IHttpActionResult Get(int id)
        {
            var haber = haberler.GetHaber(id);
            if (haber == null)
            {
                return NotFound();
            }
            return Ok(haber);
        }
    }
}
