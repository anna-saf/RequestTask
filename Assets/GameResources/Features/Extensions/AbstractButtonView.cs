namespace UnityDev.Extensions
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ����������� ����� ������
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

        protected abstract void OnButtonClicked();

        protected virtual void OnDestroy() =>
            _button.onClick.RemoveListener(OnButtonClicked);
    }
}
