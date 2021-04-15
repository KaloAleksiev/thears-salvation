using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour {
    public Player player;
    public Image swordImage;

    private Collider2D swordCollider;

    private void Start()
    {
        swordImage.sprite = player.playerData.ActiveSword.Image;
    }

    public void Update()
    {
        if (swordCollider && Input.GetKeyDown(KeyCode.F))
        {
            PickUp pickUp = swordCollider.GetComponent<PickUp>();
            if (pickUp)
            {
                Sword oldSword = player.playerData.ActiveSword;
                player.playerData.ActiveSword = pickUp.sword;
                swordImage.sprite = pickUp.sword.Image;
                pickUp.sword = oldSword;
                pickUp.GetComponent<SpriteRenderer>().sprite = oldSword.Image;
                player.playSwordPickUpSound.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Collectible collectible = collider.GetComponent<Collectible>();

        if (collectible) {
            player.addSoulOrbs.Invoke(collectible.soulOrb.Worth);
            Destroy(collider.gameObject);
            AudioSource.PlayClipAtPoint(collectible.soulOrb.PickupSound, transform.position);
        }

        PickUp pickUp = collider.GetComponent<PickUp>();
        if (pickUp)
        {
            swordCollider = collider;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PickUp pickUp = collider.GetComponent<PickUp>();
        if (pickUp)
        {
            swordCollider = null;
        }
    }
}
