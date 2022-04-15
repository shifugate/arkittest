using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using ARKit.Data.Setting;
using ARKit.Util;

namespace ARKit.Manager.Setting
{
    public class SettingManager : MonoBehaviour
    {
        #region Singleton
        private static SettingManager instance;
        public static SettingManager Instance { get { return instance; } }

        public static IEnumerator InstanceCR(InitializerManager manager)
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SettingManager");

                instance = go.AddComponent<SettingManager>();
            }

            yield return instance.InitializeCR(manager);
        }
        #endregion

        private SettingData data;
        public SettingData Data { get { return data; } }

        private string path;

        private IEnumerator InitializeCR(InitializerManager manager)
        {
            transform.SetParent(manager.transform);

            yield return SetPropertiesCR();
        }

        private IEnumerator SetPropertiesCR()
        {
            path = $"{Application.persistentDataPath}/setting.json";

            string settings = null;

            bool complete = false;

            new Thread(() =>
            {
                try
                {
                    if (File.Exists(path))
                    {
                        settings = File.ReadAllText(path);

                        data = JsonConvert.DeserializeObject<SettingData>(settings);
                    }
                }
                catch
                {

                }
            }).Start();

            yield return new WaitUntil(() => complete);

            if (data != null)
                yield break;

            ResourceRequest request = Resources.LoadAsync<TextAsset>("Manager/Setting/setting");

            yield return request;

            settings = ((TextAsset)request.asset).text;

            data = JsonConvert.DeserializeObject<SettingData>(settings);

            new Thread(() => File.WriteAllText(path, settings)).Start();
        }

        public async void SaveAction()
        {
            bool complete = false;

            new Thread(() =>
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));

                complete = true;
            }).Start();

            while (!complete)
                await Task.Yield();

            EventUtil.Setting.SaveComplete?.Invoke();
        }
    }
}
