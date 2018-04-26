using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimalSanctuary.Models
{
    [Table("Vetrinarians")]
    public class Vetrinarian
    {
        [Key]
        public int VetrinarianId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }


       public override bool Equals(System.Object otherVetrinarian)
        {
            if (!(otherVetrinarian is Vetrinarian))
            {
                return false;
            }
            else
            {
                Vetrinarian newVetrinarian = (Vetrinarian)otherVetrinarian;
                return this.VetrinarianId.Equals(newVetrinarian.VetrinarianId);
            }
        }

        public override int GetHashCode()
        {
            return this.VetrinarianId.GetHashCode();
        }

    }
}
