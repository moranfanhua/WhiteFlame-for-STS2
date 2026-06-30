using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
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

public class RmIcyStorm() : WhiteFlameCardTemplate(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies), IModelCapabilitySource
{
    public void BuildDefaultCapabilities(ModelCapabilityList capabilities)
    {
        capabilities.Add<RecallingMemoryCardPlayCapability>();
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<RecallingMemoryPower>(1m),
        new DamageVar(13, ValueProp.Move)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        StunIntent.GetStaticHoverTip(),
        HoverTipFactory.FromPower<RecallingMemoryPower>()
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [WhiteFlameKeywords.Recall.GetModCardKeyword()];

    protected override bool IsPlayable => (this.Owner?.Creature?.GetPowerAmount<RecallingMemoryPower>() ?? 0) >= 5;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState).Execute(choiceContext);

        foreach (Creature target in base.CombatState.HittableEnemies)
        {
            await CreatureCmd.Stun(target);
        }
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}
