using BookCase.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Entities.DTOs;

public class BookDto:IDto
{
    public int BookId { get; set; }

    public int UserId { get; set; }

    public string BookName { get; set; }

    public string Author { get; set; }

    public string Publisher { get; set; }

    public DateTime PublicationDate { get; set; }

    public string ImagePath { get; set; }

    public IFormFile? files { get; set; }

    public int CategoryCode { get; set; }

    public int SubCategoryCode { get; set; }
}
