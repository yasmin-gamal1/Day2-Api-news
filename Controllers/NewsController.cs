using Day2_API;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Day2API.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
       
        static List<News> newsList = new List<News>()
        {
            new News() { Id = 1, Title = "Tech Innovations", Description = "New AI technology...", Author = "Ali Mohmmed" },
            new News() { Id = 2, Title = "Health Breakthrough", Description = "Vaccine research update...", Author = "Jana Wael" },
            new News() { Id = 3, Title = "Global Politics", Description = "New policies introduced...", Author = "Mona Ali" },
        };

        
        [HttpGet]
        public List<News> GetAll()
        {
            return newsList;
        }

       
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            News newsItem = newsList.FirstOrDefault(n => n.Id == id);
            if (newsItem == null) return NotFound();
            return Ok(newsItem);
        }

        
        [HttpGet("title/{title}")]
        public IActionResult GetByTitle(string title)
        {
            News newsItem = newsList.FirstOrDefault(n => n.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));
            if (newsItem == null) return NotFound();
            return Ok(newsItem);
        }

        
        [HttpGet("author/{author}")]
        public IActionResult GetByAuthor(string author)
        {
            var newsByAuthor = newsList.Where(n => n.Author.Equals(author, System.StringComparison.OrdinalIgnoreCase)).ToList();
            if (!newsByAuthor.Any()) return NotFound();
            return Ok(newsByAuthor);
        }

      
        [HttpPost]
        public IActionResult Add(News newsItem)
        {
            if (newsItem == null) return BadRequest();
            newsItem.Id = newsList.Count > 0 ? newsList.Max(n => n.Id) + 1 : 1;
            newsList.Add(newsItem);
            return CreatedAtAction(nameof(GetById), new { id = newsItem.Id }, newsItem);
        }

       
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, News updatedNews)
        {
            if (updatedNews == null) return BadRequest();
            News newsItem = newsList.FirstOrDefault(n => n.Id == id);
            if (newsItem == null) return NotFound();

            newsItem.Title = updatedNews.Title;
            newsItem.Description = updatedNews.Description;
            newsItem.Author = updatedNews.Author;

            return NoContent();
        }

        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            News newsItem = newsList.FirstOrDefault(n => n.Id == id);
            if (newsItem == null) return NotFound();

            newsList.Remove(newsItem);
            return Ok(newsItem);
        }
    }
}
