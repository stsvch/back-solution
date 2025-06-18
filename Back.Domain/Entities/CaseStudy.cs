using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Entities
{
    public class CaseStudy : ContentBase
    {
        // Для EF Core
        private CaseStudy() { }

        public CaseStudy(string title, string description)
            : base(title, description)
        {
        }
    }
}
