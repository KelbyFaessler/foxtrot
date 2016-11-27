/******************************************************************
 * File:            DropBase.cs
 * Author:          Kelby Faessler
 * Date Created:    11/27/2016
 * Date Modified:   11/27/2016
 * Description:
 * Base class for all drop classes, contains common functionality
 ******************************************************************/
using UnityEngine;
using System.Collections;

public class DropBase : MonoBehaviour {
  float m_MinX;

  // Use this for initialization
  void Start () {
    Boundaries cameraBounds = Globals.GetCameraBounds(gameObject);
    m_MinX = cameraBounds.m_MinX;
  }

  // Update is called once per frame
  void Update()
  {
    MoveAndRotate();
  }

  // The item moves left at a constant speed and rotates
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
}
