using UnityEngine;
using System.Collections;

public class CustomExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
    ParticleSystem[] partSystem = GetComponentsInChildren<ParticleSystem>();
    Destroy(gameObject, partSystem[0].duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
