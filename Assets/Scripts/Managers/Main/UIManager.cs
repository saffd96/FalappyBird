using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [SerializeField] private EndScreenManager endScreen;

    [SerializeField] private CanvasGroup endSceneCanvasGroup;
    [SerializeField] private CanvasGroup mainPanelCanvasGroup;
    
    private void Awake()
    {
        scoreText.text = "0";

        mainPanelCanvasGroup.gameObject.SetActive(true);
        mainPanelCanvasGroup.alpha = 1;
        
        endScreen.gameObject.SetActive(false);
        endSceneCanvasGroup.alpha = 0;

        mainPanelCanvasGroup.DOFade(0, 1f);
    }

    private void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScore;
        Bird.OnPipeCollision += ShowEndScreen;
        EndScreenManager.OnButtonClicked += HideEndScreen;
    }
    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScore;
        Bird.OnPipeCollision -= ShowEndScreen;
        EndScreenManager.OnButtonClicked -= HideEndScreen;
    }

    private void UpdateScore(int scoreAmount)
    {
        scoreText.text = scoreAmount.ToString();
    }

    private void ShowEndScreen()
    {
        endScreen.gameObject.SetActive(true);
        endSceneCanvasGroup.DOFade(1, 0.1f).OnComplete(endScreen.Show);
    }
    
    private void HideEndScreen()
    {
        scoreText.enabled = false;
        mainPanelCanvasGroup.DOFade(1, 1f).OnComplete(SceneLoadManager.ReloadScene);
    }
}
