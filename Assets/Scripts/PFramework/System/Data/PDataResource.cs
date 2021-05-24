using UnityEngine;

namespace PFramework
{
    public class PDataResource : PDataBlock<PDataResource>
    {
        [SerializeField] PData<int> _resourceCoin;
        [SerializeField] PData<int> _resourceGem;
        [SerializeField] PData<int> _resourceEnergy;

        protected override void Init()
        {
            base.Init();

            _resourceCoin = _resourceCoin ?? new PData<int>(0);
            _resourceGem = _resourceGem ?? new PData<int>(0);
            _resourceEnergy = _resourceEnergy ?? new PData<int>(0);
        }

        public static PData<int> GetResourceData(PResourceType type)
        {
            switch (type)
            {
                case PResourceType.Coin:
                    return Instance._resourceCoin;
                case PResourceType.Gem:
                    return Instance._resourceGem;
                case PResourceType.Energy:
                    return Instance._resourceEnergy;
            }

            return null;
        }
    }
}