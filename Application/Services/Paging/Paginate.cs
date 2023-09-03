using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Paging
{
    public class Paginate<T>
    {
        public int Size { get; set; } //sayfada kaç data gelecek.
        public int Index { get; set; } //hangi sayfadayız.
        public int Count { get; set; } //toplam veri sayısı.
        public int Pages { get; set; } //toplam kaç sayfamız var.

        public IList<T> Items { get; set; } //datamız.
        public bool HasPrevious => Index > 0; //önceki sayfa var mı?
        public bool HasNext => Index+1 < Pages; //sonraki sayfa var mı?

        public Paginate()
        {
            Items = Array.Empty<T>();
        }
        
    }
}
