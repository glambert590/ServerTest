using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    
    public class ArticleController : ControllerBase
    {
        ApplicationDBContext context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ArticleController(ApplicationDBContext context)
        {
            this.context = context;

        }

        /// <summary>
        /// Get all articles
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("GetArticles")]
        public async Task<ActionResult<List<Article>>> GetArticles()
        {
            return await context.Article.ToListAsync();
        }

        /// <summary>
        /// Find an article by Id
        /// </summary>
        /// <param name="Id_Article"></param>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<Article>> GetById(int Id_Article)
        {
            try
            {
                var found = await context.FindAsync<Article>(Id_Article);

                if (found == null)
                {
                    return BadRequest("Article not found");
                }
                return found;
            }
            catch (System.Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.InnerException.Message);
            }

        }

        /// <summary>
        /// Add one or few articles
        /// </summary>
        /// <param name="articles"></param>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(IEnumerable<Article> articles)
        {
            try
            {
                await context.Article.AddRangeAsync(articles);
                await context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok("Article created");
        }


        /// <summary>
        /// Remove an article
        /// </summary>
        /// <param name="Id_Article"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Remove")]
        public async Task<ActionResult> Remove(int Id_Article)
        {
            try
            {
                var article = await context.Article.FindAsync(Id_Article);
                context.Article.Remove(article);
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return BadRequest("The article is not exist");
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok("Article deleted");

        }

        /// <summary>
        /// Set an article as damaged
        /// </summary>
        /// <param name="Id_Article"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Damaged")]
        public async Task<ActionResult> Damaged(int Id_Article)
        {
            Article article = await context.Article.FindAsync(Id_Article);
            article.isDamaged = true;
            try
            {
                var created = context.Article.Update(article);
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return BadRequest("The article doesn't exist");
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok("Article has been modified successfuly");

        }

        /// <summary>
        /// Relocate an article to another museum
        /// </summary>
        /// <param name="Id_Article"></param>
        /// <param name="Id_RefMuseum"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Relocate")]
        public async Task<ActionResult> Relocate_Article(int Id_Article, int Id_RefMuseum)
        {
            Article article = context.Article.Find(Id_Article);
            article.Id_RefMuseum = Id_RefMuseum;
            try
            {
                context.Article.Update(article);
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return BadRequest("Article doesn't exist");
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok("Article has been relocated successfuly");
        }

        /// <summary>
        /// Retrieve all damaged articles
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("RetrieveAll")]
        public async Task<ActionResult> RetrieveAll()
        {

            var list = context.Article.Where(x => x.isDamaged == true).ToList();
            list.ForEach(x => x.isDamaged = false);

            try
            {
                context.Article.UpdateRange(list);
                await context.SaveChangesAsync();

            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok("All articles has been retrieved");

        }
    }

}