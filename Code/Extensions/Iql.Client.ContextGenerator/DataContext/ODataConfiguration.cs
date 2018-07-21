//using System.Threading.Tasks;
//using Iql.Queryable.Data.Crud.Operations.Results;
//using Iql.Queryable.Data.Validation;

//namespace Iql.OData.TypeScript.Generator.DataContext
//{
//    public interface IHttpProvider
//    {
//        Task<GetDataResult<TResult>> PostOnEntityInstance<TEntity, TResult>(object payload) where TResult : class;
//        Task<GetDataResult<TResult>> GetOnEntityInstance<TEntity, TResult>(object payload) where TResult : class;
//        Task<GetDataResult<TResult>> PostOnEntitySet<TEntity, TResult>(object payload) where TResult : class;
//        Task<GetDataResult<TResult>> GetOnEntitySet<TEntity, TResult>(object payload) where TResult : class;
//    }

//    public interface IEntity
//    {
//        bool OnSaving();
//        bool OnDeleting();
//        EntityValidationResult ValidateEntity();
//        ODataDataStore GetODataDataStore();
//    }

//    public abstract class ODataDataStore
//    {
//        public abstract Task<ODataResult<TResult>> PostOnEntityInstance<TEntity, TResult>(TEntity entity,
//            object payload) where TEntity : class;
//        public abstract Task<ODataResult<TResult>> GetOnEntityInstance<TEntity, TResult>(TEntity entity,
//            object payload) where TEntity : class;
//    }

//    public class ODataResult<T>
//    {
//        public T Data { get; set; }
//    }

//    public class ODataConfiguration
//    {
//        public static ODataConfiguration Instance { get; set; } 
//        public IHttpProvider HttpProvider { get; set; }
//    }
//}