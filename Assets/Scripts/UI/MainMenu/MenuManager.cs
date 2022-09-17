using System.Collections;
using UI.MainMenu.HighScoreView;
using UnityEngine;
// ReSharper disable All

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        public static bool GameIsPaused;

        [SerializeField]
        private GameObject pauseMenuUI;

        [SerializeField]
        private GameObject HighScoreWindow;
        
        private void Start()
        {
            pauseMenuUI.SetActive(false);
            HighScoreWindow.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            PauseMenu();
        }
        
        public void PauseMenu()
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
        
        public void ShowHighScores()
        {
            HighScoreWindow.GetComponentInChildren<HighScoresList>().UpdateList();
            pauseMenuUI.SetActive(false);
            HighScoreWindow.SetActive(true);
        }

        public void Resume()
        {
            HighScoreWindow.SetActive(false);
            StartCoroutine(ResumeRoutine());
        }
        
        private IEnumerator ResumeRoutine()
        {
            pauseMenuUI.SetActive(false);
            GameIsPaused = false;
            
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(0.2f);
            Time.timeScale = 0.7f;
            yield return new WaitForSeconds(0.12f);
            Time.timeScale = 0.9f;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 1;
        }

        public void Pause()
        {
            HighScoreWindow.SetActive(false);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void QuitGame()
        {
            Debug.Log("Quiting");
            Application.Quit();
        }
    }
}
