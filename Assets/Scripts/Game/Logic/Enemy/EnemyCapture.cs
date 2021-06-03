using PFramework;

namespace Game
{
    public class EnemyCapture : CacheMonoBehaviour, IStateMachine
    {
        CharacterScript _characterScript;

        void Awake()
        {
            _characterScript = GetComponent<CharacterScript>();
        }

        void IStateMachine.Init()
        {

        }

        void IStateMachine.OnStart()
        {
            CacheTransform.forward = PlayerScript.Instance.Position - CacheTransform.position;

            PlayerScript.Instance.CacheTransform.LookAt(CacheTransform.position);

            // _characterScript.gameObject.GetComponent<Tran>

            _characterScript.PlayCapture();

            PlayerScript.Instance.Capture(CacheTransform.position);

            Messenger.Broadcast(GameEvent.Player_Captured);
        }

        void IStateMachine.OnStop()
        {
        }

        void IStateMachine.OnUpdate()
        {
        }
    }
}