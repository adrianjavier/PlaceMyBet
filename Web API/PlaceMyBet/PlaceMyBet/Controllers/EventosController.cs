using PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet.Controllers
{
    public class EventosController : ApiController
    {
        
        public IEnumerable<Evento> Get()
        {
            EventosRepository rep = new EventosRepository();
            List<Evento> lista = rep.Retrieve();
            return lista;
        }

        public IEnumerable<EventoDTO> GetDTO()
        {
            EventosRepository rep = new EventosRepository();
            List<EventoDTO> lista = rep.RetrieveDTO();
            return lista;
        }



        [Route("api/ApuestasExamen")]
        public EventoDTO2 Get(string equipo)
        {
            EventosRepository rep = new EventosRepository();
            EventoDTO2 e = rep.RetrieveDTO2(equipo);
            return e;
        }

        // POST: api/Eventos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Eventos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Eventos/5
        public void Delete(int id)
        {
        }
    }
}
