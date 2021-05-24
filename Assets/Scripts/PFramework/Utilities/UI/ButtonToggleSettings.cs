using UnityEngine;

namespace PFramework
{
    public class ButtonToggleSettings : ButtonBase
    {
        [System.Serializable]
        public enum Type
        {
            Audio,
            Music,
            Sound,
            Vibration,
        }

        [Header("Config")]
        [SerializeField] Type _type;
        [SerializeField] GameObject _objOn;
        [SerializeField] GameObject _objOff;

        bool IsOn
        {
            get
            {
                switch (_type)
                {
                    case Type.Audio:
                        return PDataSettings.MusicEnabled && PDataSettings.SoundEnabled;
                    case Type.Music:
                        return PDataSettings.MusicEnabled;
                    case Type.Sound:
                        return PDataSettings.SoundEnabled;
                    case Type.Vibration:
                        return PDataSettings.VibrationEnabled;
                    default:
                        return false;
                }
            }
            set
            {
                switch (_type)
                {
                    case Type.Audio:
                        PDataSettings.MusicEnabled = value;
                        PDataSettings.SoundEnabled = value;
                        break;
                    case Type.Music:
                        PDataSettings.MusicEnabled = value;
                        break;
                    case Type.Sound:
                        PDataSettings.SoundEnabled = value;
                        break;
                    case Type.Vibration:
                        PDataSettings.VibrationEnabled = value;
                        break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            UpdateUI();
        }

        protected void UpdateUI()
        {
            _objOn.SetActive(IsOn);
            _objOff.SetActive(!IsOn);
        }

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            IsOn = !IsOn;

            UpdateUI();
        }
    }
}