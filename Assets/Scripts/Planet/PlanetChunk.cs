using UnityEngine;
using System.Collections;

namespace Simoncouche.Planets {
	/// <summary>
	/// The component attached to a planet chunk
	/// </summary>
	[RequireComponent(typeof(CircleCollider2D))]
	public class PlanetChunk : MonoBehaviour {

		[Header("Planet Property")]

		/// <summary>
		/// The Color of the planet
		/// </summary>
		[SerializeField]
		[Tooltip("The Assign color of the planet")]
		private PlanetUtils.color _color;
		/// <summary>
		/// Accessor of _color
		/// </summary>
		public PlanetUtils.color color {
			get {
				return _color;
			}
			private set { }
		}

		/// <summary>
		/// Associated Chunk Letter
		/// </summary>
		[SerializeField]
		[Tooltip("The associated chunk letter")]
		private PlanetUtils.ChunkLetter _chunkLetter;
		/// <summary>
		/// Accessors of _chunkLetter
		/// </summary>
		public PlanetUtils.ChunkLetter chunkLetter {
			get {
				return _chunkLetter;
			}
			private set { }
		}

		/// <summary>
		/// The radius of the circle collider
		/// </summary>
		[Header("Collider Property")]
		[SerializeField]
		[Tooltip("The radius of the circle collider")]
		[Range(0.1f, 5f)]
		private float _circleColliderRadius = 2;

		void Awake() {
			CircleCollider2D col = GetComponent<CircleCollider2D>();
			col.radius = _circleColliderRadius;
			col.isTrigger = true;
		}

		void OnTriggerEnter2D(Collider2D other) {
			PlanetChunk chunk = other.GetComponent<PlanetChunk>();
			if (chunk != null && chunk.color == _color) {
				PlanetManager.Instance.HandleChunkCollision(this, chunk);
			}
		}
	}
}
