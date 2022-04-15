using UnityEngine;
using ARKit.MVC._Base;
using ARKit.Util;
using ARKit.UI._Screen.Setting;

namespace ARKit.MVC.Home
{
    public class HomeController : ControllerBase<HomeView, HomeModel>
    {
        private bool loading;

        private void Awake()
        {
            AddListener();
        }

        private void OnDestroy()
        {
            RemoveListener();
        }

        private void Start()
        {
            LoadScreen(ContentUtil.Constant.Screen.Setting);
        }

        private void AddListener()
        {
            EventUtil.Screen.LoadScreen += LoadScreen;
        }

        private void RemoveListener()
        {
            EventUtil.Screen.LoadScreen -= LoadScreen;
        }

        private void RemoveContent()
        {
            foreach (Transform transform in Model.SpaceHolder)
                Destroy(transform.gameObject);

            foreach (Transform transform in Model.UIHolder)
                Destroy(transform.gameObject);
        }

        private async void LoadScreen(ContentUtil.Constant.Screen screen)
        {
            if (loading)
                return;

            loading = true;

            RemoveContent();

            switch (screen)
            {
                case ContentUtil.Constant.Screen.Setting:
                    await ContentUtil.LoadContent<SettingUI>("UI/_Screen/Setting/SettingUI.prefab", Model.UIHolder);
                    break;
            }

            loading = false;
        }
    }
}
