using UnityEngine;

public class spinCoin : MonoBehaviour
{
    void Update(){
        transform.Rotate(0f, 0f, 50 * Time.deltaTime, Space.Self);
    }
}
