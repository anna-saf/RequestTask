namespace UnityDev.Extensions
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Абстрактный класс кнопки
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonView : MonoBehaviour
    {
        protected Button _button = default;

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClicked);
        }

        /// <summary>
        /// Метод клика на комнату
        /// </summary>
        protected abstract void OnButtonClicked();

        protected virtual void OnDestroy() =>
            _button.onClick.RemoveListener(OnButtonClicked);
    }
}
