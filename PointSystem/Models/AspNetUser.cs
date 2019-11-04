using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Models
{
    public class AspNetUser : IdentityUser
    {
        public ICollection<Proposal> Proposals { get; set; }
        public ICollection<RegistrationFeast> RegistrationFeasts { get; set; }
    }
}
