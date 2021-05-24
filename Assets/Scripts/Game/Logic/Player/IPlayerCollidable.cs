namespace Game
{
    public interface IPlayerCollidable
    {
        void OnTriggerEnter(PlayerScript player);
        void OnTriggerExit(PlayerScript player);
    }
}