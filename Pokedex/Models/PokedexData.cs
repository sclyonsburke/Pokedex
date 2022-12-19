using System.Collections.Generic;

namespace Pokedex.Models
{
    public class PokedexData
    {
        public PokedexData()
        {
            Types = new List<string>();
            Pokemon = new List<Pokemon>();
        }

        public int Count;

        public List<string> Types;

        public List<Pokemon> Pokemon;
    }
}