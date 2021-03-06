using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Jar"))
        {
            StartCoroutine(Despawn());
        }
    }

    private IEnumerator Despawn()
    {
        gameObject.GetComponent<Animator>().speed *= 8;
        AudioManager.Instance.Play("StarCoin");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.SetCoinTaken(gameObject);
        gameObject.SetActive(false);
    }
}
