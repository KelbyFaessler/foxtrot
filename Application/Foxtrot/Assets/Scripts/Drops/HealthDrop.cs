/******************************************************************
 * File:            HealthDrop.cs
 * Author:          David Hite
 * Date Created:    11/10/2016
 * Date Modified:   11/10/2016
 * Description:
 * Contains the HealthDrop class, which defines the behavior of 
 * HealthDrop objects.
 ******************************************************************/
using UnityEngine;
using System.Collections;

public class HealthDrop : DropBase {
  float m_HealthValue;

	// Use this for initialization
	void Start () {
    m_HealthValue = Random.Range(1, 5);
	}

  // Update is called once per frame
  //void Update () {
  //  MoveAndRotate();
  //}

  // Apply health to player then self-destruct
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Player.instance.ApplyHealth(m_HealthValue);
      Destroy(gameObject);
    }
  }
}
