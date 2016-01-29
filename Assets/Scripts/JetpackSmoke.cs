using UnityEngine;
using System.Collections;

public class JetpackSmoke : MonoBehaviour {

	#region PrivateVariables
	/// <summary>
	/// Correspond to the jetpack's particle system
	/// </summary>
	private ParticleSystem _jetpackParticleSystem;
	/// <summary>
	/// A reference to our player controller in order to get the orientation where to propulse our particles
	/// </summary>
	private PlayerController _playerController;
	#endregion

	#region PublicVariables
	/// <summary>
	/// Maximum emission rate of ParticleSystem.emission.rate
	/// </summary>
	public float maxEmissionRate;
	#endregion

	void Awake() {
		_jetpackParticleSystem = GetComponent<ParticleSystem>();
		_playerController = GetComponentInParent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

		//Need a local variable of the EmissionModule since ParticleSystem.emissionRate is deprecated
		ParticleSystem.EmissionModule emissionModule=_jetpackParticleSystem.emission;
		
		//Do a rotation with player inputs
		/*_jetpackParticleSystem.transform.rotation = Quaternion.LookRotation(
			new Vector3(-_playerController.GetLeftAnalogHorizontal(), -_playerController.GetLeftAnalogVertical(), 0.0f));*/
		//transform.LookAt(this.transform.position + new Vector3(-_playerController.GetLeftAnalogHorizontal(), -_playerController.GetLeftAnalogVertical()));

		//Modification of the particle emission rate with player inputs
		ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
		rate.constantMax = ((Mathf.Abs(_playerController.GetLeftAnalogHorizontal()) + 
			Mathf.Abs(_playerController.GetLeftAnalogVertical()) / 1.0f) * maxEmissionRate);
		emissionModule.rate = rate;
	}
}
