﻿using BookCase.Core.DataAccess.EntityFramework;
using BookCase.Core.Entities.Concrete;
using BookCase.DataAccess.Abstract;
using BookCase.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.DataAccess.Concrete.EntityFramework;

public class EfUserDal : EfEntityRepositoryBase<User, BookCaseContext>, IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new BookCaseContext())
        {
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();

        }
    }
}
