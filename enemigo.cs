﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D m_rig;
    public float speed;
    void Start()
    {
        m_rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rig.velocity = new Vector2(speed, m_rig.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.tag == "plataforma")
        {
            speed *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
        }    
    }
}
