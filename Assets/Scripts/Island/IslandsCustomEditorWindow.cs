using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Simoncouche.Islands {
	/// <summary>
	/// Custom editor window to setup Island data
	/// </summary>
	[System.Serializable]
	public class IslandCustomEditorWindow : EditorWindow {

		/// <summary>
		/// The Island data for every type of Island
		/// </summary>
		public static List<IslandData> _islandData { get; private set; }

		/// <summary>
		/// A List of every Island calculator
		/// </summary>
		[SerializeField]
		private static List<string> _islandCalculators = new List<string>();

		[SerializeField]
		private static int _islandTypeNumber = 0;

		/// <summary>
		/// Open the custom editor window
		/// </summary>
		[MenuItem("Simoncouche/Island/Island Type Reference")]
		static void OpenIslandSetupWindow() {
			EditorWindow.GetWindow(typeof(IslandCustomEditorWindow));
		}

		/// <summary>
		/// Recalculate every Island data
		/// </summary>
		public static void RecalculateIslandData() {
			_islandData = new List<IslandData>();
			Init();
			List<IslandData> data = new List<IslandData>();
			foreach(string calculator in _islandCalculators) {
				if (calculator != "") {
					data.Add(Resources.Load<GameObject>("IslandTemplate/" + calculator).GetComponent<IslandDataCalculator>().Recalculate());
				}
			}
			
			_islandData = data;

			/*foreach (IslandChunkData d in _IslandData[0].chunks) {
				Debug.Log(d.position);
				Debug.Log(d.rotation);
			}*/
		}

		void Awake() {
			Init();
		}

		public static void Init() {
			if (_islandCalculators.Count == 0) {
				_islandCalculators.Clear();
				string saveString = EditorPrefs.GetString("island Calculator Names");
				string cal = "";
				for (int i = 0; i < saveString.Length; i++) {
					if (saveString[i] == '~') {
						_islandCalculators.Add(cal);
						cal = "";
					} else {
						cal += saveString[i];
					}
				}
				_islandCalculators.Add(cal);
				_islandTypeNumber = _islandCalculators.Count;
			}
		}

		void OnDestroy() {
			string saveString = "";
			for (int i = 0; i < _islandCalculators.Count; i++) {
				if (i != 0) saveString += "~";
				saveString += _islandCalculators[i];
			}
			EditorPrefs.SetString("island Calculator Names", saveString);
		}

		void OnGUI() {
			GUILayout.Label("Reference", EditorStyles.boldLabel);
			_islandTypeNumber = EditorGUILayout.IntField("island Type Count", _islandTypeNumber);
			
			//List
			int count = _islandTypeNumber;

			//Remove out of defined range element
			if (_islandCalculators.Count > 0) {
				_islandCalculators.RemoveRange(count, _islandCalculators.Count - count);
			}
			//Debug.Log(islandCustomEditorWindow._islandCalculators.Count);

			//Create tempList
			List<string> tempList = new List<string>(_islandCalculators);
			for (int i = 0; i <_islandTypeNumber; i++) {
				if (i >= tempList.Count) {
					tempList.Add(null);
				}
			}

			//Draw List
			EditorGUI.indentLevel = 1;
			for (int i = 0; i < _islandTypeNumber; i++) {
				tempList[i] = EditorGUILayout.TextField("prefab name "+i, tempList[i]);
			}

			_islandCalculators.Clear();
			foreach (string cal in tempList) {
				if (cal != null) {
					_islandCalculators.Add(cal);
				}
			}
			EditorGUI.indentLevel = 0;
		}
	}
}
