using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerData : ScriptableObject {
    public int soulOrbs;

    public void AddSoulOrbs(int amount) {
        this.soulOrbs += amount;
    }
}
