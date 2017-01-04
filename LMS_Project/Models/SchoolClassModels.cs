using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS_Project.Models
{
    public class SchoolClassModels
    {
        [Key]
        public string SchoolClassID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }

        //[ForeignKey("SchemaModels")]
        public string SchemaID { get; set; }

    }

    
}