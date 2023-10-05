using ImobComIdentity.Data;
using ImobComIdentity.Models;
using ImobComIdentity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ImobComIdentity.Controllers
{
    public class ProcurarController : Controller
    {
       
        private readonly ApplicationDbContext _context;

        public ProcurarController(ApplicationDbContext context)
        {
           
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            List<ImovelPermutaViewModel> imoveisEncontrados = new List<ImovelPermutaViewModel>();
            List<ClienteImovel> imoveis = _context.CLienteImoveis.ToList();

            foreach (ClienteImovel venda in imoveis)
            {
                foreach (ClienteImovel troca in imoveis)
                {
                    if (venda.Imovel == troca.Permuta)
                    {
                        ImovelPermutaViewModel imovelPermutaModel = new ImovelPermutaViewModel();
                        imovelPermutaModel.ClienteInteressado = imoveis.FirstOrDefault(c => c.Cliente == troca.Cliente);
                        imovelPermutaModel.Imovel = venda.Imovel;
                        imovelPermutaModel.Proprietario = imoveis.FirstOrDefault(c => c.Cliente == venda.Cliente);

                        imoveisEncontrados.Add(imovelPermutaModel);
                    }
                }
            }
            return View(imoveisEncontrados);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var clienteImovel = await _context.CLienteImoveis.FirstOrDefaultAsync(m => m.Id == id);
            if (clienteImovel == null)
            {
                return NotFound();
            }

            return View(clienteImovel);
        }

    }
}
