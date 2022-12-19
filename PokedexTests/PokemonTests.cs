using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Models;
using System.Linq;

namespace PokedexTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PokemonTests
    {
        [TestMethod]
        public void SortPokemonTest()
        {
            var pokemon = new Pokemon();
            pokemon.evolutions.Add(new Pokemon
            {
                id = "2"
            });
            pokemon.evolutions.Add(new Pokemon
            {
                id = "1"
            });

            pokemon.previousEvolutions.Add(new Pokemon
            {
                id = "2"
            });
            pokemon.previousEvolutions.Add(new Pokemon
            {
                id = "1"
            });

            pokemon.SortEvolutions();

            Assert.AreEqual(pokemon.evolutions.First().id, "1");
            Assert.AreEqual(pokemon.previousEvolutions.First().id, "1");
        }
    }
}
