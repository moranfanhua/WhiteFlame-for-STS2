using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace WhiteFlame.WhiteFlameCode.Abstracts;

[RegisterCard(typeof(TokenCardPool), Inherit = true)]
public abstract class WhiteFlameTokenCardTemplate(int cost, CardType type, CardRarity rarity, TargetType target)
    : ModCardTemplate(cost, type, rarity, target)
{
    public override CardAssetProfile AssetProfile => new(
        PortraitPath: $"res://WhiteFlame/Images/Cards/{GetType().Name}.png"
    );
}
