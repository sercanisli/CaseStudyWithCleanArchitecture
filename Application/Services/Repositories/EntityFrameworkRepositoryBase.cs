using Application.Services.Dynamic;
using Application.Services.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Services.Repositories
{
    public class EntityFrameworkRepositoryBase<TEntity,TEntityId,TContext>:IAsyncRepository<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TContext:DbContext
    {
        protected readonly TContext _context;

        public EntityFrameworkRepositoryBase(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate=DateTime.Now;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
            }
            await _context.AddAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking(); //false gelmişse izleme.
            }
            if (withDeleted)
            {
                queryable = queryable.IgnoreQueryFilters();
            }
            if (predicate != null)
            {
                queryable = queryable.Where(predicate); //bir filtreleme yapılmışsa
            }
            return await queryable.AnyAsync(cancellationToken);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entity, permanent); //nesnenin silineceğine mi yoksa DeletedDate'in güncelleneceğine mi karar vereceğimiz method. (Soft Delete)
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entities, permanent);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking();
            }
            if (include != null)
            {
                queryable = include(queryable);
            }
            if (withDeleted)
            {
                queryable = queryable.IgnoreQueryFilters();
            }
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking();
            }
            if (include != null) //join işlemi var mı
            {
                queryable = include(queryable);
            }
            if (withDeleted)
            {
                queryable = queryable.IgnoreQueryFilters();
            }
            if (predicate != null) //where sorgumuz var mı?
            {
                queryable = queryable.Where(predicate);
            }
            if (orderBy != null)
            {
                return await orderBy(queryable).ToPaginateAsync(index, size, cancellationToken);
            }
            return await queryable.ToPaginateAsync(index, size, cancellationToken);
        }

        public async Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking();
            }
            if (include != null)
            {
                queryable = include(queryable);
            }
            if (withDeleted)
            {
                queryable = queryable.IgnoreQueryFilters();
            }
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.ToPaginateAsync(index, size, cancellationToken);
        }

        public IQueryable<TEntity> Query() => _context.Set<TEntity>(); //ilgili domain nesnemize attach oluruz. Ekleriz.

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdatedDate = DateTime.Now;
            }
            _context.Update(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        protected async Task SetEntityAsDeletedAsync(TEntity entity, bool permanent)
        {
            if(permanent)
            {
                CheckHasEntityHaveOneToOneRelation(entity); //entity'nin 1-1 ilişkisi var mı var ise ilişkilerinin de soft delete olması gerekir.
                await SetEntityAsSoftDeletedAsync(entity);
            }
            else
            {
                _context.Remove(entity);
            }
        }

        protected void CheckHasEntityHaveOneToOneRelation(TEntity entity)
        {
            bool hasEntityHaveOneToOneRelation = _context.Entry(entity).Metadata.GetForeignKeys().All(
                //ilgili entity içerisindeki metadaları kullanarak bana onların Foreignkeylerini al. Her bir foreignkey için
               x => x.DependentToPrincipal?.IsCollection == true //DeclaringEntityType'larına bak 1-1 ilişki var mı yok mu bak. 
               || x.PrincipalToDependent?.IsCollection == true //Birebir ilişkilerde 2 tarafta koleksiyon olmaz. Buna bakarız. Bi taraf nesne diğer taraf koleksiyon gibi.
               || x.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == entity.GetType()
               ) == false; //içlerinde koleksiyon yok ise 1-1 Dir. 

            if (hasEntityHaveOneToOneRelation==true)
            {
                throw new InvalidOperationException("Entity has one-to-one relationship. Soft Delete causes problems if you try to create entry again by same foreign key.");
            }
        }

        private async Task SetEntityAsSoftDeletedAsync(IEntityTimeStamps entity)
        {
            if (entity.DeletedDate.HasValue) //DeletedDate'de bir değer var mı var ise zaten silinmiş olarak işaretlenmiştir.
            {
                return;
            }
            entity.DeletedDate = DateTime.Now;

            var navigations = _context .Entry(entity).Metadata.GetNavigations()
                .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
                .ToList();
            foreach (INavigation? navigation in navigations)
            {
                if (navigation.TargetEntityType.IsOwned())
                {
                    continue;
                }
                if (navigation.PropertyInfo == null)
                {
                    continue;
                }

                object? navValue = navigation.PropertyInfo.GetValue(entity);
                if (navigation.IsCollection)
                {
                    if (navValue == null)
                    {
                        IQueryable query = _context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).ToListAsync();
                        if (navValue == null)
                            continue;
                    }

                    foreach (IEntityTimeStamps navValueItem in navValue as IEnumerable)
                        await SetEntityAsSoftDeletedAsync(navValueItem);
                }
                else
                {
                    if (navValue == null)
                    {
                        IQueryable query = _context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType())
                            .FirstOrDefaultAsync();
                        if (navValue == null)
                            continue;
                    }
                    await SetEntityAsSoftDeletedAsync((IEntityTimeStamps)navValue);
                }
            }

            _context.Update(entity);
        }

        protected IQueryable<object> GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
        {
            Type queryProviderType = query.Provider.GetType();
            MethodInfo createQueryMethod = queryProviderType.GetMethods()
                    .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                    ?.MakeGenericMethod(navigationPropertyType)
                ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");
            var queryProviderQuery = (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: new object[] { query.Expression })!;
            return queryProviderQuery.Where(x => !((IEntityTimeStamps)x).DeletedDate.HasValue);
        }

        protected async Task SetEntityAsDeletedAsync(IEnumerable<TEntity> entities, bool permanent)
        {
            foreach (TEntity entity in entities)
            {
                await SetEntityAsDeletedAsync(entity, permanent);
            }
        }
    }
}
