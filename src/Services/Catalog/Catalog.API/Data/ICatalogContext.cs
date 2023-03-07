

using Catalog.API.Entities;
using MongoDB.Driver;
using System;
namespace Catalog.API.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

