using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Servidor.Models
{
    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id_Article { get; set; }

        [Required]
        public string Article_Name { get; set; }

        [DefaultValue(false)]
        public bool isDamaged { get; set; }

        [ForeignKey("Museum")]
        public int Id_RefMuseum { get; set; }        

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Museum Museum { get; set; }
    }
}