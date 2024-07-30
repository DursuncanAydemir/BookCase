using BookCase.Core.Utilities.Result;
using BookCase.Entities.Concrete;
using BookCase.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Business.Abstract;

public interface INoteService
{
    IResult Add(NoteDto noteDto);
    IResult Update(NoteDto noteDto);
    IResult Delete(NoteDto noteDto);
    IResult UpdateNoteStatus(int noteId, ShareType shareType);
    IDataResult<List<NoteDto>> GetAll();
    IDataResult<List<NoteDto>> GetBookNotesByBookId(int bookId, int userId, NoteType noteType);
}
