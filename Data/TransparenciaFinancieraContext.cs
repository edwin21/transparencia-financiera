#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransparenciaFinanciera.Models;

    public class TransparenciaFinancieraContext : DbContext
    {
        public TransparenciaFinancieraContext (DbContextOptions<TransparenciaFinancieraContext> options)
            : base(options)
        {
        }

        public DbSet<TransparenciaFinanciera.Models.Egreso> Egreso { get; set; }
    }
