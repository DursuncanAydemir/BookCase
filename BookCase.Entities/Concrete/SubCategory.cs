using BookCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Entities.Concrete;

public class SubCategory : IEntity
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryCode { get; set; }

    public string SubCategoryName { get; set; }
}
