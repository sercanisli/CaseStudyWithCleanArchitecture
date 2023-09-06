using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Dynamic
{
    public class Sort
    {
        public string Field { get; set; } //hangi alanı sıralayacağım.
        public string Direction { get; set; } //Asc mi Desc mi?

        public Sort()
        {
            Field=string.Empty;
            Direction=string.Empty;
        }
        public Sort(string field, string direction)
        {
            Field = field; 
            Direction=direction;
        }
    }
}
