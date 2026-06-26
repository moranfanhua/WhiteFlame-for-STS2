using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace WhiteFlame.WhiteFlameCode.Abstracts;

[RegisterPower(Inherit = true)]
public abstract class WhiteFlamePowerTemplate : ModPowerTemplate
{
    public override PowerAssetProfile AssetProfile => new(
        IconPath: $"res://WhiteFlame/Images/Powers/{GetType().Name}.png",
        BigIconPath: $"res://WhiteFlame/Images/Powers/{GetType().Name}.png"
    );
}
