using System;
using System.Collections;
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
        private GameObject HighScoreContent;
        
        [SerializeField]
        private GameObject HighScoreView;
        
        private void Start()
        {
            pauseMenuUI.SetActive(false);
            HighScoreView.SetActive(false);
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
            HighScoreContent = Instantiate(HighScoreContent, HighScoreView.transform);
            pauseMenuUI.SetActive(false);
            HighScoreView.SetActive(true);
        }
        
        public void HideHighScores()
        {
            HighScoreView.SetActive(false);
            pauseMenuUI.SetActive(true);
            Destroy(HighScoreContent);
        }
        
        public void Resume()
        {
            StartCoroutine(ResumeRoutine());
        }
        private IEnumerator ResumeRoutine()
        {
            pauseMenuUI.SetActive(false);
            
            Time.timeScale = 0.2f;
            yield return new WaitForSeconds(0.06f);
            Time.timeScale = 1;
            
            GameIsPaused = false;
        }

        public void Pause()
        {
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
