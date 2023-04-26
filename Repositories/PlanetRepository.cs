using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly IDictionary<string, IPlanet> planetsByName;

        public PlanetRepository()
        {
            planetsByName = new Dictionary<string, IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planetsByName.Values.ToList();
        public void Add(IPlanet model)
        {
            planetsByName.Add(model.Name, model);
        }

        public bool Remove(IPlanet model)
        {
            return planetsByName.Remove(model.Name);
        }

        public IPlanet FindByName(string name)
        {
            IPlanet planet = null;
            if (planetsByName.ContainsKey(name))
            {
                planet = planetsByName[name];
            }

            return planet;
        }
    }
}
