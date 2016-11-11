using UnityEngine;
using System.Collections;

public class HealthDrop : MonoBehaviour {
  float m_HealthValue;
  float m_MinX;

	// Use this for initialization
	void Start () {
    m_HealthValue = Random.Range(1, 5);
    Boundaries cameraBounds = Globals.GetCameraBounds(gameObject);
    m_MinX = cameraBounds.m_MinX;
	}
	
	// Update is called once per frame
	void Update () {
    MoveAndRotate();
  }

  void MoveAndRotate()
  {
    transform.Rotate(new Vector3(0, 2f, 0));
    var pos = transform.position;
    pos.x -= 0.04f;
    if (pos.x < m_MinX)
    {
      Destroy(gameObject);
      return;
    }
    transform.position = pos;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Player.instance.ApplyHealth(m_HealthValue);
      Destroy(gameObject);
    }
  }
}
