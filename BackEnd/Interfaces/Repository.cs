using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.Utils;

namespace BackEnd.Interfaces
{
  public interface IProductRepository : IGenericRepository<Product>
  {
    Task <Product> GetById(int id);
  }

  public interface ICategoryRepository : IGenericRepository<Category>
  {
    Task <Category> GetById(int id);
  }

  public interface IRatingRepository : IGenericRepository<Rating> {}

  public interface IImageRepository : IGenericRepository<Image> {}

  public interface IGenericRepository<T> where T : class
  {
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, int page = ConstantVariable.DEFAULT_VALUE_NUMBER_TYPE, int size = ConstantVariable.DEFAULT_VALUE_NUMBER_TYPE, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includes = "");
    Task<T> GetBy(Expression<Func<T, bool>> predicate, string includes = "");
    Task<bool> Add(T entity);
    Task<bool> AddRange(IEnumerable<T> entites);
    bool Delete(T entity);
    bool DeleteRange(IEnumerable<T> entities);
    Task<bool> Update(T entity);
    Task<int> CountAll(Expression<Func<T, bool>> filter = null);
  }

  public interface IUnitOfWork
  {
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }
    IRatingRepository Ratings { get; }
    IImageRepository Images { get; }
    Task SaveChangeAsync();
  }
}