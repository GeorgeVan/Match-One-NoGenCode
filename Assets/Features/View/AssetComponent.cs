using Entitas;
using Entitas.Generic;

public sealed class AssetG : IComponent, ICompData, Scope<GameScope>, ICopyFrom<AssetG>, ICreateApply
{
    public string value;

    public void CopyFrom(AssetG other)
    {
        value = other.value;
    }

    public AssetG Set(string v)
    {
        value = v;
        return this;
    }
}
