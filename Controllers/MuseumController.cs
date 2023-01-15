using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
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
    public class MuseumController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static readonly string[] Themes = new[] { "Art", "Natural Science", "History" };
        ApplicationDBContext context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MuseumController(ApplicationDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get All Museums
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetMuseum")]
        public async Task<ActionResult<List<Museum>>> GetMuseums()
        {
            return await context.Museum.ToListAsync();

        }
        /// <summary>
        /// Get all museums by theme
        /// </summary>
        /// <remarks>
        /// The only valid themes are:
        /// Art, Natural Sciences and History
        /// </remarks>
        /// <param name="Theme"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetAllByTheme")]
        public async Task<ActionResult<List<Museum>>> GetAllByTheme([Required] string Theme)
        {
            var list = await context.Museum.ToListAsync();
            var mlist = list.FindAll(
                delegate (Museum museum)
                {
                    return museum.Theme.Equals(Theme);
                });
            return mlist;
        }


        /// <summary>
        /// Add a museum
        /// </summary>
        /// <param name="museum"></param>
        /// <remarks>
        /// Only next Themes are valid:
        /// Art, Natural Sciencies and History
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(Museum museum)
        {
            if(!Themes.Contains(museum.Theme))
            return BadRequest($"Theme {museum.Theme} is invalid");

            
                try
                {
                    await context.Museum.AddAsync(museum);
                    await context.SaveChangesAsync();

                }
                catch (DbUpdateException)
                {
                    return BadRequest("The museum is already exist");
                }
                catch (System.Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

                return Ok("Museum created");
            
        }

        /// <summary>
        /// Edit a museum
        /// </summary>
        /// <param name="museum"></param>
        /// <remarks>
        /// Only next Themes are valid:
        /// Art, Natural Sciencies and History
        /// </remarks>
        /// <returns></returns>

        [HttpPut]
        [Route("Edit")]

        public async Task<ActionResult> Edit(Museum museum)
        {
            if(!Themes.Contains(museum.Theme))
            return BadRequest($"Theme {museum.Theme} is invalid");
            
                try
                {
                    var created = context.Museum.Update(museum);
                    await context.SaveChangesAsync();

                }
                catch (DbUpdateException)
                {
                    return BadRequest("The museum is already exist");
                }
                catch (System.Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

                return Ok("Museum modified");
            
        }

        /// <summary>
        /// Delete a museum
        /// </summary>
        /// <param name="Id_Museum"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(int Id_Museum)
        {

            try
            {
                var museum = context.Museum.Find(Id_Museum);
                var created = context.Museum.Remove(museum);
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return BadRequest("The museum is not exist");
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok("Museum deleted");


        }
        
    }
}