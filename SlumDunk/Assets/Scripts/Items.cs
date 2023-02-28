using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Items : MonoBehaviour
{
    [SerializeField] private GameObject _GeneralManager;
    [SerializeField] private TextMeshProUGUI countBack;
    int _countBack;
    

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        _GeneralManager.GetComponent<GeneralManager>().BasketScaleUp();
        _GeneralManager.GetComponent<GeneralManager>().Effects[0].transform.position = gameObject.transform.position;
        _GeneralManager.GetComponent<GeneralManager>().Effects[0].gameObject.SetActive(true);
    }

    public IEnumerator CountBack() 
    {
        _countBack = 10;
        countBack.text = _countBack.ToString();
        while (true)
        {          
            yield return new WaitForSeconds(1f);            
            if (_countBack!=1)
            {                
                _countBack--;
                countBack.text = _countBack.ToString();
            }
            else
            {
                gameObject.SetActive(false);                
                break;
            }            
        }        
    }
    public void Spawner() 
    {
        StartCoroutine(CountBack());
    }    

}
