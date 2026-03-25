using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Loader
{
    public class LoadScreenView : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private Camera _loadingCamera;

        private bool _isLoading;

        public void Show()
        {
            _isLoading = true;
            _loadingCanvas.gameObject.SetActive(true);
            _loadingCamera.gameObject.SetActive(true);
        }

        public void Hide() 
        {
            _isLoading = false;
            _loadingCanvas.gameObject.SetActive(false);
            _loadingCamera.gameObject.SetActive(false);
        }

        public void SetProgress(float progress)
        {
            _progressBar.value = progress;
        }

        public void SetStatus(string status) 
        {
            _statusText.text = status;
        }

        public IProgress<float> CreateProgressReporter()
        {
            // Создаём прогресс, который будет обновлять UI
            return Progress.Create<float>(value =>
            {
                // Этот метод вызывается при каждом progress.Report()
                _progressBar.value = value;
                //_progressText.text = $"{value * 100:F0}%";
            });
        }
    }
}
