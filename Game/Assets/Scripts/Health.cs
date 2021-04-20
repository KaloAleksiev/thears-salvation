using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

[System.Serializable]
public class SliderChangeEvent : UnityEvent<bool> { }
public class Health : MonoBehaviour {
    public double maxHealth;
    public double currentHealth;
    public UnityEvent setMaxHealth;
    public UnityEvent setCurrentHealth;
    public SliderChangeEvent sliderChange;
    public Animator unitAnimator;
    public Canvas receivedDamageCanvas;

    public UnityEvent flipHealthBarRight;
    public UnityEvent flipHealthBarLeft;
    public UnityEvent flipReceivedDamageCanvasRight;
    public UnityEvent flipReceivedDamageCanvasLeft;

    [SerializeField] private TextMeshProUGUI receivedDamageText;

    private Vector3 receivedDamageCanvasLocalScale;

    private void Start() {
        currentHealth = maxHealth;
        setMaxHealth.Invoke();
        setCurrentHealth.Invoke();

        flipReceivedDamageCanvasRight.AddListener(FlipReceivedDamageCanvasRight);
        flipReceivedDamageCanvasLeft.AddListener(FlipReceivedDamageCanvasLeft);

        receivedDamageCanvasLocalScale = receivedDamageCanvas.transform.localScale;
    }

    private void OnDestroy() {
        flipReceivedDamageCanvasRight.RemoveListener(FlipReceivedDamageCanvasRight);
        flipReceivedDamageCanvasLeft.RemoveListener(FlipReceivedDamageCanvasLeft);
    }

    public void TakeDamage(double damage) {
        currentHealth -= damage;
        unitAnimator.SetTrigger("Hurt");
        StartCoroutine(ShowDamageText(damage));
        if (currentHealth < 0) currentHealth = 0;
        setCurrentHealth.Invoke();
    }

    private IEnumerator ShowDamageText(double damage) {
        TextMeshProUGUI text = Instantiate(receivedDamageText, receivedDamageCanvas.transform);
        text.transform.SetParent(receivedDamageCanvas.transform, false);

        text.text = "-" + damage.ToString();

        Animator textAnimator = text.GetComponent<Animator>();
        textAnimator.SetTrigger("ReceiveDamageUI");

        yield return new WaitForSeconds(1f);

        Destroy(text.gameObject);
    }

    private void FlipReceivedDamageCanvasRight() {
        receivedDamageCanvas.transform.localScale = new Vector3(
            receivedDamageCanvasLocalScale.x,
            receivedDamageCanvasLocalScale.y, 
            receivedDamageCanvasLocalScale.z);
    }

    private void FlipReceivedDamageCanvasLeft() {
        receivedDamageCanvas.transform.localScale = new Vector3(
            -receivedDamageCanvasLocalScale.x, 
            receivedDamageCanvasLocalScale.y, 
            receivedDamageCanvasLocalScale.z);
    }
}
