using UnityEngine;
using TMPro;

public class CoinCollection : MonoBehaviour
{
   private int Coin = 0;

   public TextMeshProUGUI coinText;

   private void OnTriggerEnter(Collider other){
       if (other.transform.tag == "coin"){
           Coin++;
           coinText.text = "Coins: " + Coin.ToString();
           Destroy(other.gameObject);
       }
   }
}
