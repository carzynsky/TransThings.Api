using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("payment-forms")]
    public class PaymentFormController : ControllerBase
    {
        private readonly IPaymentFormService paymentFormService;
        public PaymentFormController(IPaymentFormService paymentFormService)
        {
            this.paymentFormService = paymentFormService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentForm>>> GetAllPaymentForms()
        {
            var paymentForms = await paymentFormService.GetAllPaymentForms();
            if (paymentForms.Count == 0)
                return NoContent();

            return Ok(paymentForms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentForm>> GetPaymentForm([FromRoute] int id)
        {
            var paymentForm = await paymentFormService.GetPaymentFormById(id);
            if (paymentForm == null)
                return NoContent();
            return Ok(paymentForm);
        }

        [HttpPost]
        public async Task<ActionResult> AddPaymentForm([FromBody] PaymentForm paymentForm)
        {
            var addPaymentFormResult = await paymentFormService.AddPaymentForm(paymentForm);
            if (!addPaymentFormResult.IsSuccessful)
                return BadRequest(addPaymentFormResult);

            return Ok(addPaymentFormResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaymentForm([FromBody] PaymentForm paymentForm, [FromRoute] int id)
        {
            var updatePaymentFormResult = await paymentFormService.UpdatePaymentForm(paymentForm, id);
            if (!updatePaymentFormResult.IsSuccessful)
                return BadRequest(updatePaymentFormResult);

            return Ok(updatePaymentFormResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePaymentForm([FromRoute] int id)
        {
            var removePaymentFormResult = await paymentFormService.RemovePaymentForm(id);
            if (!removePaymentFormResult.IsSuccessful)
                return BadRequest(removePaymentFormResult);

            return Ok(removePaymentFormResult);
        }
    }
}
