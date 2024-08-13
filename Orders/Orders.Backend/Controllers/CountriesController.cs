using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    //[Route("api/[controller]/")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        //creamos el construtor con el ctor,para pasarle la inyeccion de dependencia de la clase DataContext con la propiedad context
        //control punto en la palabra context para que nos cree la la propiedad de lectura private readonly DataContext _context, esto para usar la 
        //inyecciones en cualquier lugar
        public CountriesController(DataContext context)
        {
            _context = context;
        }


        //creamos el metodo para obtener los paises o Countries con HttpGet
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Countries.ToListAsync());
        }

        //creamos el metodo para obtener pais o Countries por ID con HttpGet 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            { 
                return NotFound();//devuelve una repuesta 404
            }
            return Ok(country);
        }

        //creamos el metodo para agregar paises o Countries con un post
        [HttpPost]
        public async Task< IActionResult>PostAsync(Country country) 
        {
            _context.Add(country);
           await _context.SaveChangesAsync();
            return Ok(country);
        }

        //creamos el metodo para Actualizar paises o Countries con un Put
        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //creamos el metodo para Eliminar pais o Countries por ID con HttpDelete 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();//devuelve una repuesta 404
            }
            _context.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
