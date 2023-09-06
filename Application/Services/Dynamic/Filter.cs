using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Dynamic
{
    public class Filter
    {
        public string Field { get; set; } //hangi alan üzerinde çalışma olacak. Ör Title.
        public string? Value { get; set; } //bu alanın değeri ne olacak.
        public string Operator { get; set; } //IQueryableDynamicFilterExtension sınıfından hangi Operator kullanılacak.
        public string? Logic { get; set; } //And - Or Logic

        public IEnumerable<Filter>? Filters { get; set; } //Birden fazla filtre olma durumunda. 

        public Filter()
        {
            Field = string.Empty;
            Operator=string.Empty;
        }

        public Filter(string field, string @operator)
        {
            Field = field;
            Operator= @operator;
        }
    }
}
