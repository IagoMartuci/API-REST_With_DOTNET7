using API_REST_With_DOTNET7.Business;
using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Hypermedia.Filters;
using API_REST_With_DOTNET7.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace API_REST_With_DOTNET7.Controllers
{
    [ApiVersion("2")]
    [ApiController]
    [Route("api/pessoas/v{version:apiVersion}")]
    public class PessoasController : ControllerBase
    {
        private ILog _log = LogManager.GetLogger("Pessoas Controller V2");
        private IPessoaBusiness _business;

        public PessoasController(IPessoaBusiness business)
        {
            _business = business;
        }

        // Teste
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(PessoaVO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("idade/{idade}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetByIdade(int idade)
        {
            try
            {
                return Ok(_business.FindByIdadeBusiness(idade));
            }
            catch (MySqlException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return NotFound(ex.Message);
            }
        }

        // Personalizando o ResponseType no Swagger
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PessoaVO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_business.FindAllBusiness());
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(PessoaVO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_business.FindByIdBusiness(id));
            }
            catch (MySqlException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return NotFound(ex.Message);
            }
        }

        // Personalizando o ResponseType no Swagger
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(PessoaVO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        // public IActionResult Create([FromBody] Pessoa pessoa)
        public IActionResult Create([FromBody] PessoaVO pessoa)
        {
            try
            {
                return Ok(_business.CreateBusiness(pessoa));
            }
            catch (MySqlException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(PessoaVO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        // public IActionResult Update([FromBody] Pessoa pessoa)
        public IActionResult Update([FromBody] PessoaVO pessoa)
        {
            try
            {
                return Ok(_business.UpdateBusiness(pessoa));
            }
            catch (MySqlException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _business.DeleteBusiness(id);
                return NoContent();
            }
            catch (MySqlException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return NotFound(ex.Message);
            }
        }
    }
}