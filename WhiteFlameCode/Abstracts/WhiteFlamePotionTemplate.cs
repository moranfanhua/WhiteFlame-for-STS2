using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using WhiteFlame.WhiteFlameCode.Pools;

namespace WhiteFlame.WhiteFlameCode.Abstracts;

[RegisterPotion(typeof(WhiteFlamePotionPool), Inherit = true)]
public abstract class WhiteFlamePotionTemplate : ModPotionTemplate
{
    public override PotionAssetProfile AssetProfile => new(
        ImagePath: $"res://WhiteFlame/Images/Potions/{GetType().Name}.png",
        OutlinePath: $"res://WhiteFlame/Images/Potions/{GetType().Name}.png"
    );
}
