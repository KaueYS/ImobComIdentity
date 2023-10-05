using ImobComIdentity.Models;
using System.ComponentModel;

namespace ImobComIdentity.ViewModel
{
    public class ImovelPermutaViewModel
    {
        [DisplayName("Comprador")]
        public ClienteImovel ClienteInteressado { get; set; }


        public string Imovel { get; set; }


        public ClienteImovel Proprietario { get; set; }
    }
}
