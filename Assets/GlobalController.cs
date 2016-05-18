﻿using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

	public float interval;
	private float timeLeft;
	public GameObject[] asteroidPrefabs;
	public GameObject asteroidParent;
	private GameObject tempAsteroid;
	public GameObject playerObject;
	public GameObject FuelPrefab;
	
	int numfuel=0;
	public int startingAsteroids = 1000;
	public float dimX, dimY;

	// Use this for initialization
	void Start () {
		timeLeft = interval;

		GameObject space = GameObject.Find("Space");
		Renderer spaceRenderer = space.GetComponent<Renderer>();
		dimX = spaceRenderer.bounds.size.x;
		dimY = spaceRenderer.bounds.size.y;

		Debug.Log(dimX + " " + dimY);

		for (int i = 0; i < startingAsteroids; i++) {
			Vector3 startingPosition = new Vector3(Random.Range(-dimX/2, dimX/2), Random.Range(-dimY/2, dimY/2), 0);
			createAsteroid(startingPosition);
		}
		createFuel(playerObject.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(numfuel==0){
			createFuel(playerObject.transform.position);
		}
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = interval;

			Vector3 playerPosition = playerObject.transform.position;
			createAsteroid(playerPosition);
		}	
	}

	void createAsteroid(Vector3 position) {
		// Debug.Log("Asteroid created!");

		Vector3 startingPosition = new Vector3 (
                Random.Range(-5, 5),
                Random.Range(-5, 5),
                0
                );

		startingPosition = position + startingPosition.normalized * 15;

		tempAsteroid = asteroidPrefabs[(int)Mathf.Round(Random.Range(0,2))];

		GameObject asteroid = (GameObject)Instantiate(
        		tempAsteroid,
        		startingPosition,
        		Quaternion.identity
        		);

		asteroid.transform.parent = asteroidParent.transform;
	}

	void createFuel(Vector3 position){
		numfuel=1;
		Vector3 startingPosition = new Vector3 (
                Random.Range(-10, 10),
                Random.Range(-10, 10),
                0
                );
		startingPosition = position + startingPosition.normalized * 15;
		GameObject fuel = (GameObject)Instantiate(
        		FuelPrefab,
        		startingPosition,
        		Quaternion.identity
        		);
	}

	public void pickedUpFuel(){
		numfuel=0;
	}
}
