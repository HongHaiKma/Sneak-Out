using PFramework;

namespace Game
{
    public class ButtonRating : ButtonRate
    {
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            DataMain.RateGame = true;
        }
    }
}