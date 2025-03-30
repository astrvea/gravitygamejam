using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Stars;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    void Start()
    {
        setTokens();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTokens() {
        var all = 0;
        for (int i = 1; i < Stars.Count; ++i) {
            var egg = Random.Range(0, 1);
            if (Random.Range(0, 2) == 1) {Stars[i].SetActive(true);}
            else {Stars[i].SetActive(false); all += 1;}
            if (all == 4) {
                break;
            }
        } 
    }
}
