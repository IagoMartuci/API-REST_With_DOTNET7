using API_REST_With_DOTNET7.Business;
using API_REST_With_DOTNET7.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace API_REST_With_DOTNET7.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/")]
    public class LivrosController : ControllerBase
    {
        private ILog _log = LogManager.GetLogger(typeof(LivrosController));
        private ILivroBusiness _business;

        public LivrosController(ILivroBusiness business)
        {
            _business = business;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        public IActionResult Create([FromBody] Livro livro)
        {
            try
            {
                return Ok(_business.CreateBusiness(livro));
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public IActionResult Update([FromBody] Livro livro)
        {
            try
            {
                return Ok(_business.UpdateBusiness(livro));
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
