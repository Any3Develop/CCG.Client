﻿using System;
using CardGame.Services.StorageService;

namespace CardGame.Services.TypeRegistryService
{
    public struct TypeItem : IStorageItem
    {
        public string Id { get; set; }
        public Type Type;
    }
}