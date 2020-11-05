using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class PaymentFormRepository
    {
        private readonly TransThingsDbContext context;

        public PaymentFormRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PaymentForm>> GetAllPaymentFormsAsync()
        {
            var paymentForms = await context.PaymentForms.ToListAsync();
            return paymentForms;
        }

        public async Task<PaymentForm> GetPaymentFormByIdAsync(int id)
        {
            var paymentForm = await context.PaymentForms.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return paymentForm;
        }

        public async Task<PaymentForm> GetPaymentFormByNameAsync(string paymentFormName)
        {
            var paymentForm = await context.PaymentForms.SingleOrDefaultAsync(x => x.PaymentName.ToLower().Equals(paymentFormName.ToLower()));
            return paymentForm;
        }

        public async Task AddPaymentFormAsync(PaymentForm paymentForm)
        {
            await context.PaymentForms.AddAsync(paymentForm);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePaymentForm(PaymentForm paymentForm)
        {
            context.PaymentForms.Update(paymentForm);
            await context.SaveChangesAsync();
        }

        public async Task RemovePaymentForm(PaymentForm paymentForm)
        {
            context.PaymentForms.Remove(paymentForm);
            await context.SaveChangesAsync();
        }
    }
}
