using BookCase.Core.DataAccess.EntityFramework;
using BookCase.DataAccess.Abstract;
using BookCase.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.DataAccess.Concrete.EntityFramework;

public class EfSubCategoryDal : EfEntityRepositoryBase<SubCategory, BookCaseContext>, ISubCategoryDal
{
}
