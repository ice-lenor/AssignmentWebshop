﻿using AssignmentWebshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace AssignmentWebshop.ProductImport
{
    /// <summary>
    /// Cache of dictionary values
    /// </summary>
    public class DictionaryCache : IDictionaryCache
    {
        static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        Dictionary<String, Dictionary<String, int>> m_dictionaryValues;
        ProductDBContext m_db;

        public DictionaryCache(ProductDBContext db)
        {
            m_db = db;
            m_dictionaryValues = new Dictionary<string, Dictionary<string, int>>();
        }

        /// <summary>
        /// Gets the dictionary value id by its name.
        /// If the value exists, returns its id.
        /// If the value doesn't exist, creates it in the database and then returns its id.
        /// </summary>
        /// <typeparam name="T">Type of value in a dictionary</typeparam>
        /// <param name="valueRaw">String name of the dictionary item</param>
        /// <param name="dbCollection">Collection of dictionary values</param>
        /// <returns>Id of the dictionary item</returns>
        public int? GetIdInDictionary<T>(String valueRaw)
            where T : class, INamedDictionary, new()
        {
            if (String.IsNullOrEmpty(valueRaw))
                return null;

            var dictionaryName = typeof(T).Name;

            locker.EnterReadLock();
            try
            {
                // first try to get the dictionary value by name from cache
                if (m_dictionaryValues.ContainsKey(dictionaryName))
                {
                    var dictionaryValues = m_dictionaryValues[dictionaryName];
                    // if value is present, return the id
                    if (dictionaryValues.ContainsKey(valueRaw))
                        return dictionaryValues[valueRaw];
                }
            }
            finally
            {
                locker.ExitReadLock();
            }

            // if the value is not in cache, let's add it
            locker.EnterWriteLock();
            try
            {
                var dbCollection = m_db.GetDbSetByItemType<T>();
                if (dbCollection == null)
                    return null;
                if (dbCollection.FirstOrDefault(x => x.Name == valueRaw) == null)
                {
                    try
                    {
                        // here could also be a set of validations for dictionary values

                        T valueToCreate = new T() { Name = valueRaw };
                        var valueDb = dbCollection.Add(valueToCreate);
                        m_db.ChangeTracker.DetectChanges();
                        m_db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        // this would probably mean we already have this entry in the db; great, we'll just refresh the cache then
                    }
                }

                m_dictionaryValues.Remove(dictionaryName);
                var dictionaryValues = dbCollection.ToDictionary(x => x.Name, x => x.Id);
                m_dictionaryValues.Add(dictionaryName, dictionaryValues);

                if (dictionaryValues.ContainsKey(valueRaw))
                    return dictionaryValues[valueRaw];
            }
            finally
            {
                locker.ExitWriteLock();
            }

            return null;
        }
    }
}