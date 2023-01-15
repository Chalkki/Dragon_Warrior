using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    public int moneyAmount;
    public TextMeshProUGUI Cointext;
    // Start is called before the first frame update
    void Start()
    {
        moneyAmount= 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Pick up coin");
            Destroy(col.gameObject);
            SetMoney(moneyAmount += 1);
        }
    }

    public void SetMoney(int amount)
    {
        moneyAmount = amount;
        Cointext.text = moneyAmount.ToString();
    }
}
