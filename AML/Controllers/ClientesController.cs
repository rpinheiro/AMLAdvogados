using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AML.Models.Cliente;
using Microsoft.AspNetCore.Authorization;

namespace AML.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ClienteContext _context;

        public ClientesController(ClienteContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.Include(c => c.Email)
                .Include(c => c.Telefone)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CPF,RG, Endereco, Complemento, Bairro, CEP, Cidade, Email, Telefone")] Cliente cliente)
        {
            if (cliente.Email != null)
                cliente.Email.ToList().ForEach(c => { c.Cliente.RG = cliente.RG; c.Cliente.Nome = cliente.Nome; c.Cliente.CPF = cliente.CPF; });

            if (cliente.Telefone != null)
                cliente.Telefone.ToList().ForEach(c => { c.Cliente.RG = cliente.RG; c.Cliente.Nome = cliente.Nome; c.Cliente.CPF = cliente.CPF; });

            ModelState.Clear();
            TryValidateModel(cliente);

            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                                        .Include(c => c.Email)
                                        .Include(c => c.Telefone)
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,RG, Endereco, Complemento, Bairro, CEP, Cidade, Email, Telefone")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (cliente.Email != null)
                cliente.Email.ToList().ForEach(c => { c.Cliente.RG = cliente.RG; c.Cliente.Nome = cliente.Nome; c.Cliente.CPF = cliente.CPF; });

            if (cliente.Telefone != null)
                cliente.Telefone.ToList().ForEach(c => { c.Cliente.RG = cliente.RG; c.Cliente.Nome = cliente.Nome; c.Cliente.CPF = cliente.CPF; });

            ModelState.Clear();
            TryValidateModel(cliente);

            if (ModelState.IsValid)
            {

                Cliente clienteBd = _context.Cliente.Include(c => c.Email)
                                                    .Include(c => c.Telefone)
                                            .Where(c => c.Id == id).SingleOrDefault();
                if (clienteBd != null)
                {
                    if (cliente.Email != null && cliente.Email.Count > 0)
                    {
                        var listaEmailsExcluidos = clienteBd.Email.ToList().Where(c => !cliente.Email.Any(d => d.Id == c.Id)).ToList();
                        listaEmailsExcluidos.ToList().ForEach(e => clienteBd.Email.Remove(e));

                        var listaEmailsNovos = cliente.Email.Where(e => e.Id == 0).ToList();
                        listaEmailsNovos.ToList().ForEach(e => clienteBd.Email.Add(e));


                        foreach (var email in clienteBd.Email.ToList())
                        {
                            var clienteCadastro = cliente.Email.FirstOrDefault(e => e.Id == email.Id);
                            if (clienteCadastro != null)
                            {
                                email.EnderecoEmail = clienteCadastro.EnderecoEmail;
                            }
                        }
                    }
                    else
                    {
                        if (cliente.Email == null)
                        {
                            if (clienteBd.Email != null )
                                clienteBd.Email.Clear();
                        }
                    }

                    if (cliente.Telefone != null && cliente.Telefone.Count > 0)
                    {
                        var listaTelefonesExcluidos = clienteBd.Telefone.ToList().Where(c => !cliente.Telefone.Any(d => d.Id == c.Id)).ToList();
                        listaTelefonesExcluidos.ToList().ForEach(e => clienteBd.Telefone.Remove(e));

                        var listaTelefonesNovos = cliente.Telefone.Where(e => e.Id == 0).ToList();
                        listaTelefonesNovos.ToList().ForEach(t => clienteBd.Telefone.Add(t));

                        foreach (var telefone in clienteBd.Telefone.ToList())
                        {
                            var clienteCadastro = cliente.Telefone.FirstOrDefault(e => e.Id == telefone.Id);
                            if (clienteCadastro != null)
                            {
                                telefone.Numero = clienteCadastro.Numero;
                                telefone.Tipo = clienteCadastro.Tipo;
                            }
                        }
                    }
                    else
                    {
                        if (cliente.Telefone == null)
                        {
                            if (clienteBd.Telefone != null)
                                clienteBd.Telefone.Clear();
                        }
                    }

                    clienteBd.Nome = cliente.Nome;
                    clienteBd.RG = cliente.RG;
                    clienteBd.CPF = cliente.CPF;
                }


                try
                {
                    _context.Update(clienteBd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente
                 .Include(c => c.Email)
                 .Include(c => c.Telefone)
                .SingleOrDefaultAsync(m => m.Id == id);

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
