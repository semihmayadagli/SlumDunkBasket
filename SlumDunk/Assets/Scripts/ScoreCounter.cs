using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    GameObject _GameManager;
    [SerializeField] private GameObject confirmationObject;
    [SerializeField] private AudioSource BallSound;
    
    void Start()
    {
        _GameManager = GameObject.FindWithTag("GameManager");        
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BasketCounter"))
        {
            StartCoroutine(BasketConfirmationWaiter());                       
        }
        if (other.gameObject.CompareTag("B_Confirm"))
        {
            _GameManager.GetComponent<GeneralManager>().BasketCondition(transform.position);
        }
        if (other.gameObject.CompareTag("GameOver"))
        {
            _GameManager.GetComponent<GeneralManager>().GameLost();
        }
    }

    IEnumerator BasketConfirmationWaiter()
    {
        confirmationObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        confirmationObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        BallSound.Play();
    }
}
