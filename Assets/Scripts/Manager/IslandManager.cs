using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Simoncouche.Islands {
	/// <summary>
	/// Every Island data
	/// </summary>
	public class IslandManager : Manager<IslandManager> {
		
		/// <summary>
		/// The Island data for every type of Island
		/// </summary>
		private List<IslandData> _islandData = new List<IslandData>();

		/// <summary>
		/// A list of every Island currently in play
		/// </summary>
		private List<Island> _island = new List<Island>();

		/// <summary>
		/// The Island prefab reference
		/// </summary>
		[SerializeField]
		[Tooltip("Island Object Prefab Reference")]
		private GameObject _islandComponent;

		public override void Awake() {
			base.Awake();
			if (Instance == this) {
				//Get Island Data from the editor pref
				IslandCustomEditorWindow.RecalculateIslandData();
				_islandData = IslandCustomEditorWindow._islandData;
			}
		}

		/// <summary>
		/// Creates a Island from 2 chunk, Will not work for multiple piece of the same letter in one scene
		/// </summary>
		/// <param name="a">the first chunk</param>
		/// <param name="b">the second chunk</param>
		public void HandleChunkCollision(IslandChunk a, IslandChunk b) {
			Island a_IslandLink = ChunkContainedInIsland(a);
			Island b_IslandLink = ChunkContainedInIsland(b);
			
			//If both are contained in Island
			if (a_IslandLink != null && b_IslandLink != null && a_IslandLink != b_IslandLink) {
				List<IslandChunk> chunks = b_IslandLink.chunks;
				foreach(IslandChunk chunk in chunks) {
					IslandChunkData data = FindChunkInIslandData(chunk);
					a_IslandLink.AddChunkToIsland(chunk, data.position, data.rotation);
				}
				RemoveIsland(b_IslandLink);
			} 

			//If a is contained in a Island
			else if (a_IslandLink != null) {
				IslandChunkData data = FindChunkInIslandData(b);
				a_IslandLink.AddChunkToIsland(b, data.position, data.rotation);
			} 
			
			//If b is contained in a Island
			else if (b_IslandLink != null) {
				IslandChunkData data = FindChunkInIslandData(a);
				b_IslandLink.AddChunkToIsland(a, data.position, data.rotation);
			} 
			
			//If a & b are not contained in a Island
			else {
				CreateIsland(a, b);
			}
		}

		/// <summary>
		/// Check if the chunk is contained in a Island
		/// </summary>
		/// <param name="chunk">Chunk to check</param>
		/// <returns>THe reference to the Island that contains this chunk or null if no Island contains it </returns>
		private Island ChunkContainedInIsland(IslandChunk chunk) {
			foreach (Island Island in _island) {
				if (Island.IslandContainsChunk(chunk)) return Island;
			}
			return null;
		}

		/// <summary>
		/// Find the chunk data related to the chunk
		/// </summary>
		/// <param name="chunk">The reference to the chunk</param>
		/// <returns>The Island chunk data</returns>
		private IslandChunkData FindChunkInIslandData(IslandChunk chunk) {
			foreach (IslandData d_Island in _islandData) {
				if (d_Island.color == chunk.color) {
					foreach (IslandChunkData d_chunk in d_Island.chunks) {
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
		/// Creates a Island at position a and adds a, b has it's child. Adds the new Island to the _Island list
		/// </summary>
		/// <param name="a">First chunk</param>
		/// <param name="b">Second chunk</param>
		private void CreateIsland(IslandChunk a, IslandChunk b) {
			Debug.Log(a.transform.position + " " + b.transform.position + " " + FindMiddlePoint(a.transform.position, b.transform.position));
			GameObject Island = Instantiate(_islandComponent, FindMiddlePoint(a.transform.position, b.transform.position), a.transform.rotation) as GameObject;
			Island.name = "Island";

			IslandChunkData data = FindChunkInIslandData(a);
			Island.GetComponent<Island>().AddChunkToIsland(a, data.position, data.rotation);

			data = FindChunkInIslandData(b);
			Island.GetComponent<Island>().AddChunkToIsland(b, data.position, data.rotation);

			_island.Add(Island.GetComponent<Island>());
		}

		/// <summary>
		/// Removes the Island from the list then destroy it
		/// </summary>
		/// <param name="Island">The Island that need to be removed</param>
		private void RemoveIsland(Island Island) {
			_island.Remove(Island);
			Destroy(Island.gameObject);
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
