using UnityEngine;
using System.Collections;

namespace Simoncouche.Islands {
	/// <summary>
	/// The component attached to a Island chunk
	/// </summary>
	[RequireComponent(typeof(CircleCollider2D))]
	public class IslandChunk : MonoBehaviour {

		[Header("Island Property")]

		/// <summary>
		/// The Color of the Island
		/// </summary>
		[SerializeField]
		[Tooltip("The Assign color of the Island")]
		private IslandUtils.color _color;
		/// <summary>
		/// Accessor of _color
		/// </summary>
		public IslandUtils.color color {
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
		private IslandUtils.ChunkLetter _chunkLetter;
		/// <summary>
		/// Accessors of _chunkLetter
		/// </summary>
		public IslandUtils.ChunkLetter chunkLetter {
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
			IslandChunk chunk = other.GetComponent<IslandChunk>();
			if (chunk != null && chunk.color == _color) {
				IslandManager.Instance.HandleChunkCollision(this, chunk);
			}
		}
	}
}
