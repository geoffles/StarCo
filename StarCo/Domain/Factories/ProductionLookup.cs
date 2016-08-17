using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Factories
{
    public class ProductionLookup
    {
        private static Prerequisite Consume(int amount, string key)
        {
            return new InventoryPrerequisite(key, amount, true);
        }

        private static Prerequisite Upgrades(string key)
        {
            return new ImprovementPrerequisite(key, 1, true);
        }

        private static Prerequisite Depends(int amount, string key)
        {
            return new ImprovementPrerequisite(key, amount, false);
        }

        private static IList<Prerequisite> Requires(params Prerequisite[] r)
        {
            return r;
        }

        private static IList<Prerequisite> None()
        {
            return new Prerequisite[0];
        }

        private enum SpaceAlloc
        {
            Storage,
            Improvements,
            Living
        }

        private class Production
        {
            public string Label;
            public string Category;
            public string SubCategory;
            public int Time;
            public int Size;
            public string Glyph;
            public SpaceAlloc SpaceAlloc;
            public IList<Prerequisite> Prerequisites;
        }

        private IDictionary<string, Production> ProductionDefinitions;
        public ProductionLookup()
        {
            ProductionDefinitions = new Dictionary<string, Production>
            {
                { "smallstorage", new Production
                    {
                        Label = "Small Storage", Category = "Improvements", SubCategory = "Storage",
                        Glyph = "SmallStorage", 
                        Time = 5, 
                        Size = 0,
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Improvements,
                        Prerequisites = Requires(Consume(5, "stone")) 
                    }
                }, { "mediumstorage", new Production
                    {
                        Label = "Medium Storage", Category = "Improvements", SubCategory = "Storage",
                        Time = 10, 
                        Size = 0, 
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Improvements,
                        Prerequisites = Requires(Consume(50, "stone"), Upgrades("smallstorage")) 
                    }
                }, { "largestorage", new Production
                    {
                        Label = "Large Storage", Category = "Improvements", SubCategory = "Storage",
                        Time = 15, 
                        Size = 0, 
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Improvements,
                        Prerequisites = Requires(Consume(500, "stone"), Upgrades("mediumstorage"))
                    } 
                }, { "basicmine", new Production
                    {
                        Label = "Basic Mine", Category = "Improvements", SubCategory = "Resources",
                        Time = 10, 
                        Size = 0, 
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Improvements,
                        Prerequisites = Requires(Consume(50, "stone"), Depends(1,"basicquarry")) 
                    } 
                }, { "basicquarry", new Production
                    {
                        Label = "Basic Quarry", Category = "Improvements", SubCategory = "Resources",
                        Time = 10,
                        Size = 0,
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Improvements,
                        Prerequisites = Requires()
                    }
                }, { "gold", new Production
                    {
                        Label = "Gold", Category = "Inventory", SubCategory = "Luxuries",
                        Glyph = "Gold",
                        Time = 3,
                        Size = 1,
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Storage,
                        Prerequisites = None()
                    }
                }, { "stone", new Production
                    {
                        Label = "Stone", Category = "Inventory", SubCategory = "Commodities",
                        Glyph = "Stone",
                        Time = 3,
                        Size = 1,
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Storage,
                        Prerequisites = None()
                    } 
                }, { "basicworker", new Production
                    {
                        Label = "Basic Worker", Category = "Workers", SubCategory = "Basic Workers",
                        Glyph = "BasicWorker",
                        Time = 3,
                        Size = 1,
                        SpaceAlloc = ProductionLookup.SpaceAlloc.Living,
                        Prerequisites = Requires(Consume(10, "iron"), Consume(1, "livingspace"), Depends(1, "colonyship"))
                    }
                }
            };
        }

        public ProductionSpace GetProductionSpaceFor(string resourceName)
        {
            if (!ProductionDefinitions.ContainsKey(resourceName))
            {
                throw new ArgumentException("No Resource '" + resourceName + "'");
            }

            Production production = ProductionDefinitions[resourceName];

            switch (production.SpaceAlloc)
            {
                case SpaceAlloc.Improvements: return new ProductionSpace.Improvement(production.Size);
                case SpaceAlloc.Living: return new ProductionSpace.Living(production.Size);
                case SpaceAlloc.Storage: return new ProductionSpace.Storage(production.Size);
                default: throw new ArgumentException("Unkown Space allocation");
            }
        }

        public IEnumerable<Prerequisite> GetPrerequisitesFor(string resourceName)
        {
            if (!ProductionDefinitions.ContainsKey(resourceName))
            {
                throw new ArgumentException("No Resource '" + resourceName + "'");
            }

            return ProductionDefinitions[resourceName].Prerequisites;
        }

        public bool CheckPrerequisites(string resourceName, Colony colony)
        {
            if (!ProductionDefinitions.ContainsKey(resourceName))
            {
                throw new ArgumentException("No Resource '" + resourceName + "'");
            }

            Production production = ProductionDefinitions[resourceName];

            return production.Prerequisites.All(p => p.Check(colony));
        }

        public int GetProductionTimeFor(string resourceName)
        {
            if (ProductionDefinitions.ContainsKey(resourceName))
            {
                return ProductionDefinitions[resourceName].Time;
            }
            throw new ArgumentException("No Resource '" + resourceName + "'");
        }

        public string GetLabelFor(string resourceName)
        {
            if (ProductionDefinitions.ContainsKey(resourceName))
            {
                return ProductionDefinitions[resourceName].Label;
            }
            throw new ArgumentException("No Resource '" + resourceName + "'");
        }

        public string GetGlyphKeyFor(string resourceName)
        {
            if (ProductionDefinitions.ContainsKey(resourceName))
            {
                return ProductionDefinitions[resourceName].Glyph;
            }
            throw new ArgumentException("No Resource '" + resourceName + "'");
        }
    }
}
