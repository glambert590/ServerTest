using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Servidor.Models
{
    public class Museum
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public int Id_Museum { get; set; }       
        [Required]         
        public string Museum_Name { get; set; }
        
        public string Theme { get; set; }
        public string City { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Article> Articles { get; set; }
    }
}