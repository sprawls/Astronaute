using UnityEngine;
using System.Collections;

namespace Simoncouche.Planets {
	/// <summary>
	/// Useful function and enums for planets
	/// </summary>
	public class PlanetUtils {
		/// <summary>
		/// Possible color of planets
		/// </summary>
		public enum color {
			red,
			blue,
			green
		}

		/// <summary>
		/// Planet Chunk possible Letters
		/// </summary>
		public enum ChunkLetter { A, B, C, D, E, F, G, H, I, J }

		/// <summary>
		/// Creates a string from the planet information of a planet
		/// </summary>
		/// <param name="c"> The color of the planet </param>
		/// <param name="number"> It's numerical value </param>
		/// <returns> The info in string form </returns>
		public static string CreateStringWithPlanetInfo(color c, int number) {
			return c.ToString("g") + " " + number.ToString();
		}
	}
}
