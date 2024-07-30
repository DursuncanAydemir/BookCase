using BookCase.Core.Utilities.Result;
using BookCase.Entities.Concrete;
using BookCase.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Business.Abstract;

public interface IBookService
{
    IDataResult<List<BookDto>> GetAll();
    IResult Add(BookDto book);
    IResult Update(BookDto book);
    IResult Delete(BookDto book);
    IDataResult<BookDto> GetById(int id);
    IDataResult<List<BookDto>> GetBooksByCategory(int categoryCode, int subCategoryCode);
    IDataResult<List<BookDto>>GetBookByName(string bookName);
    //IDataResult<List<BookDto>> GetBookNotesByBookId(int bookId);
}
