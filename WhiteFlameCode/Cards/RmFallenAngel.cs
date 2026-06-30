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

public class RmFallenAngel() : WhiteFlameCardTemplate(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self), IModelCapabilitySource
{
    public void BuildDefaultCapabilities(ModelCapabilityList capabilities)
    {
        capabilities.Add<RecallingMemoryCardPlayCapability>();
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<RecallingMemoryPower>(1m),
        new CalculationBaseVar(0m),
        new CalculationExtraVar(1m),
        new CalculatedVar("BonusDexterity").WithMultiplier((CardModel card, Creature _) => card.Owner.Creature.GetPowerAmount<RecallingMemoryPower>() / 10)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DexterityPower>(),
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust,
        WhiteFlameKeywords.Recall.GetModCardKeyword()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int strengthAmount = base.Owner.Creature.GetPowerAmount<StrengthPower>();
        int bonusDex = (int)((CalculatedVar)DynamicVars["BonusDexterity"]).Calculate(cardPlay.Target);

        if (strengthAmount > 0)
        {
            await PowerCmd.Remove<StrengthPower>(base.Owner.Creature);
        }

        int totalDex = strengthAmount + bonusDex;
        if (totalDex > 0)
        {
            await PowerCmd.Apply<DexterityPower>(choiceContext, base.Owner.Creature, totalDex, base.Owner.Creature, this);
        }

    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}
