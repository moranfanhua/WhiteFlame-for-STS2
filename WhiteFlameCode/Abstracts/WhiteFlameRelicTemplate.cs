using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using WhiteFlame.WhiteFlameCode.Pools;

namespace WhiteFlame.WhiteFlameCode.Abstracts;

[RegisterRelic(typeof(WhiteFlameRelicPool), Inherit = true)]
public abstract class WhiteFlameRelicTemplate : ModRelicTemplate
{
    public override RelicAssetProfile AssetProfile => new(
        IconPath: $"res://WhiteFlame/Images/Relics/{GetType().Name}.png",
        IconOutlinePath: $"res://WhiteFlame/Images/Relics/{GetType().Name}.png",
        BigIconPath: $"res://WhiteFlame/Images/Relics/{GetType().Name}.png"
    );
}
