using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.Sql;
using System.Data.SqlClient;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroController : ControllerBase
    {

        private string _req = "REQUEST RECEIVED: ";
        private string _res = "RESPONSE SENT: ";
        private readonly ILogger<HeroController> _logger;
        public HeroController(ILogger<HeroController> logger)
        {
            _logger = logger;
        }

        private void log(string msg) {
            _logger.LogInformation(msg);
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Hero>> GetAll()
        {
            log(_req + "Get All");
            log(_res + "200 OK w/ Hero Objects.");
            return HeroService.GetAll();
        }


        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Hero> Get(int id) {
            log(_req + "Get By ID " + id.ToString());
            var hero = HeroService.Get(id);

            if (hero == null)
            {
                log(_res + "404 Not Found");
                return NotFound();
            }

            log(_res + "200 OK w/ Hero Object.");
            return hero;
        }


        // POST action
        [HttpPost]
        public IActionResult Create(Hero hero)
        {            
            log(_req + "Post");
            // This code will save the hero and return a result

            // // SQL
            // var sql = new InsertDB(_logger, Helper.HeroesDB);
            // sql.Run(hero);
            // //


            HeroService.Add(hero);
            log(_res + "200 OK");
            return CreatedAtAction(nameof(Create), new { id = hero.id }, hero);
        }


        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Hero hero)
        {
            log(_req + "Put " + id);
            // This code will update the hero and return a result
            if (id != hero.id)
            {
                log(_res + "400 Bad Request");
                return BadRequest();
            }

            var existingHero = HeroService.Get(id);
            if(existingHero is null)
                return NotFound();

            HeroService.Update(hero);           
            log(_res + "200 OK");
            return Ok();
        }

        // PATCH action
        [HttpPatch("{id}")]
        public IActionResult Train(int id) {
            log(_req + "Patch ID " + id.ToString());

            // This code will train the hero.
            // if (trainer.dailyCount > 5) ...

            var hero = HeroService.Get(id);

            if (hero.dailyTrainCount == 5)
            {
                log(_res + "400: Training limit reached for id " + id);
                return BadRequest("Training limit exceeded");
            }

            HeroService.Train(hero);
            log(_res + "200 OK");
            return Ok();

        }


        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            log(_req + "Delete ID " + id.ToString());
            // This code will delete the hero and return a result
            var hero = HeroService.Get(id);

            if (hero is null)
            {
                log(_res + "404 Not Found");
                return NotFound();
            }

            HeroService.Delete(id);
            log(_res + "200 OK");
            return Ok();
        }
    }
}