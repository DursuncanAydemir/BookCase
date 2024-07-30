using BookCase.Core.Entities.Concrete;
using BookCase.Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Business.Abstract;

public interface IUserService
{
    List<OperationClaim> GetClaims(User user);
    IResult Add(User user);
    User GetByMail(string email);
}
