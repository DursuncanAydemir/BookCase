using BookCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Entities.Concrete;

public class Category : IEntity
{
    public int Id { get; set; }

    public int CategoryCode { get; set; }

    public string CategoryName { get; set; }
}
