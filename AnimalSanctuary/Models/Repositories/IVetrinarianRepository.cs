using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalSanctuary.Models
{
    interface IVetrinarianRepository
    {
        IQueryable<Vetrinarian> Vetrinarians { get; }
        Vetrinarian Save ( Vetrinarian vetrinarian );
        Vetrinarian Edit ( Vetrinarian vetrinarian );
        void Remove ( Vetrinarian vetrinarian );
    }
}
