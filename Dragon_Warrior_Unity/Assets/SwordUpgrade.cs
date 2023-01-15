using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUpgrade : MonoBehaviour
{
    private int Charge;
    private void Start()
    {
    }
    public void upgrade(GameObject caller)
    {
        GameObject Player = GameObject.Find("PlayerSword");
        int SwordLevel = Player.GetComponent<Player_Sword_Attack>().GetSwordLevel();
        Charge = 20*(SwordLevel-1) + 10;
        if (Player.GetComponent<PlayerEconomy>().moneyAmount - Charge < 0)
        {
            string prefix = "Your Sword Level is " + SwordLevel + ". I need " + Charge + " coins to upgrade it.";
            caller.GetComponent<ButtonResponse>().MakeChoice(prefix, 0, 0);
        }
        else
        {
            string suffix = " The level is " + SwordLevel +".";
            Player.GetComponent<PlayerEconomy>().SetMoney(Player.GetComponent<PlayerEconomy>().moneyAmount - Charge);
            Debug.Log("Successfully upgrade!");
            Player.GetComponent<Player_Sword_Attack>().SetSwordLevel(SwordLevel+1);
            caller.GetComponent<ButtonResponse>().MakeChoice(1,0,suffix);
        }
    }

}
