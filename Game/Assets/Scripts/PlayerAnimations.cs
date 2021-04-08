using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    public Player player;
    public Animator animator;

    void Start() {
        player.playDeathAnim.AddListener(PlayDeathAnimation);
        player.playRecoverAnim.AddListener(PlayRecoverAnimation);
    }

    private void OnDestroy() {
        player.playDeathAnim.RemoveListener(PlayDeathAnimation);
        player.playRecoverAnim.RemoveListener(PlayRecoverAnimation);
    }

    private void PlayDeathAnimation() {
        animator.SetTrigger("FallDown");
        animator.SetTrigger("Death");
    }

    private void PlayRecoverAnimation() {
        animator.SetTrigger("Recover");
    }
}
