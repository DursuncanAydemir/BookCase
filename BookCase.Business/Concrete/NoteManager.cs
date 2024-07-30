using BookCase.Business.Abstract;
using BookCase.Core.Utilities.Result;
using BookCase.DataAccess.Abstract;
using BookCase.DataAccess.Concrete.EntityFramework;
using BookCase.Entities.Concrete;
using BookCase.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Business.Concrete;

public class NoteManager : INoteService
{
    private readonly INoteDal _noteDal;
    public NoteManager(INoteDal noteDal)
    {
        _noteDal = noteDal;
    }

    public IResult Add(NoteDto noteDto)
    {
        _noteDal.Add(new Note
        {
            AddDate = DateTime.Now,
            BookId = noteDto.BookId,
            Notes = noteDto.Notes,
            NoteType = (int)noteDto.NoteType,
            UserId = noteDto.UserId,
            UpdateDate = DateTime.Now
        });
        return new SuccessResult();
    }

    public IResult Delete(NoteDto noteDto)
    {
        var note = _noteDal.Get(n => n.Id == noteDto.NoteId);
        _noteDal.Delete(note);
        return new SuccessResult();
    }

    public IDataResult<List<NoteDto>> GetAll()
    {
        return new SuccessDataResult<List<NoteDto>>(_noteDal.GetAll().Select(b => new NoteDto()
        {
            BookId = b.BookId,
            Notes = b.Notes,
            NoteType = (NoteType)b.NoteType,
            UserId = b.UserId
        }).ToList());
    }

    public IResult Update(NoteDto noteDto)
    {
        Note note = new Note()
        {
            Notes = noteDto.Notes,
            NoteType = (int)noteDto.NoteType,
            UserId = noteDto.UserId,
            UpdateDate = DateTime.Now
        };
        _noteDal.Update(note);
        return new SuccessResult();
    }

    public IDataResult<List<NoteDto>> GetBookNotesByBookId(int bookId, int userId, NoteType noteType)
    {

        var notes = _noteDal.GetAll(n => n.Id == bookId && (n.UserId == userId || n.NoteType == (int)NoteType.Public)).Select(note => new NoteDto
        {
            BookId = note.BookId,
            Notes = note.Notes,
            NoteType = noteType,
            NoteId = note.Id,
            UserId = note.UserId
        }).ToList();
        return new SuccessDataResult<List<NoteDto>>(notes);
    }

    public IResult UpdateNoteStatus(int noteId, ShareType shareType)
    {
        var notes = _noteDal.Get(n => n.Id == noteId);
        Note note = new Note()
        {
            Notes=notes.Notes,
            ShareType = (int)shareType,
            UserId = notes.UserId,
            UpdateDate = DateTime.Now
        };
        _noteDal.Update(note);
        return new SuccessResult();
    }
}
