using System;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_EF_Core_Code_First.Models
{
    public class BloggingContext : DbContext

    {

        public BloggingContext(DbContextOptions<BloggingContext> options)

        : base(options)

        { }

        public DbSet<Universite> Universiteler { get; set; }

        public DbSet<Bolum> Bolumler { get; set; }

    }

    public class Universite

    {

        public int UniversiteId { get; set; }

        public string UniversiteAdi { get; set; }

        public List<Bolum> Bolumler { get; set; }

    }

    public class Bolum

    {

        public int BolumId { get; set; }

        public string BolumAdi { get; set; }

        public string Aciklama { get; set; }

        public int UniversiteId { get; set; }

        public Universite Universite { get; set; }

    }
}
