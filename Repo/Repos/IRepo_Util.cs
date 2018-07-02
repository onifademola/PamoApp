﻿using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public interface IRepo_Util : IRepositoryBase<ClientCompany>
    {
        List<KeyValuePair<int, string>> ProcessStatuses();
        List<KeyValuePair<int, string>> QueueTypes();
        List<KeyValuePair<int, string>> ColorCodes();
        List<KeyValuePair<int, string>> ClientGroupType();
        List<KeyValuePair<int, string>> AdmissionStatus();
        DateTime GetNijaTime(DateTime timeToConvert);
        void AddToRolesTable();
    }
}
