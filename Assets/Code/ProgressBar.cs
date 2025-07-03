using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float MaxProgress = 100f;
    private Image bar;
    private float progress;

    private void Awake()
    {
        progress = 0;
        bar = GetComponent<Image>();
        UpdateBar(progress);
    }

    public void UpdateBar(float percent)
    {
        progress += percent;

        bar.fillAmount = progress / MaxProgress;

        if (progress >= MaxProgress)
        {
            PerderPartida();
        }
    }

    void PerderPartida()
    {
        SceneManager.LoadScene("EscenaDerrota");
    }
}
