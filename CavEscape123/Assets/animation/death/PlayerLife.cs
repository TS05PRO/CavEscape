using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigB;
    private Animator anim;

    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource spawnSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigB = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            deathSound.Play();

        }
    }

    private void Die()
    {
        anim.SetTrigger("death");
        rigB.bodyType = RigidbodyType2D.Static;
    }
    private void ResttartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        spawnSound.Play();
    }
}
