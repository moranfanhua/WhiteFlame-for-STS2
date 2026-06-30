using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Keywords;
using STS2RitsuLib.Models.Capabilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Capabilities;
using WhiteFlame.WhiteFlameCode.Cards.Keywords;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class RmIronRampart() : WhiteFlameCardTemplate(1, CardType.Skill, CardRarity.Common, TargetType.Self), IModelCapabilitySource
{
    public void BuildDefaultCapabilities(ModelCapabilityList capabilities)
    {
        capabilities.Add<RecallingMemoryCardPlayCapability>();
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(7, ValueProp.Move),
        new PowerVar<RecallingMemoryPower>(1m),
        new CalculationBaseVar(0m),
        new CalculationExtraVar(1m),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((CardModel card, Creature? _) => card.Owner.Creature.GetPowerAmount<RecallingMemoryPower>() / 5)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<RecallingMemoryPower>()
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        WhiteFlameKeywords.Recall.GetModCardKeyword()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        decimal block = ((CalculatedVar)DynamicVars["CalculatedBlock"]).Calculate(null);
        await CreatureCmd.GainBlock(base.Owner.Creature, block, ValueProp.Move, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4m);
    }
}
