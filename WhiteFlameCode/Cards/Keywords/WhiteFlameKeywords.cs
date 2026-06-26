using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Content;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Keywords;

namespace WhiteFlame.WhiteFlameCode.Cards.Keywords;

[RegisterOwnedCardKeyword(nameof(Recall), IconPath = "res://WhiteFlame/Images/Keywords/recall.png", CardDescriptionPlacement = ModKeywordCardDescriptionPlacement.BeforeCardDescription)]
[RegisterOwnedCardKeyword(nameof(Immortal), IconPath = "res://WhiteFlame/Images/Keywords/immortal.png", CardDescriptionPlacement = ModKeywordCardDescriptionPlacement.AfterCardDescription)]
public class WhiteFlameKeywords
{
    public static readonly string Recall = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(Recall));
    public static readonly string Immortal = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(Immortal));
}
