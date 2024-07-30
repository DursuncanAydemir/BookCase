using BookCase.Business.Abstract;
using BookCase.DataAccess.Abstract;
using BookCase.Entities.Concrete;
using BookCase.Entities.DTOs;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static Azure.Core.HttpHeader;

namespace BookCase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, IWebHostEnvironment environment, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _environment = environment;
            _logger = logger;
        }

        [HttpPost("addbook")]
        public IActionResult AddBook([FromForm] BookDto bookDto)
        {
            try
            {
                _logger.LogInformation("Get method executing...");
                bookDto.ImagePath = "\\Upload\\" + bookDto.files.FileName;
                if (bookDto.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + bookDto.files.FileName))
                    {
                        bookDto.files.CopyTo(filestream);
                        filestream.Flush();
                        //  return "\\Upload\\" + objFile.files.FileName;
                    }
                }
                _bookService.Add(bookDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpGet("getbooksbycategory")]
        public IActionResult GetBooksByCategory(int categoryCode, int subCategoryCode)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _bookService.GetBooksByCategory(categoryCode,subCategoryCode);
            sw.Stop();
            _logger.LogInformation($"Get boks by category:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbookbyname")]
        public IActionResult GetBookByName(string bookName)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _bookService.GetBookByName(bookName);
            sw.Stop();
            _logger.LogInformation($"Get book by name. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Stopwatch sw = Stopwatch.StartNew();
        
            var result = _bookService.GetAll();
            sw.Stop();
            _logger.LogInformation($"GetAll books. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _bookService.GetById(id);
            sw.Stop();
            _logger.LogInformation($"Get by id. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(BookDto bookDto)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _bookService.Delete(bookDto);
            sw.Stop();
            _logger.LogInformation($"Delete book. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(BookDto bookDto)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _bookService.Update(bookDto);
            sw.Stop();
            _logger.LogInformation($"Update book. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
