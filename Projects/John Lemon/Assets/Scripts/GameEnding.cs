using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1.0f;
    [SerializeField] float displayImageDuration = 1.0f;
    [SerializeField] GameObject player;
    [SerializeField] CanvasGroup exitBackgroundImageCanvasGroup, caughtBackgroundCanvasGroup;

    private bool isPlayerAtExit, isPlayerCaught;
    private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        } 
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundCanvasGroup, true);
        }
    }

    /// <summary>
    /// Lancia l'immagine di fine partita
    /// </summary>
    /// <param name="imageCanvasGroup">Immagine di fine partite corrispondente</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
}
