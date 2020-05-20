using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerRendering : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FlipSprite(Vector2 _input)
    {
        if (_input.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (_input.x > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }

    public void Animate(string type,string animationName,float floatValue=0,bool boolValue=false,int intValue=0)
    {
        switch (type)
        {
            case "float":
                anim.SetFloat(animationName,floatValue);
                break;
            case "bool":
                anim.SetBool(animationName,boolValue);
                break;
            case "int":
                anim.SetInteger(animationName,intValue);
                break;
            case "trigger":
                anim.SetTrigger(animationName);
                break;
        }
    }

}
