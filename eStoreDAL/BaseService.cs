/// <summary>
///     Author:     Evan Lauersen
///     Date:       Created: Feb 27, 2015
///     Purpose:    modified version of the original BaseService  Class from CypressNorth
///                  https://github.com/CypressNorth/.NET-EF6-GenericRepository
///                  http://www.itworld.com/article/2700950/development/a-generic-repository-for--net-entity-framework-6-with-async-operations.html
///                  A Generic Entity Framework 6 data repository for .NET 4.5 supporting just the async methods
///     Revisions:  none                needs the following us
using eStoreDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;   
/// </summary>
public class BaseService<TObject> where TObject : class
{
    protected AppDbContext _context;

    /// <summary>
    /// The contructor requires an open DataContext to work with
    /// </summary>
    /// <param name="context">An open DataContext</param>
    public BaseService(AppDbContext ctx)
    {
        _context = ctx;
    }

    /// <summary>
    /// Returns a single object with a primary key of the provided id
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <param name="id">The primary key of the object to fetch</param>
    /// <returns>A single object with the provided primary key or null</returns>
    public async Task<TObject> GetAsync(int id)
    {
        return await _context.Set<TObject>().FindAsync(id);
    }
    /// <summary>
    /// Gets a collection of all objects in the database
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <returns>An ICollection of every object in the database</returns>
    public async Task<ICollection<TObject>> GetAllAsync()
    {
        return await _context.Set<TObject>().ToListAsync();
    }

    /// <summary> 
    /// Gets a collection of all objects in the database 
    /// </summary> 
    /// <remarks>Synchronous</remarks> 
    /// <returns>An ICollection of every object in the database</returns> 
    public ICollection<TObject> GetAll()
    {
        return _context.Set<TObject>().ToList();
    }

    
    /// Returns a single object which matches the provided expression
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <param name="match">A Linq expression filter to find a single result</param>
    /// <returns>A single object which matches the expression filter. 
    /// If more than one object is found or if zero are found, null is returned</returns>
    public async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
    {
        return await _context.Set<TObject>().SingleOrDefaultAsync(match);
    }
    /// <summary>
    /// Returns a collection of objects which match the provided expression
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <param name="match">A linq expression filter to find one or more results</param>
    /// <returns>An ICollection of object which match the expression filter</returns>
    public async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
    {
        return await _context.Set<TObject>().Where(match).ToListAsync();
    }
    /// <summary>
    /// Inserts a single object to the database and commits the change
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <param name="t">The object to insert</param>
    /// <returns>The resulting object including its primary key after the insert</returns>
    public async Task<TObject> AddAsync(TObject t)
    {
        _context.Set<TObject>().Add(t);
        await _context.SaveChangesAsync();
        return t;
    }
    /// <summary>
    /// Inserts a collection of objects into the database and commits the changes
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <param name="tList">An IEnumerable list of objects to insert</param>
    /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
    public async Task<IEnumerable<TObject>> AddAllAsync(IEnumerable<TObject> tList)
    {
        _context.Set<TObject>().AddRange(tList);
        await _context.SaveChangesAsync();
        return tList;
    }
    /// <summary>
    /// Updates a single object based on the provided primary key and commits the change
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <param name="updated">The updated object to apply to the database</param>
    /// <param name="key">The primary key of the object to update</param>
    /// <returns>The resulting updated object</returns>
    public async Task<TObject> UpdateAsync(TObject updated, int key)
    {
        if (updated == null)
            return null;

        TObject existing = await _context.Set<TObject>().FindAsync(key);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();
        }
        return existing;
    }
    /// <summary>
    /// Deletes a single object from the database and commits the change
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <param name="t">The object to delete</param>
    public async Task<int> DeleteAsync(TObject t)
    {
        _context.Set<TObject>().Remove(t);
        return await _context.SaveChangesAsync();
    }
    /// <summary>
    /// Gets the count of the number of objects in the databse
    /// </summary>
    /// <remarks>Asynchronous</remarks>
    /// <returns>The count of the number of objects</returns>
    public async Task<int> CountAsync()
    {
        return await _context.Set<TObject>().CountAsync();
    }
}
