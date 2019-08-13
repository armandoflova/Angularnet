﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public DataContext _context { get; }

        public ValuesController(DataContext context)
        {
           _context = context;
            
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> ObtenerValores()
        {
            var valores =await _context.Valores.ToListAsync();
            return Ok(valores);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerValor(int id)
        {
            var valor =await _context.Valores.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(valor);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
