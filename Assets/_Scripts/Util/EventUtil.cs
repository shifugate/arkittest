using System;

namespace ARKit.Util
{
    public static class EventUtil
    {
        public static class Screen
        {
            public static Action<ContentUtil.Constant.Screen> LoadScreen;
        }

        public static class Setting 
        {
            public static Action SaveComplete;
        }

        public static class Anchror
        {
            public static Action CreateRoomComplete;
        }
    }
}
