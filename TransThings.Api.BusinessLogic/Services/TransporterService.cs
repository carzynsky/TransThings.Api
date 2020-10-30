using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class TransporterService : ITransporterService
    {
        private readonly IUnitOfWork unitOfWork;

        public TransporterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Transporter>> GetAllTransporters()
        {
            var transporters = await unitOfWork.TransporterRepository.GetAllTransportersAsync();
            return transporters;
        }

        public async Task<Transporter> GetTransporterById(int id)
        {
            var transporters = await unitOfWork.TransporterRepository.GetTransporterByIdAsync(id);
            return transporters;
        }

        public async Task<GenericResponse> AddTransporter(Transporter transporter)
        {
            if (transporter == null)
                return new GenericResponse(false, "No transporter has been provided.");

            try
            {
                await unitOfWork.TransporterRepository.AddTransporterAsync(transporter);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New transporter has been created.");
        }

        public async Task<GenericResponse> RemoveTransporter(int id)
        {
            var transporterToRemove = await unitOfWork.TransporterRepository.GetTransporterByIdAsync(id);
            if (transporterToRemove == null)
                return new GenericResponse(false, $"Transporter with id={id} does not exist.");

            try
            {
                await unitOfWork.TransporterRepository.RemoveTransporter(transporterToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Transporter has been removed.");
        }

        public async Task<GenericResponse> UpdateTransporter(Transporter transporter, int id)
        {
            if (transporter == null)
                return new GenericResponse(false, "No transporter has been provided.");

            var transporterToUpdate = await unitOfWork.TransporterRepository.GetTransporterByIdAsync(id);
            if (transporterToUpdate == null)
                return new GenericResponse(false, $"Transporter with id={id} does not exist.");

            transporterToUpdate.City = transporter.City;
            transporterToUpdate.Country = transporter.Country;
            transporterToUpdate.FullName = transporter.FullName;
            transporterToUpdate.Mail = transporter.Mail;
            transporterToUpdate.NIP = transporter.NIP;
            transporterToUpdate.ShortName = transporter.ShortName;
            transporterToUpdate.StreetAddress = transporter.StreetAddress;
            transporterToUpdate.SupportedPathsDescriptions = transporter.SupportedPathsDescriptions;
            transporterToUpdate.ZipCode = transporter.ZipCode;

            try
            {
                await unitOfWork.TransporterRepository.UpdateTransporter(transporterToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Transporter has been updated.");
        }
    }
}
