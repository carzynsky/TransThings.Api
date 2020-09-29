using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IPaymentFormService
    {
        Task<List<PaymentForm>> GetAllPaymentForms();
        Task<PaymentForm> GetPaymentFormById(int id);
        Task<GenericResponse> AddPaymentForm(PaymentForm paymentForm);
        Task<GenericResponse> UpdatePaymentForm(PaymentForm paymentForm, int id);
        Task<GenericResponse> RemovePaymentForm(int id);
    }
}
