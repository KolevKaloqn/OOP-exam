using System;
using System.Collections.Generic;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;
        private bool canBreath = true;

        protected Astronaut(string name, double oxygen)
        {
            Name = name;
            Oxygen = oxygen;
            this.Bag = new Backpack();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidAstronautName);
                }

                name = value;
            }

        }

        public double Oxygen
        {
            get => oxygen;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }

                oxygen = value;
            }

        }

        public bool CanBreath
        {
            get => canBreath;
            protected set
            {
                canBreath = value;
            }
        } 

        public IBag Bag { get; protected set; }
        public virtual void Breath()
        {
            double currOxygen = this.Oxygen;
            if (currOxygen - 10 <= 0)
            {
                this.Oxygen = 0;
                this.CanBreath = false;
            }
            else
            {
                this.Oxygen -= 10;
            }
        }
    }
}
