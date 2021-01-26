using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using PlaceMyBet.Models;

namespace PlaceMyBet.Controllers
{
    [Route("api/Apuestas/{action}")]
    public class ApuestasController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Apuesta> Get()
        {
            ApuestasRepository rep = new ApuestasRepository();
            List<Apuesta> lista = rep.Retrieve();
            return lista;
        }
        [HttpGet]
        [ActionName("GetDTO")]
        public IEnumerable<ApuestaDTO> GetDTO()
        {
            ApuestasRepository rep = new ApuestasRepository();
            List<ApuestaDTO> lista = rep.RetrieveDTO();
            return lista;
        }

        [Route("api/Apuestas")]
        public int Get(string mail, double cuota)
        {
            ApuestasRepository rep = new ApuestasRepository();
            int cantidad = rep.RetrieveByCuota(mail, cuota);
            return cantidad;
        }

        [Route("api/Apuestas")]
        public List<Object> Get(int id)
        {
            ApuestasRepository rep = new ApuestasRepository();
            List<Object> lista = rep.RetrieveById(id);
            return lista;
        }

        public void Post([FromBody]Apuesta a)
        {
            var repo = new ApuestasRepository();
            repo.Save(a);
        }

        // PUT: api/Apuestas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Apuestas/5
        public void Delete(int id)
        {
        }
    }
}
