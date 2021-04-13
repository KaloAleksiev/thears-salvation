using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour {
    public Player player;
    public Image swordImage;
    private bool allowPickUp = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            allowPickUp = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            allowPickUp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Collectible collectible = collider.GetComponent<Collectible>();

        if (collectible) {
            player.addSoulOrbs.Invoke(collectible.worth);
            Destroy(collider.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        PickUp pickUp = collider.GetComponent<PickUp>();
        if (pickUp && Input.GetKeyDown(KeyCode.F))
        {
            player.playerData.ActiveSword = pickUp.sword;
            swordImage.sprite = pickUp.sword.Image;
            Destroy(collider.gameObject);
        }
    }


}
