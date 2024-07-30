using BookCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Entities.Concrete;

public class Book:IEntity
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string BookName { get; set; }

    public string Author { get; set; }

    public string Publisher { get; set; }

    public DateTime PublicationDate { get; set; }

    public DateTime AddDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string ImagePath { get; set; }


}
