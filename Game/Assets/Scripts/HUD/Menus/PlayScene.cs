using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
