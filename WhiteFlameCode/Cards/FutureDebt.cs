using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class FutureDebt() : WhiteFlameCardTemplate(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FutureDebtPower>(base.IsUpgraded ? 2m : 3m)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<FutureDebtPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // 本回合所有牌耗能-1
        var handCards = PileType.Hand.GetPile(base.Owner).Cards.ToList();
        foreach (var c in handCards)
        {
            c.EnergyCost.AddThisTurn(-1);
        }

        // 施加固定层数的伤害放大debuff
        await PowerCmd.Apply<FutureDebtPower>(
            choiceContext,
            base.Owner.Creature,
            base.DynamicVars["FutureDebtPower"].BaseValue,
            base.Owner.Creature,
            this);
    }
}
