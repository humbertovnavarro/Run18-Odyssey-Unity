using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii
{
    public class DroppedItem : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip pickupSound;
        [SerializeField]
        private ItemID itemID;
        private void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
            this.GetComponentInChildren<SpriteRenderer>().sprite = Items.GetSprite(itemID);
        }
        public ItemID Pickup()
        {
            audioSource.PlayOneShot(pickupSound);
            Object.Destroy(this.gameObject);
            return itemID;
        }
    }
}

