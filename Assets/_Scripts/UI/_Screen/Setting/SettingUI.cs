using ARKit.Helper.UI;
using ARKit.Manager.Language.Token;
using ARKit.Manager.Setting;
using ARKit.Manager.System;
using ARKit.Util;
using UnityEngine;
using UnityEngine.UI;

namespace ARKit.UI._Screen.Setting
{
    public class SettingUI : MonoBehaviour
    {
        [SerializeField]
        private InputFieldHelper sizeInputField;
        [SerializeField]
        private InputFieldHelper anchorReachInputField;
        [SerializeField]
        private Toggle fpsToggle;

        private void Awake()
        {
            AddListener();
        }

        private void OnDestroy()
        {
            RemoveListener();
        }

        private void AddListener()
        {
            EventUtil.Setting.SaveComplete += SaveComplete;
        }

        private void RemoveListener()
        {
            EventUtil.Setting.SaveComplete -= SaveComplete;
        }

        public void FPSToggleAction(bool enable)
        {
            SystemManager.Instance.FPSEnableAction(enable);
        }

        private void SaveComplete()
        {
            EventUtil.Screen.LoadScreen?.Invoke(ContentUtil.Constant.Screen.Anchor);
        }

        private bool ValidForm()
        {
            bool valid = true;

            sizeInputField.SetError(null);
            anchorReachInputField.SetError(null);

            if (!int.TryParse(sizeInputField.text, out int size))
            {
                sizeInputField.SetError(LanguageManagerToken.common.invalid_size_token);

                valid = false;
            }

            if (!int.TryParse(anchorReachInputField.text, out int anchorReach))
            {
                anchorReachInputField.SetError(LanguageManagerToken.common.invalid_anchor_reach_token);

                valid = false;
            }

            return valid;
        }

        public void StartAction()
        {
            if (!ValidForm())
                return;

            SettingManager.Instance.Data.anchor_reach = int.Parse(sizeInputField.text);
        }
    }
}
