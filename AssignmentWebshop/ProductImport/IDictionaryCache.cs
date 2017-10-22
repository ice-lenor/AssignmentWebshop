using System;
using AssignmentWebshop.Models;
using System.Data.Entity;

namespace AssignmentWebshop.ProductImport
{
    /// <summary>
    /// Cache of dictionary values
    /// </summary>
    public interface IDictionaryCache
    {
        int? GetIdInDictionary<T>(String valueRaw)
            where T : class, INamedDictionary, new();
    }
}
