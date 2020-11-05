using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class PaymentFormService : IPaymentFormService
    {
        private readonly IUnitOfWork unitOfWork;

        public PaymentFormService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<PaymentForm>> GetAllPaymentForms()
        {
            var paymentForms = await unitOfWork.PaymentFormRepository.GetAllPaymentFormsAsync();
            return paymentForms;
        }

        public async Task<PaymentForm> GetPaymentFormById(int id)
        {
            var paymentForm = await unitOfWork.PaymentFormRepository.GetPaymentFormByIdAsync(id);
            return paymentForm;
        }

        public async Task<GenericResponse> AddPaymentForm(PaymentForm paymentForm)
        {
            if (paymentForm == null)
                return new GenericResponse(false, "No payment form has been provided.");

            if (string.IsNullOrEmpty(paymentForm.PaymentName))
                return new GenericResponse(false, "Payment form name has not been provided.");

            var paymentFormAlreadyInDb = await unitOfWork.PaymentFormRepository.GetPaymentFormByNameAsync(paymentForm.PaymentName);
            if (paymentFormAlreadyInDb != null)
                return new GenericResponse(false, $"Payment form {paymentForm.PaymentName} already exists.");

            try
            {
                await unitOfWork.PaymentFormRepository.AddPaymentFormAsync(paymentForm);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New payment form has been created.");
        }

        public async Task<GenericResponse> RemovePaymentForm(int id)
        {
            var paymentFormToRemove = await unitOfWork.PaymentFormRepository.GetPaymentFormByIdAsync(id);
            if (paymentFormToRemove == null)
                return new GenericResponse(false, $"Payment form with id={id} does not exist.");

            try
            {
                await unitOfWork.PaymentFormRepository.RemovePaymentForm(paymentFormToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Payment form has been removed.");
        }

        public async Task<GenericResponse> UpdatePaymentForm(PaymentForm paymentForm, int id)
        {
            if (paymentForm == null)
                return new GenericResponse(false, "No payment form has been provided.");

            var paymentFormToUpdate = await unitOfWork.PaymentFormRepository.GetPaymentFormByIdAsync(id);
            if (paymentFormToUpdate == null)
                return new GenericResponse(false, $"Payment form with id={id} does not exist.");

            var paymentFormAlreadyInDb = await unitOfWork.PaymentFormRepository.GetPaymentFormByNameAsync(paymentForm.PaymentName);
            if (paymentFormAlreadyInDb != null)
                return new GenericResponse(false, $"Payment form {paymentForm.PaymentName} already exists.");

            paymentFormToUpdate.PaymentName = paymentForm.PaymentName;

            try
            {
                await unitOfWork.PaymentFormRepository.UpdatePaymentForm(paymentFormToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Payment form has been updated.");
        }
    }
}
