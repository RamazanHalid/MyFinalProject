using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IOwnerService
    {
        IDataResult<List<Owner>> GetAll();
        IResult Add(Owner owner);
    }
}