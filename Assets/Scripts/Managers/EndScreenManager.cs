using System;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private Text highScore;
    [SerializeField] private Button button;

    public static event Action OnButtonClicked;

    private void Awake()
    {
        highScore.enabled = false;
        button.enabled = false;

        button.onClick.AddListener(Hide);
    }

    public void Show()
    {
        highScore.enabled = true;
        button.enabled = true;

        highScore.text = $"HighScore:\n{GameManager.HighScore.ToString()}";
    }

    private void Hide()
    {
        button.gameObject.SetActive(false);
        highScore.enabled = false;
        OnButtonClicked?.Invoke();
    }
}
