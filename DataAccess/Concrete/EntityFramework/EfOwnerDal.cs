using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOwnerDal: EfEntityRepositoryBase<Owner,NorthwindContext>,IOwnerDal
    {
        
    }
}