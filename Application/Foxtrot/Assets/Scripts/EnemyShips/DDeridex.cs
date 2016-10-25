/*****************************************************************************************************************************************************************************************************************************
 * File: DDeridex.cs
 * Author: Austin Welborn
 * Date: 10/23/2016
 * Date Modified: 10/24/2016
 * Description:  Child Class of EnemyBase for the D'Deridex boss enemy ship
 *
 */

using UnityEngine;
using System.Collections;

public class DDeridex : EnemyBase {

	// Use this for initialization
	void Awake () {
		m_BaseMaxHealth = 400f;
		m_MoveSpeed = 3.0f;
		m_Name = "D'Deridex";
		m_points = 1000f;

		if (m_SpriteRenderer == null) {
			m_SpriteRenderer = GetComponent<SpriteRenderer> ();
			var temp = Resources.Load ("SpriteRenderers/dderidex_renderer", typeof(SpriteRenderer)) as SpriteRenderer;
			m_SpriteRenderer.sprite = temp.sprite;
			m_SpriteRenderer.enabled = true;
		}
	}
    
    void Update()
    {
        // Get the enemy current position
        Vector2 position = transform.position;
        var direction = 1;

        if (position.x > 6.5)
        {
            //Compute the enemy new position
            position = new Vector2(position.x - m_MoveSpeed * Time.deltaTime, position.y);

            //Update the ship with new position
            transform.position = position;
        }
        else
        {
            if (position.y > 5.5)
            {
                direction = -1;
                position = new Vector2(position.x, position.y + direction * m_MoveSpeed * Time.deltaTime);
            }
            else if (position.y < -5.5)
            {
                direction = 1;
                position = new Vector2(position.x, position.y + direction * m_MoveSpeed * Time.deltaTime);
            }
            else
            {
                if (direction == 1)
                {
                    position = new Vector2(position.x, position.y + direction * m_MoveSpeed * Time.deltaTime);
                }
                if (direction == -1)
                {
                    position = new Vector2(position.x, position.y + direction * m_MoveSpeed * Time.deltaTime);
                }
            }
            transform.position = position;
        }

        // Find the left-most edge of the screen. If the ship is to the left of it, destroy it.
        var bounds = Globals.GetCameraBounds(gameObject);
        float minX = bounds.m_MinX;

        var pos = transform.position;
        if (pos.x < minX)
        {
            Destroy(gameObject);
            return;
        }
    }

    //float PingPong(float t, float minLength, float maxLength)
    //{
    //    return Mathf.PingPong(t, maxLength - minLength) + minLength;
    //}
}
