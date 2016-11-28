using UnityEngine;
using System.Collections;

public class SpellSpawner : MonoBehaviour {

	public GameObject[] projectiles;
	public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GvrController.State != GvrConnectionState.Connected) {
			// Pause game. 
		}
		if (GvrController.ClickButtonUp || Input.GetKeyDown(KeyCode.L)) {
			GameObject projectile;
			projectile = (GameObject) Instantiate(projectiles[0], spawnPoint.transform.position, transform.rotation);
			projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 60f);
			Object.Destroy(projectile, 3f);
		}
	}
}
