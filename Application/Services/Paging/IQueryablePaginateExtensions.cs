using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Paging
{
    public static class IQueryablePaginateExtensions
    {
        public static async Task<Paginate<T>> ToPaginateAsync<T>(
            this IQueryable<T> source,
            int index,
            int size,
            CancellationToken cancellationToken = default
            //her sayfada ne kadar veri olacağına karar verilen alan.
            )
        {
            int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            List<T> items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);
            //bulunduğum sayfa ile sayfamda gelen veri miktarını çarparak sayfa atlama işlemini gerçekleştirdim. Ve ardından gelen eriyi aldım.

            Paginate<T> list = new()
            {
                Index = index,
                Count = count,
                Items = items,
                Size = size,
                Pages = (int)Math.Ceiling(count / (double)size), //toplam sayfa sayım.
            };
            return list;
        }
    }
}
