namespace Game
{
    public static class GameHelper
    {
        public static int TotalLevel { get { return PrefabFactory.ObjLevels.Count; } }

        public static TutorialConfig GetCurrentTutorialConfig()
        {
            for (int i = 0; i < GameConfig.TutorialConfigs.Count; i++)
            {
                if (GameConfig.TutorialConfigs[i].LevelIndex == DataMain.LevelIndex)
                    return GameConfig.TutorialConfigs[i];
            }

            return null;
        }
    }
}