﻿using StarCo.Domain.Factories;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    [DataContract]
    public class Inventory
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Key { get; private set; }
        [DataMember]
        public int Quantity { get; private set; }
        [DataMember]
        public int Size { get; private set; }
        [DataMember]
        public Storage Storage { get; private set; }

        public Inventory(string name, string key, int size, Storage storage)
        {
            Name = name;
            Size = size;
            Key = key;
            Storage = storage;
            Quantity = 0;
        }

        public bool Add(int quantity)
        {
            if (Storage.Store(Size * quantity))
            {
                Quantity += quantity;
                return true;
            }

            return false;
        }

        public bool Consume(int quantity)
        {
            if (quantity <= Quantity)
            {
                Quantity -= quantity;
                Storage.Free(Size * quantity);
                return true;
            }
            return false;
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            string glyph = ObjectFactory.ProductionLookup().GetGlyphKeyFor(Key);
            return new ColonyItemViewModel
            {
                Label = Name,
                Detail = Quantity.ToString(),
                SpriteUri = ObjectFactory.AssetName(glyph),
                Tokens = ""
            };
        }
    }
}
