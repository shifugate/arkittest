using ARKit.Manager.Setting;
using ARKit.Util;
using UnityEngine;

namespace ARKit.Helper.Anchor
{
    public class AnchorHelper : MonoBehaviour
    {
        [SerializeField]
        private Light ligth;

        private float size;

        private void Awake()
        {
            SetProperties();
            SetupRoom();
            SetupLight();
        }

        private void SetProperties()
        {
            size = SettingManager.Instance.DataCurrent.room_size / 100f;
        }

        private async void SetupRoom()
        {
            GameObject roof = await ContentUtil.LoadContent<GameObject>("Helper/Wall/WallHelper.prefab", transform);
            roof.name = "Roof";

            GameObject floor = await ContentUtil.LoadContent<GameObject>("Helper/Wall/WallHelper.prefab", transform);
            floor.name = "Floor";

            GameObject leftWall = await ContentUtil.LoadContent<GameObject>("Helper/Wall/WallHelper.prefab", transform);
            leftWall.name = "LeftWall";

            GameObject rightWall = await ContentUtil.LoadContent<GameObject>("Helper/Wall/WallHelper.prefab", transform);
            rightWall.name = "RightWall";

            GameObject frontWall = await ContentUtil.LoadContent<GameObject>("Helper/Wall/WallHelper.prefab", transform);
            frontWall.name = "FrontWall";

            GameObject backWall = await ContentUtil.LoadContent<GameObject>("Helper/Wall/WallHelper.prefab", transform);
            backWall.name = "BackWall";

            roof.transform.localScale = new Vector3(size, 0.001f, size);
            roof.transform.localPosition = new Vector3(0, size / 2, 0);

            floor.transform.localScale = new Vector3(size, 0.001f, size);
            floor.transform.localPosition = new Vector3(0, -size / 2, 0);

            leftWall.transform.localScale = new Vector3(size, size, 0.001f);
            leftWall.transform.localEulerAngles = new Vector3(0, 90, 0);
            leftWall.transform.localPosition = new Vector3(-size / 2, 0, 0);

            rightWall.transform.localScale = new Vector3(size, size, 0.001f);
            rightWall.transform.localEulerAngles = new Vector3(0, -90, 0);
            rightWall.transform.localPosition = new Vector3(size / 2, 0, 0);

            frontWall.transform.localScale = new Vector3(size, size, 0.001f);
            frontWall.transform.localPosition = new Vector3(0, 0, size / 2);

            backWall.transform.localScale = new Vector3(size, size, 0.001f);
            backWall.transform.localPosition = new Vector3(0, 0, -size / 2);

            EventUtil.Anchror.CreateRoomComplete?.Invoke();
        }

        private void SetupLight()
        {
            float intensity = size / 3 * size / 3;

            ligth.range = size * 100;
            ligth.intensity = intensity;
        }
    }
}
