using ARKit.Manager.Language;
using ARKit.Util;
using UnityEngine;
using UnityEngine.UI;

namespace ARKit.UI._Screen.Anchor
{
    public class AnchorUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform interactionHolder;
        [SerializeField]
        private Text interactionText;

        private void Awake()
        {
            SetInteractionText(LanguageManager.Instance.GetTranslation("common", "add_interaction_token"));
        }

        private void Update()
        {
            UpdateInput();
        }

        private void SetInteractionText(string anchorMessage = null)
        {
            string interactionText = $"{LanguageManager.Instance.GetTranslation("common", "move_interaction_token")}\n" +
                $"{LanguageManager.Instance.GetTranslation("common", "look_interaction_token")}\n" +
                $"{LanguageManager.Instance.GetTranslation("common", "exit_interaction_token")}";

            if (anchorMessage != null)
                interactionText += $"\n{anchorMessage}";

            this.interactionText.text = interactionText;
        }

        private void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.P))
                EventUtil.Screen.LoadScreen?.Invoke(ContentUtil.Constant.Screen.Setting);
        }
    }
}
