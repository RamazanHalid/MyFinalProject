using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryOwnerDal:IOwnerDal
    {
        private List<Owner> _owners;

        public InMemoryOwnerDal()
        {
            _owners = new List<Owner>()
            {
                new Owner
                {
                    OwnerId = 1,
                    OwnerName = "Alperen"
                }
            };
        }
        public List<Owner> GetAll(Expression<Func<Owner, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Owner Get(Expression<Func<Owner, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Owner entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Owner entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Owner entity)
        {
            throw new NotImplementedException();
        }
    }
}