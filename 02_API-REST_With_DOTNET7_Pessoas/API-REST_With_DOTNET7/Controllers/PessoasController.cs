using API_REST_With_DOTNET7.Model;
using API_REST_With_DOTNET7.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_With_DOTNET7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly ILogger<PessoasController> _logger;
        private IPessoaService _pessoaService;

        public PessoasController(ILogger<PessoasController> logger, IPessoaService pessoaService)
        {
            _logger = logger;
            _pessoaService = pessoaService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_pessoaService.FindAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pessoa = _pessoaService.FindByID(id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Create([FromBody] Pessoa pessoa)
        {
            if (pessoa == null)
                return BadRequest();

            return Ok(_pessoaService.Create(pessoa));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public IActionResult Update([FromBody] Pessoa pessoa)
        {
            if (pessoa == null)
                return BadRequest();

            return Ok(_pessoaService.Update(pessoa));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _pessoaService.Delete(id);
            return NoContent();
        }
    }

}