using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class FactorShield() : WhiteFlameCardTemplate(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
{
    private const decimal BlockPerStack = 2m;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<LyticFactorPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int totalLytic = 0;
        foreach (var enemy in base.CombatState.HittableEnemies)
        {
            int stacks = enemy.GetPowerAmount<LyticFactorPower>();
            if (stacks > 0)
            {
                totalLytic += stacks;
                await PowerCmd.Remove<LyticFactorPower>(enemy);
            }
        }

        if (totalLytic > 0)
        {
            decimal block = totalLytic * (base.IsUpgraded ? 3m : BlockPerStack);
            await CreatureCmd.GainBlock(base.Owner.Creature, block, ValueProp.Move, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
    }
}
