using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPlayer : MonoBehaviour
{
    public float JumpForce = 10;
    public float Velocity = 5;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    public GameObject BulletPrefab;
    public int vidas = 3;

    private static readonly string ANIMATOR_STATE = "estado";
    private static readonly int ANIMATION_IDLE = 0;
    public static readonly int ANIMATION_RUN = 1;
    private static readonly int ANIMATION_JUMP = 2;
    private static readonly int ANIMATION_CORRER_DISPARAR = 3;
    private static readonly int ANIMATION_SLIDE = 4;
    private static readonly int ANIMATION_DISPARAR = 5;

    private static readonly int RIGHT = 1;
    private static readonly int LEFT = -1;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        ChangeAnimation(ANIMATION_IDLE);

        //TXT_score.text = "Vida: " + vida;

       if(Input.GetKey(KeyCode.RightArrow)) //si presiono flecha a la derecha
        {
            Desplazarse(RIGHT);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Desplazarse(LEFT);
        }

        if(Input.GetKey(KeyCode.C))
        {
            Deslizarse();
        }

        if(Input.GetKeyUp(KeyCode.X))
        {
            ChangeAnimation(ANIMATION_DISPARAR);
            Disparar();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            //_audioSource.PlayOneShot(SaltoAudioClip);
            ChangeAnimation(ANIMATION_JUMP);
        }
    }

    private void Deslizarse()
    {
        ChangeAnimation(ANIMATION_SLIDE);
    }    

    private void Desplazarse(int position)
    {
        _rb.velocity = new Vector2(Velocity * position, _rb.velocity.y);
        _sr.flipX = position == LEFT;
        ChangeAnimation(ANIMATION_RUN); //Cambiamos el valor del atributo
    }

    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE, animation);
    }

    private void Disparar()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var bulletGO = Instantiate(BulletPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bulletGO.GetComponent<ataque>();

        if (_sr.flipX)
            controller.velX *= -1;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var tag = other.gameObject.tag;
        if(tag == "enemy")
        {
            vidas--;
        }   
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        var tag = other.gameObject.tag;
        if(tag == "boton")
        {
            SceneManager.LoadScene("Escena2");
        }    
    }


}
