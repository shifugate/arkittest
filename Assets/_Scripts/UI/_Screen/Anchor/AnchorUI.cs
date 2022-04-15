using ARKit.Helper.Camera;
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

        private CameraHelper cameraHelper;
        private bool createRoomComplete;

        private void Awake()
        {
            AddListener();
            SetProperties();
        }

        private void OnDestroy()
        {
            RemoveListener();

            cameraHelper.transform.position = Vector3.zero;
            cameraHelper.transform.rotation = Quaternion.identity;

            Destroy(cameraHelper);

            Cursor.visible = true;
        }

        private void Update()
        {
            UpdateInput();
        }

        private void AddListener()
        {
            EventUtil.Anchror.CreateRoomComplete += CreateRoomComplete;
        }

        private void RemoveListener() 
        {
            EventUtil.Anchror.CreateRoomComplete -= CreateRoomComplete;
        }

        private void SetProperties()
        {
            cameraHelper = Camera.main.gameObject.AddComponent<CameraHelper>();

            Cursor.visible = false;
        }

        private void CreateRoomComplete()
        {
            createRoomComplete = true;

            SetInteractionText(LanguageManager.Instance.GetTranslation("common", "add_interaction_token"));
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
            if (!createRoomComplete)
                return;

            if (Input.GetKeyDown(KeyCode.P))
                EventUtil.Screen.LoadScreen?.Invoke(ContentUtil.Constant.Screen.Setting);
        }
    }
}
