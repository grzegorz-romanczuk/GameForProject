using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinDrop()
    {
        Vector3 position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        Instantiate(coin, position, Quaternion.identity);
        coin.SetActive(true);

    }
}
