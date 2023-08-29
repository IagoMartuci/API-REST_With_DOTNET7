using Microsoft.AspNetCore.Mvc;

namespace API_REST_With_DOTNET7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculadoraController : ControllerBase
    {
        private readonly ILogger<CalculadoraController> _logger;

        public CalculadoraController(ILogger<CalculadoraController> logger)
        {
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Soma/{n1}/{n2}")]
        public IActionResult GetSoma(string n1, string n2)
        {
            if (IsNumeric(n1) && IsNumeric(n2))
            {
                var soma = ConvertToDecimal(n1) + ConvertToDecimal(n2);
                return Ok(soma.ToString());
            }

            return BadRequest("Valor inválido!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Subtracao/{n1}/{n2}")]
        public IActionResult GetSubtracao(string n1, string n2)
        {
            if (!IsNumeric(n1) || !IsNumeric(n2))
            {
                return BadRequest("Valor inválido!");
            }
            var subtracao = ConvertToDecimal(n1) - ConvertToDecimal(n2);
            return Ok(subtracao.ToString());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Multiplicacao/{n1}/{n2}")]
        public IActionResult GetMultiplicacao(string n1, string n2)
        {
            if (IsNumeric(n1) && IsNumeric(n2))
            {
                var multiplicacao = ConvertToDecimal(n1) * ConvertToDecimal(n2);
                return Ok(multiplicacao.ToString());
            }
            return BadRequest("Valor inválido!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Divisao/{n1}/{n2}")]
        public IActionResult GetDivisao(string n1, string n2)
        {
            if (!IsNumeric(n1) || !IsNumeric(n2))
            {
                return BadRequest("Valor inválido!");
            }
            var divisao = ConvertToDecimal(n1) / ConvertToDecimal(n2);
            return Ok(divisao.ToString());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("RaizQuadrada/{n}")]
        public IActionResult GetRaizQuadrada(string n)
        {
            if (IsNumeric(n))
            {
                var raizQuadrada = Math.Sqrt((double)ConvertToDecimal(n));
                return Ok(raizQuadrada.ToString());
            }
            return BadRequest("Valor inválido!");
        }
            private bool IsNumeric(string strNum)
            {
                double number;
                // Quando for fazer o Parse se der certo é true para numérico
                bool isNumber = double.TryParse(strNum, System.Globalization.NumberStyles.Any,
                    System.Globalization.NumberFormatInfo.InvariantInfo, out number);
                return isNumber;
            }

            private decimal ConvertToDecimal(string strNum)
            {
                decimal decimalValue;
                if (decimal.TryParse(strNum, out decimalValue))
                {
                    return decimalValue;
                }
                return 0;
            }
        }
    }