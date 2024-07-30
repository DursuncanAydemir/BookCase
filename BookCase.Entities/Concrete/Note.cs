using BookCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Entities.Concrete;

public class Note : IEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public string Notes { get; set; }

    public int NoteType { get; set; }

    public int ShareType { get; set; }

    public DateTime AddDate { get; set; }

    public DateTime UpdateDate { get; set; }
}
