using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OwnerManager:IOwnerService
    {
        private IOwnerDal _ownerDal;

        public OwnerManager(IOwnerDal ownerDal)
        {
            _ownerDal = ownerDal;
            
        }


        public IDataResult<List<Owner>> GetAll()
        {

            return new SuccessDataResult<List<Owner>>(_ownerDal.GetAll());
        }

        public IResult Add(Owner owner)
        {
            _ownerDal.Add(owner);
            return new SuccessResult("Added!");
        }
    }
}