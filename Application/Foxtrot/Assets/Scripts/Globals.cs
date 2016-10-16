/******************************************************************
 * File:            Globals.cs
 * Author:          David Hite
 * Date Created:    10/14/2016
 * Date Modified:   10/14/2016
 * Description:
 * Contains Global classes and structs
 ******************************************************************/
using UnityEngine;
using System.Collections;


/***********************************************************
/** Globals
 Class to contain global functions and variables.
/***********************************************************/
static public class Globals {

  /***********************************************************
  /** GetCameraBounds
   Gets the minimum and maximum x and y values for the camera
   view.

  @param  obj : GameObject we use to calculate camera relative
                to it
  /***********************************************************/
  static public Boundaries GetCameraBounds(GameObject obj)
  {
    float camDistance = Vector3.Distance(obj.transform.position, Camera.main.transform.position);
    Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
    Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
    Boundaries returnBounds = new Boundaries();

    returnBounds.m_MinX = bottomCorner.x;
    returnBounds.m_MaxX = topCorner.x;
    returnBounds.m_MinY = bottomCorner.y;
    returnBounds.m_MaxY = topCorner.y;

    return returnBounds;
  }
}

public struct Boundaries
{
  public float m_MinX { get; set; }
  public float m_MaxX { get; set; }
  public float m_MinY { get; set; }
  public float m_MaxY { get; set; }
}