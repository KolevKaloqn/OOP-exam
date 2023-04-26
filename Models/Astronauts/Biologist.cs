using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
   public class Biologist:Astronaut
    {
        public Biologist(string name) : base(name, 70)
        {

        }

        public override void Breath()
        {
            double currOxygen = this.Oxygen;
            if (currOxygen - 5 <= 0)
            {
                this.Oxygen = 0;
                this.CanBreath = false;
            }
            else
            {
                this.Oxygen -= 5;
            }
        }
    }
}
