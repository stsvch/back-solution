using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Entities
{
    public class News : ContentBase
    {
        // Для EF Core
        private News() { }

        public News(string title, string description)
            : base(title, description)
        {
        }
    }
}
