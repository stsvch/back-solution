using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Entities
{
    public class Development : ContentBase
    {
        // Для EF Core
        private Development() { }

        public Development(string title, string description)
            : base(title, description)
        {
        }
    }
}
