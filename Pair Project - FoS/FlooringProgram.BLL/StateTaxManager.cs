using System;
using System.Collections.Generic;
using FlooringProgram.Contracts;
using FlooringProgram.Data;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{
    public class StateTaxManager
    {
        private readonly IStateTaxRepository _stateTaxRepository;

        public StateTaxManager()
        {
            _stateTaxRepository = RepositoryFactory.GetStateTaxRepository();
        }

        public Response<StateTax> AddStateTax(StateTax stateTaxToAdd)
        {
            var response = new Response<StateTax>();

            try
            {
                _stateTaxRepository.AddStateTax(stateTaxToAdd);
                response.Success = true;
                response.Message = "State added.";
                response.Data = _stateTaxRepository.LoadStateTax(stateTaxToAdd.StateAbbreviation);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to add state.";
            }
            return response;
        }

        public Response<StateTax> RemoveStateTax(string stateTaxToRemove)
        {
            var response = new Response<StateTax>();

            try
            {
                _stateTaxRepository.RemoveStateTax(stateTaxToRemove);
                response.Success = true;
                response.Message = "State removed.";
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to remove state.";
            }
            return response;
        }

        public Response<StateTax> EditStateTax(StateTax stateTaxToEdit)
        {
            var response = new Response<StateTax>();

            try
            {
                _stateTaxRepository.EditStateTax(stateTaxToEdit);
                response.Success = true;
                response.Message = "State edited.";
                response.Data = _stateTaxRepository.LoadStateTax(stateTaxToEdit.StateAbbreviation);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to edit state.";
            }
            return response;
        }

        public Response<StateTax> LoadStateTax(string stateAbbreviation)
        {
            var response = new Response<StateTax>();

            try
            {
                response.Success = true;
                response.Message = "State loaded.";
                response.Data = _stateTaxRepository.LoadStateTax(stateAbbreviation);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to load state.";
            }
            return response;
        }

        public Response<List<StateTax>> LoadAllStateTax()
        {
            var response = new Response<List<StateTax>>();

            try
            {
                response.Success = true;
                response.Message = "All states loaded.";
                response.Data = _stateTaxRepository.LoadAllStateTax();
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to load all states.";
            }
            return response;
        }
    }
}