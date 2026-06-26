using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class LastShot() : WhiteFlameCardTemplate(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<LastShotPower>(3m),
        new PowerVar<PyrePower>(2m),
        new PowerVar<MachineLearningPower>(2m)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<LastShotPower>(),
        HoverTipFactory.FromPower<PyrePower>(),
        HoverTipFactory.FromPower<MachineLearningPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<LastShotPower>(choiceContext, base.Owner.Creature, base.DynamicVars["LastShotPower"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<PyrePower>(choiceContext, base.Owner.Creature, base.DynamicVars["PyrePower"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<MachineLearningPower>(choiceContext, base.Owner.Creature, base.DynamicVars["MachineLearningPower"].BaseValue, base.Owner.Creature, this);

        foreach (CardModel card in base.Owner.PlayerCombatState.AllCards)
        {
            if (card != this && card.IsUpgradable)
            {
                CardCmd.Upgrade(card);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["LastShotPower"].UpgradeValueBy(-1m);
    }
}
