using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    public GameObject winCutScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.HasCard == true)
            {
                winCutScene.SetActive(true);
            }
            else
            {
                Debug.Log("You must have the keycard.");
            }
        }
    }
}
