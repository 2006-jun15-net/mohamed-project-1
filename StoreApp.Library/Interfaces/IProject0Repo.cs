using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace StoreApp.Library.Interfaces
{
    public interface IProject0Repo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Add(T obj);
        void Delete(object id);
        void Save();

    }
}
