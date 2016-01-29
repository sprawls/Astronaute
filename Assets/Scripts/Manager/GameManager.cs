using UnityEngine;
using System.Collections;

public class GameManager : Manager<GameManager> {

	public override void Awake() {
		base.Awake();
		if (Instance == this) {
			DontDestroyOnLoad(gameObject);
		}
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {          
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Time.timeScale = 5f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            Time.timeScale = 25f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            Time.timeScale = 50f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            Time.timeScale = 100f;
        }
    }
}
