using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class AstronautRepository:IRepository<IAstronaut>
    {
        private readonly IDictionary<string, IAstronaut> astronautsByName;

        public AstronautRepository()
        {
            astronautsByName = new Dictionary<string, IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => astronautsByName.Values.ToList();
        public void Add(IAstronaut model)
        {
            astronautsByName.Add(model.Name, model);
        }

        public bool Remove(IAstronaut model)
        {
            return astronautsByName.Remove(model.Name);
        }

        public IAstronaut FindByName(string name)
        {
            IAstronaut astronaut = null;
            if (astronautsByName.ContainsKey(name))
            {
                astronaut = astronautsByName[name];
            }

            return astronaut;
        }
    }
}
