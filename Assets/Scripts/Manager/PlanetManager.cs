using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Simoncouche.Planets {
	/// <summary>
	/// Every planets data
	/// </summary>
	public class PlanetManager : Manager<PlanetManager> {
		
		/// <summary>
		/// The planet data for every type of planet
		/// </summary>
		private List<PlanetData> _planetData = new List<PlanetData>();

		/// <summary>
		/// A list of every planet currently in play
		/// </summary>
		private List<Planet> _planets = new List<Planet>();

		/// <summary>
		/// The planet prefab reference
		/// </summary>
		[SerializeField]
		[Tooltip("Planet Object Prefab Reference")]
		private GameObject _planetComponent;

		public override void Awake() {
			base.Awake();
			if (Instance == this) {
				//Get Planet Data from the editor pref
				PlanetsCustomEditorWindow.RecalculatePlanetsData();
				_planetData = PlanetsCustomEditorWindow._planetData;
			}
		}

		/// <summary>
		/// Creates a planet from 2 chunk, Will not work for multiple piece of the same letter in one scene
		/// </summary>
		/// <param name="a">the first chunk</param>
		/// <param name="b">the second chunk</param>
		public void HandleChunkCollision(PlanetChunk a, PlanetChunk b) {
			Planet a_planetLink = ChunkContainedInPlanets(a);
			Planet b_planetLink = ChunkContainedInPlanets(b);
			
			//If both are contained in planet
			if (a_planetLink != null && b_planetLink != null && a_planetLink != b_planetLink) {
				List<PlanetChunk> chunks = b_planetLink.chunks;
				foreach(PlanetChunk chunk in chunks) {
					PlanetChunkData data = FindChunkInPlanetData(chunk);
					a_planetLink.AddChunkToPlanet(chunk, data.position, data.rotation);
				}
				RemovePlanet(b_planetLink);
			} 

			//If a is contained in a planet
			else if (a_planetLink != null) {
				PlanetChunkData data = FindChunkInPlanetData(b);
				a_planetLink.AddChunkToPlanet(b, data.position, data.rotation);
			} 
			
			//If b is contained in a planet
			else if (b_planetLink != null) {
				PlanetChunkData data = FindChunkInPlanetData(a);
				b_planetLink.AddChunkToPlanet(a, data.position, data.rotation);
			} 
			
			//If a & b are not contained in a planet
			else {
				CreatePlanet(a, b);
			}
		}

		/// <summary>
		/// Check if the chunk is contained in a planet
		/// </summary>
		/// <param name="chunk">Chunk to check</param>
		/// <returns>THe reference to the planet that contains this chunk or null if no planet contains it </returns>
		private Planet ChunkContainedInPlanets(PlanetChunk chunk) {
			foreach (Planet planet in _planets) {
				if (planet.PlanetContainsChunk(chunk)) return planet;
			}
			return null;
		}

		/// <summary>
		/// Find the chunk data related to the chunk
		/// </summary>
		/// <param name="chunk">The reference to the chunk</param>
		/// <returns>The planet chunk data</returns>
		private PlanetChunkData FindChunkInPlanetData(PlanetChunk chunk) {
			foreach (PlanetData d_planet in _planetData) {
				if (d_planet.color == chunk.color) {
					foreach (PlanetChunkData d_chunk in d_planet.chunks) {
						if (d_chunk.chunkLetter == chunk.chunkLetter) {
							return d_chunk;
						}
					}
				}
			}

			Debug.LogError("The type of chunk used does not exist");
			return null;
		}

		/// <summary>
		/// Creates a planet at position a and adds a, b has it's child. Adds the new planet to the _planets list
		/// </summary>
		/// <param name="a">First chunk</param>
		/// <param name="b">Second chunk</param>
		private void CreatePlanet(PlanetChunk a, PlanetChunk b) {
			Debug.Log(a.transform.position + " " + b.transform.position + " " + FindMiddlePoint(a.transform.position, b.transform.position));
			GameObject planet = Instantiate(_planetComponent, FindMiddlePoint(a.transform.position, b.transform.position), a.transform.rotation) as GameObject;
			planet.name = "Planet";

			PlanetChunkData data = FindChunkInPlanetData(a);
			planet.GetComponent<Planet>().AddChunkToPlanet(a, data.position, data.rotation);

			data = FindChunkInPlanetData(b);
			planet.GetComponent<Planet>().AddChunkToPlanet(b, data.position, data.rotation);

			_planets.Add(planet.GetComponent<Planet>());
		}

		/// <summary>
		/// Removes the planet from the list then destroy it
		/// </summary>
		/// <param name="planet">The planet that need to be removed</param>
		private void RemovePlanet(Planet planet) {
			_planets.Remove(planet);
			Destroy(planet.gameObject);
		}

		/// <summary>
		/// Finds the middle point between two Vector3
		/// </summary>
		/// <param name="a">First Vector</param>
		/// <param name="b">Second Vector</param>
		/// <returns>Middle point between a and b</returns>
		private Vector3 FindMiddlePoint(Vector3 a, Vector3 b) {
			return (a + (b - a) / 2);
		}
	}
}
