using UnityEngine;
using System.Collections.Generic;

namespace Simoncouche.Islands {
	/// <summary>
	/// The global Island information, parent to Island Chunk
	/// </summary>
	public class Island : MonoBehaviour {

		/// <summary>
		/// The many part of the Island
		/// </summary>
		public List<IslandChunk> chunks { get; private set; }

		void Awake() {
			chunks = new List<IslandChunk>();
		}

		/// <summary>
		/// Returns if this Island has the target chunk
		/// </summary>
		/// <param name="chunk">Target chunk</param>
		/// <returns>True if it contains the chunk</returns>
		public bool IslandContainsChunk(IslandChunk chunk) {
			return chunks.Contains(chunk);
		}

		/// <summary>
		/// Add a chunk to this Island, used when a chunk collides with a Island
		/// </summary>
		/// <param name="chunk">Reference to the collinding chunk</param>
		/// <param name="pos">The position of the chunk</param>
		/// <param name="rot">The rotation of the chunk</param>
		public void AddChunkToIsland(IslandChunk chunk, Vector3 pos, Vector3 rot) {
			if (!chunks.Contains(chunk)) {
				chunk.transform.SetParent(transform);
				chunks.Add(chunk);
				chunk.transform.localPosition = pos;
				chunk.transform.localRotation = Quaternion.Euler(rot);
				ChangeVelocityWhenMerging(chunk);
			}
		}

		/// <summary>
		/// Changes the velocity of the entire Island based on new fragment
		/// </summary>
		/// <param name="chunk"></param>
		private void ChangeVelocityWhenMerging(IslandChunk chunk) {
			Rigidbody2D Island = this.GetComponent<Rigidbody2D>();
			Island.velocity = (Island.velocity + chunk.GetComponent<Rigidbody2D>().velocity) / 2;
			chunk.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}
}
