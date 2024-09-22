using UnityRandom = UnityEngine.Random;

namespace Core.Base.RandomExt
{
    public class UnityRandomDecorator : IRandom
    {
        public float Range(float minInclusive, float maxInclusive)
        {
            return UnityRandom.Range(minInclusive, maxInclusive);
        }

        public int Range(int minInclusive, int maxInclusive)
        {
            return UnityRandom.Range(minInclusive, maxInclusive);
        }

        public float NextFloat()
        {
            return UnityRandom.value;
        }
    }
}