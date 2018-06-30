using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    //public class Repo_Labs : RepositoryBase<PamoDbEntities, labw>, IRepo_Labs
    //{
    //    public IQueryable GetAllLabws()
    //    {
    //        var model = _entities.labws;
    //        return model;
    //    }

    //    public bool AddLabw(labw pat)
    //    {
    //        //var entity = Mapper.Map<eregister>(pat);            
    //        _entities.labws.Add(pat);
    //        var result = _entities.SaveChanges();
    //        if (result > 0)
    //            return true;
    //        else
    //            return false;
    //    }

    //    public bool EditLabw(labw pat)
    //    {
    //        //var entity = Mapper.Map<eregister>(pat);
    //        _entities.Set<labw>().Attach(pat);
    //        _entities.Entry<labw>(pat).State = System.Data.Entity.EntityState.Modified;
    //        _entities.SaveChanges();
    //        var result = _entities.SaveChanges();
    //        if (result > 0)
    //            return true;
    //        else
    //            return false;
    //    }

    //    public bool DeleteLabw(long patId)
    //    {
    //        var patient = _entities.labws.FirstOrDefault(n => n.ID == patId);
    //        if (patient == null)
    //            return false;
    //        _entities.labws.Remove(patient);
    //        var result = _entities.SaveChanges();
    //        if (result > 0)
    //            return true;
    //        else
    //            return false;

    //    }
    //}
}
