using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    public Player player;
    public Animator animator;

    void Start() {
        player.playJumpAnimation.AddListener(PlayJumpAnimation);
        player.playAttackAnimation.AddListener(PlayAttackAnimation);
        player.playHurtAnimation.AddListener(PlayHurtAnimation);
        player.playDeathAnimation.AddListener(PlayDeathAnimation);
        player.playRecoverAnimation.AddListener(PlayRecoverAnimation);

        player.setIntegerAnimator.AddListener(SetInteger);
        player.setFloatAnimator.AddListener(SetFloat);
        player.setBoolAnimator.AddListener(SetBool);
    }

    private void OnDestroy() {
        player.playJumpAnimation.RemoveListener(PlayJumpAnimation);
        player.playAttackAnimation.RemoveListener(PlayAttackAnimation);
        player.playHurtAnimation.RemoveListener(PlayHurtAnimation);
        player.playDeathAnimation.RemoveListener(PlayDeathAnimation);
        player.playRecoverAnimation.RemoveListener(PlayRecoverAnimation);

        player.setIntegerAnimator.RemoveListener(SetInteger);
        player.setFloatAnimator.RemoveListener(SetFloat);
        player.setBoolAnimator.RemoveListener(SetBool);
    }

    private void PlayJumpAnimation() {
        animator.SetTrigger("Jump");
    }

    private void PlayAttackAnimation() {
        animator.SetTrigger("Attack");
    }

    private void PlayHurtAnimation() {
        animator.SetTrigger("Hurt");
    }

    private void PlayDeathAnimation() {
        animator.SetTrigger("FallDown");
        animator.SetTrigger("Death");
    }

    private void PlayRecoverAnimation() {
        animator.SetTrigger("Recover");
    }

    private void SetInteger(string name, int value) {
        animator.SetInteger(name, value);
    }

    private void SetFloat(string name, float value) {
        animator.SetFloat(name, value);
    }

    private void SetBool(string name, bool value) {
        animator.SetBool(name, value);
    }
}
