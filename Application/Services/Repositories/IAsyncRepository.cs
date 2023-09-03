using Application.Services.Dynamic;
using Application.Services.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Repositories
{
    public interface IAsyncRepository<TEntity,TEntityId>:IQuery<TEntity> where TEntity : Entity<TEntityId>
    {
        Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate, //where koşulu yazabilmek için. 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include= null,  //Join desteği getirdim. Join yaparak da veri getirebiliriz. Tablo birleştirerek de. Join yapmak zorunda olmadığım için null geçtim.
            bool withDeleted = false, //Veritabanında silindi olarak işaretli verileri ister isem getirebileceğim yapı.
            bool enableTracking = true,
            CancellationToken cancellationToken = default
            );


        Task<Paginate<TEntity>> GetListAsync(
           Expression<Func<TEntity, bool>>? predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
           bool withDeleted = false,
           int index = 0,
           int size = 10,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
           );
        Task<Paginate<TEntity>> GetListByDynamicAsync(
           DynamicQuery dynamic,
           Expression<Func<TEntity, bool>>? predicate = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
           bool withDeleted = false,
           int index = 0,
           int size = 10,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
       );

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool enableTracking = true,
            bool withDeleted = false,
            CancellationToken cancellationToken = default
            );

        Task<TEntity> AddAsync(TEntity entity);

        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);

        Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false); //permenant ile soft delete mi kalıcı mı silinecek kararı veriliyor.

        Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false);
    }
}
