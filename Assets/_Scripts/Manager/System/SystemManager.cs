﻿using ARKit.Manager.Setting;
using ARKit.Manager.System.Support;
using UnityEngine;

namespace ARKit.Manager.System
{
    public class SystemManager : MonoBehaviour
    {
        #region Singleton
        private static SystemManager instance;
        public static SystemManager Instance { get { return instance; } }

        public static void InstanceNW(InitializerManager manager)
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SystemManager");

                instance = go.AddComponent<SystemManager>();
            }

            instance.Initialize(manager);
        }
        #endregion

        private SystemManagerFPSDisplaySupport systemManagerFPSDisplaySupport;

        private void Initialize(InitializerManager manager)
        {
            transform.SetParent(manager.transform);

            SetProperties();
        }

        private void SetProperties()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.brightness = 1;

            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = true;

            FPSEnableAction(SettingManager.Instance.Data.show_fps);
        }

        public void FPSEnableAction(bool enable)
        {
            if (enable && !systemManagerFPSDisplaySupport)
                systemManagerFPSDisplaySupport = gameObject.AddComponent<SystemManagerFPSDisplaySupport>();
            else if (!enable && systemManagerFPSDisplaySupport)
                Destroy(systemManagerFPSDisplaySupport);
        }
    }
}
