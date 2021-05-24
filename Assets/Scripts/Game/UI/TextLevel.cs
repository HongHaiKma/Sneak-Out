using PFramework;

namespace Game
{
    public class TextLevel : TextBase
    {
        protected override string _strText => string.Format("LEVEL {0}", DataMain.LevelIndex + 1);
    }
}