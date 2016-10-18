/***************************************************************************************************************
* File:						EnemyBase.cs
* Author:					Austin Welborn
* Date Created:				10/16/2016
* Date Modified:			
* Description:
* Contains the EnemyBase class, which is to be used as an abstract class.  Children of this class will represent different types of enemy ships for the Borg, Romulan, and Klingon ships and also the bosses from each race.

***************************************************************************************************************/
using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {
    // Member variables
    public float m_SpriteWidthFromCenter;
    public float m_SpriteHeightFromCenter;

    // Each ship can have a different starting max health and move speed
    public float m_BaseMaxhealth;
    public float m_MoveSpeed = 0.08f;

    // Each ship can have a name and rendering model
    public string m_Name;
    public SpriteRenderer m_SpriteRender;

    // Each enemy ship can have its own points for scoreboard
    public float m_points;

	// Use this for initialization
	void Start () {
        var sprite = GetComponent<SpriteRenderer>();
        m_SpriteWidthFromCenter = (float)(sprite.bounds.size.x / 2);
        m_SpriteHeightFromCenter = (float)(sprite.bounds.size.y / 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
