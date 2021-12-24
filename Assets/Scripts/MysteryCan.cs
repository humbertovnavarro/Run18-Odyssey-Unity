using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii {
    public class MysteryCan : MonoBehaviour, IInteractable 
    {
        public Rigidbody2D rb;
        public void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void OnInteraction()
        {
            GameObject.Instantiate(this);
            float x = Random.Range(-200, 200);
            float y = Random.Range(-200, 200f);
            Vector2 velocity = new Vector2(x, y);
            rb.AddForce(velocity);
            Debug.Log("HI");
        }
    }
}

