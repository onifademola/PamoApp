﻿using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public class Repo_Util : RepositoryBase<PamoDbEntities, company>, IRepo_Util
    {
        public List<KeyValuePair<int, string>> ProcessStatuses()
        {            
            var data = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "At OPD"),
                new KeyValuePair<int, string>(2, "At Consulting"),
                new KeyValuePair<int, string>(3, "Awaiting Lab/Scan Result"),
                new KeyValuePair<int, string>(4, "On Admission"),
                new KeyValuePair<int, string>(5, "At Pharmacy"),
                new KeyValuePair<int, string>(6, "With Accounts"),
                new KeyValuePair<int, string>(7, "Done")
            };
            return data;
        }

        public List<KeyValuePair<int, string>> QueueTypes()
        {
            var data = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Queue-OPD"),
                new KeyValuePair<int, string>(2, "Queue-Consulting"),
                new KeyValuePair<int, string>(3, "Queue-Lab/Scan"),
                new KeyValuePair<int, string>(4, "Queue-Admission"),
                new KeyValuePair<int, string>(5, "Queue-Pharmacy"),
                new KeyValuePair<int, string>(6, "Queue-Accounts"),
                new KeyValuePair<int, string>(7, "Done")
            };
            return data;
        }

        public List<KeyValuePair<int, string>> ColorCodes()
        {
            var data = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Light Blue"),
                new KeyValuePair<int, string>(2, "Blue"),
                new KeyValuePair<int, string>(3, "Purple"),
                new KeyValuePair<int, string>(4, "Red"),
                new KeyValuePair<int, string>(5, "Pink"),
                new KeyValuePair<int, string>(6, "Yellow"),                
                new KeyValuePair<int, string>(7, "Green")
            };
            return data;
        }

        public List<KeyValuePair<int, string>> ClientGroupType()
        {
            var data = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "HMO"),
                new KeyValuePair<int, string>(2, "NHIS"),
                new KeyValuePair<int, string>(3, "Private")
            };
            return data;
        }

        public List<KeyValuePair<int, string>> AdmissionStatus()
        {
            var data = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "On Admission"),
                new KeyValuePair<int, string>(2, "Discharged")
            };
            return data;
        }

        public DateTime GetNijaTime(DateTime timeToConvert)
        {
            //DateTime _date = DateTime.UtcNow.ToUniversalTime();
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
            DateTime resultingTime = DateTime.SpecifyKind(timeToConvert, DateTimeKind.Unspecified);
            var utcDate = TimeZoneInfo.ConvertTimeToUtc(resultingTime, tz);
            return utcDate;
        }

        //public string GenerateNewNextString(string param1, string param2, string param3)
        //{
        //    int batchno = 0;
        //    TableSerialNo promoBatch = new TableSerialNo();
        //    string thisBatch = promoBatch.GetNextNumberString(param1, param2, param3, ref batchno);
        //    return thisBatch;
        //}
    }
}
