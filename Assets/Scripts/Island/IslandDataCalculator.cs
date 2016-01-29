using UnityEngine;
using System.Collections.Generic;

namespace Simoncouche.Islands {
	/// <summary>
	/// Class to calculate the position of every type of piece on a Island
	/// </summary>
	public class IslandDataCalculator : MonoBehaviour {
		/// <summary>
		/// The color of the Island
		/// </summary>
		[Header("Island Information")]
		[SerializeField]
		[Tooltip("Island color")]
		private IslandUtils.color _islandColor;

		/*/// <summary>
		/// The number of the Island
		/// </summary>
		[SerializeField]
		[Tooltip("Island number")]
		[Range(0, 10)]
		private int _IslandNumber;*/

		/// <summary>
		/// Recalculate the Island data
		/// </summary>
		public IslandData Recalculate() {
			List<IslandChunkData> chunks = new List<IslandChunkData>();
			foreach (IslandChunk chunk in GetComponentsInChildren<IslandChunk>()) {
				chunks.Add(new IslandChunkData(chunk.chunkLetter, chunk.transform.localPosition, chunk.transform.localRotation.eulerAngles));
			}

			return  new IslandData(_islandColor, chunks);
		}
	}

	/// <summary>
	/// The Data of each Island, representing the correct local position of each chunk
	/// </summary>
	[System.Serializable]
	public class IslandData {
		/// <summary>
		/// The name of the Island: Color followed by number, ex: Red 2
		/// </summary>
		public IslandUtils.color color { get; private set; }

		/// <summary>
		/// A list of every chunks related data
		/// </summary>
		public List<IslandChunkData> chunks { get; private set; }

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="_name">The name of the Island: Color followed by number, ex: Red 2</param>
		/// <param name="_chunks">A list of every chunks related data</param>
		public IslandData(IslandUtils.color _color, List<IslandChunkData> _chunks) {
			color = _color;
			chunks = _chunks;
		}
	}

	/// <summary>
	/// The data for every chunk
	/// It's position and letter associated with it
	/// </summary>
	[System.Serializable]
	public class IslandChunkData {
		/// <summary>
		/// The letter associated to the chunk
		/// </summary>
		public IslandUtils.ChunkLetter chunkLetter { get; private set; }

		/// <summary>
		/// The position of the chunk in a completed Island
		/// </summary>
		public Vector3 position { get; private set; }

		/// <summary>
		/// The rotation of the chunk in a completed Island
		/// </summary>
		public Vector3 rotation { get; private set; }

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="letter">The letter associated to the chunk</param>
		/// <param name="pos">The position of the chunk in a completed Island</param>
		/// <param name="rot">The rotation of the chunk in a completed Island</param>
		public IslandChunkData(IslandUtils.ChunkLetter letter, Vector3 pos, Vector3 rot) {
			chunkLetter = letter;
			position = pos;
			rotation = rot;
		}
	}
}
