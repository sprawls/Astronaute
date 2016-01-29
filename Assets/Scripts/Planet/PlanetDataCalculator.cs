using UnityEngine;
using System.Collections.Generic;

namespace Simoncouche.Planets {
	/// <summary>
	/// Class to calculate the position of every type of piece on a planet
	/// </summary>
	public class PlanetDataCalculator : MonoBehaviour {
		/// <summary>
		/// The color of the planet
		/// </summary>
		[Header("Planet Information")]
		[SerializeField]
		[Tooltip("Planet color")]
		private PlanetUtils.color _planetColor;

		/*/// <summary>
		/// The number of the planet
		/// </summary>
		[SerializeField]
		[Tooltip("Planet number")]
		[Range(0, 10)]
		private int _planetNumber;*/

		/// <summary>
		/// Recalculate the planet data
		/// </summary>
		public PlanetData Recalculate() {
			List<PlanetChunkData> chunks = new List<PlanetChunkData>();
			foreach (PlanetChunk chunk in GetComponentsInChildren<PlanetChunk>()) {
				chunks.Add(new PlanetChunkData(chunk.chunkLetter, chunk.transform.localPosition, chunk.transform.localRotation.eulerAngles));
			}

			return  new PlanetData(_planetColor, chunks);
		}
	}

	/// <summary>
	/// The Data of each planet, representing the correct local position of each chunk
	/// </summary>
	[System.Serializable]
	public class PlanetData {
		/// <summary>
		/// The name of the planet: Color followed by number, ex: Red 2
		/// </summary>
		public PlanetUtils.color color { get; private set; }

		/// <summary>
		/// A list of every chunks related data
		/// </summary>
		public List<PlanetChunkData> chunks { get; private set; }

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="_name">The name of the planet: Color followed by number, ex: Red 2</param>
		/// <param name="_chunks">A list of every chunks related data</param>
		public PlanetData(PlanetUtils.color _color, List<PlanetChunkData> _chunks) {
			color = _color;
			chunks = _chunks;
		}
	}

	/// <summary>
	/// The data for every chunk
	/// It's position and letter associated with it
	/// </summary>
	[System.Serializable]
	public class PlanetChunkData {
		/// <summary>
		/// The letter associated to the chunk
		/// </summary>
		public PlanetUtils.ChunkLetter chunkLetter { get; private set; }

		/// <summary>
		/// The position of the chunk in a completed planet
		/// </summary>
		public Vector3 position { get; private set; }

		/// <summary>
		/// The rotation of the chunk in a completed planet
		/// </summary>
		public Vector3 rotation { get; private set; }

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="letter">The letter associated to the chunk</param>
		/// <param name="pos">The position of the chunk in a completed planet</param>
		/// <param name="rot">The rotation of the chunk in a completed planet</param>
		public PlanetChunkData(PlanetUtils.ChunkLetter letter, Vector3 pos, Vector3 rot) {
			chunkLetter = letter;
			position = pos;
			rotation = rot;
		}
	}
}
