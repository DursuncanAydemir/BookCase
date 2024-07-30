using BookCase.Core.DataAccess;
using BookCase.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.DataAccess.Abstract;

public interface ICategoryDal : IEntityRepository<Category>
{
}
