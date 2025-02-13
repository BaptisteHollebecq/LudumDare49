﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chest : Interactable
{
    CharacterController player;
    public enum item { Key, BigKey, Life, Sword }
    public item Gift;
    public GameObject Anim;
    public Sprite plusKey;
    public Sprite plusBoss;
    public Sprite plusLife;
    public Sprite plusSword;
    [Header("trapped")]
    public bool traped = false;
   
    public List<GameObject> Enemies = new List<GameObject>();

    public Animator animator;
    public  AudioSource source;

    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        Anim.SetActive(false);
        if (traped)
        {
            foreach (GameObject g in Enemies)
            {
                g.SetActive(false);
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        source.Play();
        animator.SetTrigger("OpenChest");
        if (!traped)
        {
            switch (Gift)
            {
                case item.Key:
                    {
                        player.keys++;
                        player.Hud.SetKeys();
                        Anim.GetComponent<SpriteRenderer>().sprite = plusKey;
                        break;
                    }
                case item.BigKey:
                    {
                        player.BigKeys++;
                        player.Hud.SetKeys();
                        Anim.GetComponent<SpriteRenderer>().sprite = plusBoss;
                        break;
                    }
                case item.Life:
                    {
                        player.Life++;
                        if (player.Life > 3)
                            player.Life = 3;
                        Anim.GetComponent<SpriteRenderer>().sprite = plusLife;
                        player.Hud.SetLife();
                        break;
                    }
                case item.Sword:
                    {
                        player.attackZone.GetComponent<AttackZone>().Ekip(true);
                        Anim.GetComponent<SpriteRenderer>().sprite = plusSword;
                        break;
                    }
            }
            Anim.SetActive(true);
            Anim.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            Anim.transform.DOMove(new Vector3(Anim.transform.position.x, Anim.transform.position.y + 2 , Anim.transform.position.z), 3f);
            Anim.GetComponent<SpriteRenderer>().DOFade(0, 3f).OnComplete(() =>
            {
                Destroy(Anim.gameObject);
            });
        }
        else
        {
            foreach (GameObject g in Enemies)
            {
                g.SetActive(true);
            }
        }
    }
}
