using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Entities.DTOs;

public class NoteDto
{
    public int NoteId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public string Notes { get; set; }

    public NoteType NoteType { get; set; }

    public ShareType ShareType { get; set; }
}

public enum NoteType 
{
    Private=0,
    Public=1  
}

public enum ShareType
{
    Private=0,
    Public=1,
    JustFriend=2
}