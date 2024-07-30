using BookCase.Business.Abstract;
using BookCase.Entities.Concrete;
using BookCase.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookCase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ILogger _logger;
        public NoteController(INoteService noteService, ILogger logger)
        {
            _noteService = noteService;
            _logger = logger;

        }
        [HttpPost]
        public IActionResult AddNote(NoteDto noteDto)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _noteService.Add(noteDto);
            sw.Stop();
            _logger.LogInformation($"Add note. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(NoteDto noteDto)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _noteService.Delete(noteDto);
            sw.Stop();
            _logger.LogInformation($"Delete note. ms:{sw.ElapsedMilliseconds}");
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
            var result = _noteService.GetAll();
            sw.Stop();
            _logger.LogInformation($"Get all note. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(NoteDto noteDto)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = _noteService.Update(noteDto);
            sw.Stop();
            _logger.LogInformation($"Update note. ms:{sw.ElapsedMilliseconds}");
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
