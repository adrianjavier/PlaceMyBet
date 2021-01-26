using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class Evento
    {
        public int idEvento { get; set; }
        public string local { get; set; }
        public string visitante { get; set; }
        public string fecha { get; set; }

        public Evento(int idEvento, string local, string visitante, string fecha)
        {
            this.idEvento = idEvento;
            this.local = local;
            this.visitante = visitante;
            this.fecha = fecha;
        }
    }

    public class EventoDTO
    {
        public string local { get; set; }
        public string visitante { get; set; }
        public string fecha { get; set; }

        public EventoDTO(string local, string visitante, string fecha)
        {
            this.local = local;
            this.visitante = visitante;
            this.fecha = fecha;
        }
    }

    public class EventoDTO2
    {
        public string rival { get; set; }
        public string cantidad { get; set; }

        public EventoDTO2(string r, string c)
        {
            this.rival = r;
            this.cantidad = c;
        }
    }
}