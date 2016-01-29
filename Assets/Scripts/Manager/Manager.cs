using UnityEngine;
using System.Collections;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour {

	public static T Instance { get; protected set; }

	public virtual void Awake() {
		if (Instance == null) {
			Instance = (T) FindObjectOfType(typeof(T));
		}
	}

	void OnApplicationQuit() {
		Instance = null;
	}
}
