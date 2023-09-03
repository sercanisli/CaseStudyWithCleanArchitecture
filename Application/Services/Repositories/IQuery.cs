using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface IQuery<T>
    { //LINQ değil SQL query'si geçmek ister isek kullanılacak.
        IQueryable<T> Query();
    }
}
