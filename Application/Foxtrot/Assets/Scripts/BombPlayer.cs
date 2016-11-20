using UnityEngine;
using System.Collections;

public class BombPlayer : MonoBehaviour {
  private float m_speed;
  private float m_blastRadius;
  private float m_minWaitTime;
  private float m_aliveTime;
  private int m_power;
  //private GameObject m_player;
  

	// Use this for initialization
	void Start () {
    m_speed = 0.10f;
    m_blastRadius = 10.0f;
    m_minWaitTime = 0.25f;
    m_aliveTime = 0.0f;
    m_power = 20;
  }
	
	// Update is called once per frame
	void Update () {
    m_aliveTime += Time.deltaTime;
    MoveBomb();
    bool bombDetonated = CheckForDetonation();
    if (bombDetonated) {
      Detonate();
    }
  }

  void MoveBomb() {
    // Find the right-most edge of the screen. If laser is to right of it, destroy it.
    float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
    float maxX = bottomCorner.x + m_blastRadius;

    var pos = transform.position;
    if (pos.x > maxX)
    {
      Destroy(gameObject);
      return;
    }

    // Move bomb to the right
    pos.x += m_speed;
    transform.position = pos;
  }

  bool CheckForDetonation() {
    if (Input.GetKey(KeyCode.B) && (m_aliveTime >= m_minWaitTime)) {
      return true;
    }
    else {
      return false;
    }
  }

  void Detonate() {
    // Destroy objects within blast radius
    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, m_blastRadius);
    //Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_blastRadius);
    foreach (Collider2D collider in hitColliders) {
      var distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);
      if (distance == 0) // no division by 0
        distance = 1;

      // TODO: replace "DestructibleObject" with "Enemy"
      if (collider.gameObject.tag == "DestructibleObject") {
        EnemyBase enemy = collider.gameObject.GetComponent<EnemyBase>();
        enemy.ApplyDamage((int)(m_power / distance));
      }
      if (collider.gameObject.tag == "Asteroid")
      {
        Asteroid asteroid = collider.gameObject.GetComponent<Asteroid>();
        asteroid.ApplyDamage((int)(m_power / distance));
      }
    }

    // Destroy bomb
    Destroy(gameObject);
    Instantiate(Resources.Load("Prefabs\\CustomExplosion"), transform.position, Quaternion.identity);
  }

  //void SetPlayer(GameObject player) {
  //  m_player = player;
  //}
}
