using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Client>> GetAllClients()
        {
            var clients = await unitOfWork.ClientRepository.GetAllClientsAsync();
            return clients;
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await unitOfWork.ClientRepository.GetClientByIdAsync(id);
            return client;
        }

        public async Task<GenericResponse> AddClient(Client client)
        {
            if (client == null)
                return new GenericResponse(false, "Client's data has not been provided.");

            if (string.IsNullOrEmpty(client.ClientFirstName) || string.IsNullOrEmpty(client.ClientLastName))
                return new GenericResponse(false, "Client's personal data information has not been provided.");

            if (client.Gender != 'm' && client.Gender != 'M' && client.Gender != 'k' && client.Gender != 'K')
                return new GenericResponse(false, "Incorrect gender has been provided.");

            if (client.Gender == 'm')
                client.Gender = 'M';

            else if (client.Gender == 'k')
                client.Gender = 'K';

            bool isPeselValid = PeselValidator.Validate(client.ClientPeselNumber, client.Gender, client.BirthDate);
            if (!isPeselValid)
                return new GenericResponse(false, "Pesel number is incorrect.");

            var clientAlreadyInDb = unitOfWork.ClientRepository.GetClientByPeselNumberAsync(client.ClientPeselNumber);
            if (clientAlreadyInDb != null)
                return new GenericResponse(false, "Client with given Pesel number already exists.");

            try
            {
                await unitOfWork.ClientRepository.AddClientAsync(client);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New client has been added.");
        }

        public async Task<GenericResponse> RemoveClient(int id)
        {
            var clientToRemove = await unitOfWork.ClientRepository.GetClientByIdAsync(id);
            if (clientToRemove == null)
                return new GenericResponse(false, $"Client with id={id} not exist.");

            try
            {
                await unitOfWork.ClientRepository.RemoveClient(clientToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Client has been removed.");
        }

        public async Task<GenericResponse> UpdateClient(Client client, int id)
        {
            if (client == null)
                return new GenericResponse(false, "No client has been provided");

            var clientToUpdate = await unitOfWork.ClientRepository.GetClientByIdAsync(id);
            if (clientToUpdate == null)
                return new GenericResponse(false, $"Client with id={id} does not exist.");

            if (client.Gender != 'm' && client.Gender != 'M' && client.Gender != 'k' && client.Gender != 'K')
                return new GenericResponse(false, "Incorrect gender has been provided.");

            if (client.Gender == 'm')
                client.Gender = 'M';

            else if (client.Gender == 'k')
                client.Gender = 'K';

            bool isPeselValid = PeselValidator.Validate(client.ClientPeselNumber, client.Gender, client.BirthDate);
            if (!isPeselValid)
                return new GenericResponse(false, "Pesel number is incorrect.");

            clientToUpdate.ClientFirstName = client.ClientFirstName;
            clientToUpdate.ClientLastName = client.ClientLastName;
            clientToUpdate.ClientPeselNumber = client.ClientPeselNumber;
            clientToUpdate.CompanyFullName = client.CompanyFullName;
            clientToUpdate.CompanyShortName = client.CompanyShortName;
            clientToUpdate.ContactPhoneNumber1 = client.ContactPhoneNumber1;
            clientToUpdate.ContactPhoneNumber2 = client.ContactPhoneNumber2;
            clientToUpdate.Gender = client.Gender;
            clientToUpdate.BirthDate = client.BirthDate;
            clientToUpdate.ApartmentNumber = client.ApartmentNumber;
            clientToUpdate.BuildingNumber = client.BuildingNumber;
            clientToUpdate.City = client.City;
            clientToUpdate.Country = client.Country;
            clientToUpdate.NIP = client.NIP;
            clientToUpdate.StreetName = client.StreetName;
            clientToUpdate.ZipCode = client.ZipCode;

            try
            {
                await unitOfWork.ClientRepository.UpdateClient(clientToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Client has been updated.");
        }
    }
}
