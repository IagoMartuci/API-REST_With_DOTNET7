using API_REST_With_DOTNET7.Business;
using API_REST_With_DOTNET7.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace API_REST_With_DOTNET7.Controllers
{
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/pessoas/")]
    public class PessoasControllerV2 : ControllerBase
    {
        private ILog _log = LogManager.GetLogger("Pessoas Controller V2");
        private IPessoaBusiness _business;

        public PessoasControllerV2(IPessoaBusiness business)
        {
            _business = business;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Create([FromBody] Pessoa pessoa)
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public IActionResult Update([FromBody] Pessoa pessoa)
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