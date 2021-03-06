﻿using Repo.DTOs;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public interface IRepo_Patient : IRepositoryBase<patient>
    {
        bool CardNumberExist(string cardNo);
        dto_Patients GetPatientById(int patientId);
        dto_Patients GetPatientByCardNumber(string cardNumber);
        IQueryable<dto_Patients> GetAllPatients();
        RepositoryActionResult<dto_Patients> AddPatient(dto_Patients pat);
        RepositoryActionResult<dto_Patients> EditPatient(dto_Patients pat);
        RepositoryActionResult<dto_Patients> DeletePatient(long patId);

        #region Patient's Biovital Repo
        IQueryable<dto_BioVital> GetAllBiovitalsForPatient(int patientID);
        IQueryable<dto_BioVital> Get5BiovitalsForPatient(int patientID);
        RepositoryActionResult<dto_BioVital> AddBiovital4Patient(dto_BioVital bio, int patientID);
        RepositoryActionResult<dto_BioVital> EditBiovital4Patient(dto_BioVital bio);
        RepositoryActionResult<dto_BioVital> DeleteBiovital4Patient(int bioId);
        #endregion

        #region Appointment Repo
        //IQueryable<appointment> GetAllAppointments();
        //appointment GetAnAppointment(long id);
        //IQueryable<appointment> GetPatientAppointment(string cardNumber);
        //bool AddPatientsAppointment(appointment patApp, string cardNumber, long AttdID);
        //bool EditPatientsAppointment(appointment patApp, string cardNumber, long AttdID);
        //bool DeletePatientsAppointment(long ID);
        #endregion
    }
}
