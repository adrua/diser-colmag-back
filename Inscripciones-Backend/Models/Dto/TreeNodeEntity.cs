using System.Collections.Generic;

namespace Inscripciones_Backend.Security.Models
{
    public class TreeNodeEntity 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string descripcion { get; set; }
        public bool check { get; set; }
        public IEnumerable<TreeNodeEntity> children { get; set; }
    }    
}
