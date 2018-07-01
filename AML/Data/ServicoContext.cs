using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AML.Models.Cliente;

public class ServicoContext : DbContext
{
    public ServicoContext(DbContextOptions<ServicoContext> options)
        : base(options)
    {
    }

    public DbSet<AML.Models.Servico.Servico> Servico { get; set; }
    public DbSet<AML.Models.Servico.ServicoCliente> ServicoCliente { get; set; }
    public DbSet<AML.Models.Servico.Pagamento> Pagamento { get; set; }

}
