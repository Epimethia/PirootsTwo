using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonScript : MonoBehaviour
{
    private Animator PopupAnimator;
    public Text PreviousScore;
    public Text CurrentScore;
    void Start()
    {
        PopupAnimator = GetComponent<Animator>();
    }
    public void Pause()
    {
        bool IsActive = PopupAnimator.GetBool("Active");
        PopupAnimator.SetBool("Active", !IsActive);

        PreviousScore.text = PlayerPrefs.GetInt("PreviousHighScore").ToString();
        CurrentScore.text = PlayerPrefs.GetInt("CurrentScore").ToString();
    }

    public void Continue()
    {
        PopupAnimator.SetBool("Active", false);
    }

    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
