using AppProtoype.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinForms.SQLite;

namespace AppProtoype.Services
{
    public interface IBayManagerService
    {
        Task<List<ServiceModel>> GetServicesByGarageID(StoreModel store);
        Task<List<string>> GetStoreSchedule(AppointmentDetails appointment, List<int> selectedServiceIDs, DateTime dateSelected);
    }
}