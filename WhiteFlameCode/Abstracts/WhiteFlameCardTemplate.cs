using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using WhiteFlame.WhiteFlameCode.Pools;

namespace WhiteFlame.WhiteFlameCode.Abstracts;

[RegisterCard(typeof(WhiteFlameCardPool), Inherit = true)]
public abstract class WhiteFlameCardTemplate(int cost, CardType type, CardRarity rarity, TargetType target)
    : ModCardTemplate(cost, type, rarity, target)
{
    public override CardAssetProfile AssetProfile => new(
        PortraitPath: $"res://WhiteFlame/Images/Cards/{GetType().Name}.png"
    );
}
