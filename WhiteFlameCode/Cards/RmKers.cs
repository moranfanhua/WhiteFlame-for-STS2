using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Keywords;
using System.Collections.Generic;
using System.Threading.Tasks;
using STS2RitsuLib.Models.Capabilities;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Capabilities;
using WhiteFlame.WhiteFlameCode.Cards.Keywords;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class RmKers() : WhiteFlameCardTemplate(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy), IModelCapabilitySource
{
    public void BuildDefaultCapabilities(ModelCapabilityList capabilities)
    {
        capabilities.Add<RecallingMemoryCardPlayCapability>();
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(6, ValueProp.Unpowered | ValueProp.Move),
        new PowerVar<RecallingMemoryPower>(1m),
        new CalculationBaseVar(0m),
        new ExtraDamageVar(2m),
        new CalculatedDamageVar(ValueProp.Unpowered | ValueProp.Move).WithMultiplier((CardModel card, Creature? _) => card.Owner.Creature.GetPowerAmount<RecallingMemoryPower>() / 6)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<RecallingMemoryPower>()
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        WhiteFlameKeywords.Recall.GetModCardKeyword()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(DynamicVars.CalculatedDamage)
            .FromCard(this)
            .Targeting(cardPlay.Target!)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}
