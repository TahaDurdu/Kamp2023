using System;
using System.Linq.Expressions;
using Entities.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    //generic Constraint
    // class = referans
    // IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir.
    // new() : soyut bir nesne olamaz.sadece somut nesne alabilir.

	public interface IEntityRepository<T> where T:class,IEntity,new()
	{
        List<T> GetAll(Expression<Func<T ,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

