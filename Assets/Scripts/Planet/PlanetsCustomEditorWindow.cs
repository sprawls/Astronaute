using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Simoncouche.Planets {
	/// <summary>
	/// Custom editor window to setup planets data
	/// </summary>
	[System.Serializable]
	public class PlanetsCustomEditorWindow : EditorWindow {

		/// <summary>
		/// The planet data for every type of planet
		/// </summary>
		public static List<PlanetData> _planetData { get; private set; }

		/// <summary>
		/// A List of every planet calculator
		/// </summary>
		[SerializeField]
		private static List<string> _planetCalculators = new List<string>();

		[SerializeField]
		private static int _planetTypeNumber = 0;

		/// <summary>
		/// Open the custom editor window
		/// </summary>
		[MenuItem("Simoncouche/Planets/Planets Type Reference")]
		static void OpenPlanetsSetupWindow() {
			EditorWindow.GetWindow(typeof(PlanetsCustomEditorWindow));
		}

		/// <summary>
		/// Recalculate every planet data
		/// </summary>
		public static void RecalculatePlanetsData() {
			_planetData = new List<PlanetData>();
			Init();
			List<PlanetData> data = new List<PlanetData>();
			foreach(string calculator in _planetCalculators) {
				data.Add(Resources.Load<GameObject>("PlanetTemplate/"+calculator).GetComponent<PlanetDataCalculator>().Recalculate());
			}
			
			_planetData = data;

			/*foreach (PlanetChunkData d in _planetData[0].chunks) {
				Debug.Log(d.position);
				Debug.Log(d.rotation);
			}*/
		}

		void Awake() {
			Init();
		}

		public static void Init() {
			if (_planetCalculators.Count == 0) {
				_planetCalculators.Clear();
				string saveString = EditorPrefs.GetString("Planet Calculator Names");
				string cal = "";
				for (int i = 0; i < saveString.Length; i++) {
					if (saveString[i] == '~') {
						_planetCalculators.Add(cal);
						cal = "";
					} else {
						cal += saveString[i];
					}
				}
				_planetCalculators.Add(cal);
				_planetTypeNumber = _planetCalculators.Count;
			}
		}

		void OnDestroy() {
			string saveString = "";
			for (int i = 0; i < _planetCalculators.Count; i++) {
				if (i != 0) saveString += "~";
				saveString += _planetCalculators[i];
			}
			EditorPrefs.SetString("Planet Calculator Names", saveString);
		}

		void OnGUI() {
			GUILayout.Label("Reference", EditorStyles.boldLabel);
			_planetTypeNumber = EditorGUILayout.IntField("Planet Type Count", _planetTypeNumber);
			
			//List
			int count = _planetTypeNumber;

			//Remove out of defined range element
			if (_planetCalculators.Count > 0) {
				_planetCalculators.RemoveRange(count, _planetCalculators.Count - count);
			}
			//Debug.Log(PlanetsCustomEditorWindow._planetCalculators.Count);

			//Create tempList
			List<string> tempList = new List<string>(_planetCalculators);
			for (int i = 0; i <_planetTypeNumber; i++) {
				if (i >= tempList.Count) {
					tempList.Add(null);
				}
			}

			//Draw List
			EditorGUI.indentLevel = 1;
			for (int i = 0; i < _planetTypeNumber; i++) {
				tempList[i] = EditorGUILayout.TextField("prefab name "+i, tempList[i]);
			}

			_planetCalculators.Clear();
			foreach (string cal in tempList) {
				if (cal != null) {
					_planetCalculators.Add(cal);
				}
			}
			EditorGUI.indentLevel = 0;
		}
	}
}
