using System;
using CardGame.Services.StorageService;

namespace CardGame.Services.TypeRegistryService
{
    public struct TypeModel : IStorageEntity
    {
        public string Id { get; set; }
        public Type Type;
    }
}