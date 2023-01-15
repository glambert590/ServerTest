using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor{

    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        :base(options)
        {


        }
        public ApplicationDBContext(){
            
        }
        
        public DbSet<Article> Article{ get; set;}
        public DbSet<Museum> Museum{ get; set;}
    }

}