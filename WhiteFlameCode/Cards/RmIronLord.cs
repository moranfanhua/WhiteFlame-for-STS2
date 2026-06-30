using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2RitsuLib.Keywords;
using STS2RitsuLib.Models.Capabilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Capabilities;
using WhiteFlame.WhiteFlameCode.Cards.Keywords;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class RmIronLord() : WhiteFlameCardTemplate(3, CardType.Power, CardRarity.Uncommon, TargetType.Self), IModelCapabilitySource
{
    public void BuildDefaultCapabilities(ModelCapabilityList capabilities)
    {
        capabilities.Add<RecallingMemoryCardPlayCapability>();
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<RecallingMemoryPower>(1m),
        new CalculationBaseVar(15m),
        new CalculationExtraVar(1m),
        new CalculatedVar("GetPlatingPower").WithMultiplier((CardModel card, Creature _) => card.Owner.Creature.GetPowerAmount<RecallingMemoryPower>())
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<PlatingPower>(),
        HoverTipFactory.FromPower<RecallingMemoryPower>()
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [WhiteFlameKeywords.Recall.GetModCardKeyword()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<PlatingPower>(choiceContext, base.Owner.Creature, this.DynamicVars["GetPlatingPower"].PreviewValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["GetPlatingPower"].UpgradeValueBy(3m);
    }
}
