using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninja : MonoBehaviour
{
    private Rigidbody2D m_rig;
    public float speed;
    public int vidas_n = 4;
    // Start is called before the first frame update
    void Start()
    {
        m_rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rig.velocity = new Vector2(speed, m_rig.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "plataforma")
        {
            speed *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "bolaFuego")
        {
            vidas_n--;
            if(vidas_n <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        if(other.gameObject.tag == "ataqueGrande")
        {
            vidas_n = 0;
            Destroy(this.gameObject);
        }

    }
}
