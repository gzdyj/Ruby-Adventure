using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3; //移动

    public float changeDirectionTime = 2f; //改变方向的时间
    public bool isVertical; //是否垂直方向移动
    private float changTimer; //改变方向的计时器
    private Vector2 moveDirection; //移动方向
    private Rigidbody2D rbody;
    private Animator anim;
    private bool isFixed; //是否被修复

    public ParticleSystem brokenEffect; //损坏特效
    public AudioClip fixedClip;//被修复的音效

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        moveDirection = isVertical ? Vector2.up : Vector2.right;//如果是垂直移动，方向朝上，否则方向朝右

        anim = GetComponent<Animator>();

        changTimer = changeDirectionTime;

        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed) return;//如果被修复了，就不执行一下所有的代码
        changTimer -= Time.deltaTime;
        if (changTimer < 0) {
            moveDirection *= -1;
            changTimer = changeDirectionTime;
        }
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);
        anim.SetFloat("moveX",moveDirection.x);
        anim.SetFloat("moveY",moveDirection.y);


        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null) {
            pc.ChangeHealth(-1);
        }
    }
    /// <summary>
    /// 敌人被修复
    /// </summary>
    public void Fixed()
    {
        isFixed = true;
        if (brokenEffect.isPlaying == true) {
            brokenEffect.Stop();
        }
        AudioManager.instance.AudioPlay(fixedClip);//播放被修复的音效
        rbody.simulated = false; //禁用物理
        anim.SetTrigger("fix"); //播放被修复的动画


    }
}
