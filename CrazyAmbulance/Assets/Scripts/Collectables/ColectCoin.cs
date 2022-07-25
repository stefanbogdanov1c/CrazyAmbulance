using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectCoin : MonoBehaviour
{
    public AudioSource coinFX;

    void OnTriggerEnter (Collider other)
    {
        coinFX.Play();
        CollectableControl.coinCount += 1;
        this.gameObject.SetActive(false);
    }
}
