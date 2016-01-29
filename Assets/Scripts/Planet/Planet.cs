using UnityEngine;
using System.Collections.Generic;

namespace Simoncouche.Planets {
	/// <summary>
	/// The global planet information, parent to Planet Chunk
	/// </summary>
	public class Planet : MonoBehaviour {

		/// <summary>
		/// The many part of the planet
		/// </summary>
		public List<PlanetChunk> chunks { get; private set; }

		void Awake() {
			chunks = new List<PlanetChunk>();
		}

		/// <summary>
		/// Returns if this planet has the target chunk
		/// </summary>
		/// <param name="chunk">Target chunk</param>
		/// <returns>True if it contains the chunk</returns>
		public bool PlanetContainsChunk(PlanetChunk chunk) {
			return chunks.Contains(chunk);
		}

		/// <summary>
		/// Add a chunk to this planet, used when a chunk collides with a planet
		/// </summary>
		/// <param name="chunk">Reference to the collinding chunk</param>
		/// <param name="pos">The position of the chunk</param>
		/// <param name="rot">The rotation of the chunk</param>
		public void AddChunkToPlanet(PlanetChunk chunk, Vector3 pos, Vector3 rot) {
			if (!chunks.Contains(chunk)) {
				chunk.transform.SetParent(transform);
				chunks.Add(chunk);
				chunk.transform.localPosition = pos;
				chunk.transform.localRotation = Quaternion.Euler(rot);
				ChangeVelocityWhenMerging(chunk);
			}
		}

		/// <summary>
		/// Changes the velocity of the entire planet based on new fragment
		/// </summary>
		/// <param name="chunk"></param>
		private void ChangeVelocityWhenMerging(PlanetChunk chunk) {
			Rigidbody2D planet = this.GetComponent<Rigidbody2D>();
			planet.velocity = (planet.velocity + chunk.GetComponent<Rigidbody2D>().velocity) / 2;
			chunk.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}
}
