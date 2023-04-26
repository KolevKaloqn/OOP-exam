using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private Mission mission;
        private int exploredPlanetsCount;

        public Controller()
        {
            astronauts = new AstronautRepository();
            planets = new PlanetRepository();
            mission = new Mission();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = null;
            if (type == nameof(Biologist))
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == nameof(Geodesist))
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == nameof(Meteorologist))
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            astronauts.Add(astronaut);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            planets.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronauts.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            astronauts.Remove(astronaut);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> astronautsToExplore = astronauts.Models.Where(x => x.Oxygen > 60).ToList();

            if (astronautsToExplore.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IPlanet planet = planets.FindByName(planetName);

            mission.Explore(planet, astronautsToExplore);
            exploredPlanetsCount++;

            List<IAstronaut> deadAstronauts = astronautsToExplore.Where(x => x.CanBreath == false).ToList();
            return string.Format(OutputMessages.PlanetExplored, planetName, deadAstronauts.Count);
            
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanetsCount} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (var astronaut in astronauts.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: { astronaut.Oxygen}");
                if (astronaut.Bag.Items.Count == 0)
                {
                    sb.AppendLine("Bag items: none");
                }
                else
                {
                    sb.AppendLine($"Bag items: {string.Join(", ", astronaut.Bag.Items)}");
                }

            }
            //"{exploredPlanetsCount} planets were explored!
            //Astronauts info:
            //Name: { astronautName}
            //Oxygen: { astronautOxygen}
            //
            return sb.ToString().TrimEnd();
        }
    }
}
