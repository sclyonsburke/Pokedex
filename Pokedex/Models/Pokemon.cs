using System.Collections.Generic;
using System.Linq;

namespace Pokedex.Models
{
    public class Pokemon
    {
        public Pokemon()
        {
            evolutions = new List<Pokemon>();
            previousEvolutions = new List<Pokemon>();
        }

        public void SortEvolutions()
        {
            this.previousEvolutions = this.previousEvolutions.OrderBy(p => p.id).ToList();
            this.evolutions = this.evolutions.OrderBy(p => p.id).ToList();
        }

        public string id;

        public string name;

        public int number;

        public string image;

        public bool isFavorite;

        public List<string> types;

        public string displayTypes {
            get { 
                if(this.types.Count > 1)
                {
                    return types.First();
                }
                else
                {
                    return types.First() + "/" + types.Last();
                }
            }
        }

        public struct StatRange
        {
            public string minimum;
            public string maximum;
        }

        public StatRange weight;

        public StatRange height;

        public List<Pokemon> evolutions;

        public List<Pokemon> previousEvolutions;

        public int maxCP;

        public int maxHP;

        public string sound;
    }
}