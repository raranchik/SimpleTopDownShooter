namespace Core.Base.RandomExt
{
    public interface IRandom
    {
        float Range(float minInclusive, float maxInclusive);
        int Range(int minInclusive, int maxInclusive);
        float NextFloat();
    }
}