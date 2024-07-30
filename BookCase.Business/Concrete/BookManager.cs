using BookCase.Business.Abstract;
using BookCase.Core.Entities.Concrete;
using BookCase.Core.Utilities.Result;
using BookCase.DataAccess.Abstract;
using BookCase.DataAccess.Concrete.EntityFramework;
using BookCase.Entities.Concrete;
using BookCase.Entities.DTOs;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookCase.Business.Concrete;

public class BookManager : IBookService
{
    private readonly IBookDal _bookDal;
    private readonly ICategoryDal _categoryDal;
    private readonly ISubCategoryDal _subCategoryDal;
    private readonly ILogger<BookManager> _logger;
    public BookManager(IBookDal bookDal, ICategoryDal categoryDal, ISubCategoryDal subCategoryDal,ILogger<BookManager> logger)
    {
        _bookDal = bookDal;
        _categoryDal = categoryDal;
        _subCategoryDal = subCategoryDal;
        _logger = logger;
    }

    public IResult Add(BookDto bookDto)
    {
        //Model Mapping Dto burdan çevrilcek.
        var categoryId = _categoryDal.Get(x => x.CategoryCode == bookDto.CategoryCode).Id;
        var subCategoryId = _subCategoryDal.Get(x => x.SubCategoryCode == bookDto.SubCategoryCode).Id;
        Book book = new Book()
        {
            BookName = bookDto.BookName,
            Author = bookDto.Author,
            ImagePath = bookDto.ImagePath,
            Publisher = bookDto.Publisher,
            PublicationDate = bookDto.PublicationDate,
            AddDate=DateTime.Now,
            CategoryId = categoryId,
            SubCategoryId = subCategoryId,
        };
        _bookDal.Add(book);
        return new SuccessResult();
    }

    public IResult Delete(BookDto bookDto)
    {
        var book = _bookDal.Get(b => b.Id == bookDto.BookId);
        _bookDal.Delete(book);
        return new SuccessResult();
    }

    public IDataResult<List<BookDto>> GetAll()
    {
        try
        {
            var books = _bookDal.GetAll().Select(b => new BookDto()
            {
                PublicationDate = b.PublicationDate,
                Publisher = b.Publisher,
                Author = b.Author,
                BookName = b.BookName,
                ImagePath = b.ImagePath
            }).ToList();
            return new SuccessDataResult<List<BookDto>>(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return new SuccessDataResult<List<BookDto>>(new List<BookDto>());
    }

    public IDataResult<List<BookDto>> GetBookByName(string bookName)
    {
        var books = _bookDal.GetAll(x => x.BookName.Contains(bookName)).Select(x => new BookDto
        {
            PublicationDate = x.PublicationDate,
            Publisher = x.Publisher,
            Author = x.Author,
            BookName = x.BookName,
            ImagePath = x.ImagePath
        }).ToList();
        return new SuccessDataResult<List<BookDto>>(books);
    }

    

    public IDataResult<List<BookDto>> GetBooksByCategory(int categoryCode,int subCategoryCode)
    {
        var categoryId = _categoryDal.Get(x => x.CategoryCode == categoryCode).Id;
        var subCategoryId= _subCategoryDal.Get(x=>x.SubCategoryCode == subCategoryCode).Id;
        var books = _bookDal.GetAll(x => x.CategoryId == categoryId && x.SubCategoryId == subCategoryId).Select(x => new BookDto 
        {
            PublicationDate=x.PublicationDate,
            Publisher=x.Publisher,
            Author=x.Author,
            BookName=x.BookName,
            ImagePath=x.ImagePath,
            CategoryCode=categoryCode,
            SubCategoryCode=subCategoryCode
        }).ToList();
        return new SuccessDataResult<List<BookDto>>(books);
        
    }

    public IDataResult<BookDto> GetById(int id)
    {
        var book = _bookDal.Get(b => b.Id == id);
        return new SuccessDataResult<BookDto>(new BookDto()
        {
            PublicationDate = book.PublicationDate,
            Publisher = book.Publisher,
            Author = book.Author,
            BookName = book.BookName,
            ImagePath = book.ImagePath
        });
    }

    public IResult Update(BookDto bookDto)
    {
        Book book = new Book()
        {
            BookName = bookDto.BookName,
            Author = bookDto.Author,
            ImagePath = bookDto.ImagePath,
            Publisher = bookDto.Publisher,
            PublicationDate = bookDto.PublicationDate,
            AddDate = DateTime.Now
        };
        _bookDal.Update(book);
        return new SuccessResult();
    }
}
