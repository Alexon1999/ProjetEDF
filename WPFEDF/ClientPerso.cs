using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEDF
{
    public class ClientPerso
    {
        public int Id { get; set; }
        public string NomClient { get; set; }

        public string PrenomClient { get; set; }
        public int AncienReleve { get; set; }
        public int DerneirReleve { get; set; }
        public int Consommation { get; set; }
    }
}
