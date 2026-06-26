using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Keywords;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Cards.Keywords;
using WhiteFlame.WhiteFlameCode.Powers;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class RmShadowOfAshlia() : WhiteFlameCardTemplate(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<RecallingMemoryPower>(1m),
        new CardsVar(2),
        new CalculationBaseVar(1m),
        new CalculationExtraVar(1m),
        new CalculatedVar("DrawCards").WithMultiplier((CardModel card, Creature _) => card.Owner.Creature.GetPowerAmount<RecallingMemoryPower>() / 6)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<RmAshlia>(base.IsUpgraded),
        HoverTipFactory.FromPower<RecallingMemoryPower>()
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [WhiteFlameKeywords.Recall.GetModCardKeyword()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

        var sortedCards = (from c in PileType.Draw.GetPile(base.Owner).Cards
                           orderby c.Rarity, c.Id
                           select c).ToList();

        var selected = (await CardSelectCmd.FromSimpleGrid(choiceContext, sortedCards, base.Owner,
            new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, base.DynamicVars.Cards.IntValue))).ToList();

        foreach (var card in selected)
        {
            var result = await CardCmd.TransformTo<RmAshlia>(card);
            if (base.IsUpgraded && result.HasValue)
            {
                CardCmd.Upgrade(result.Value.cardAdded);
            }
        }

        await CardPileCmd.Draw(choiceContext, this.DynamicVars["DrawCards"].PreviewValue, base.Owner);
        await PowerCmd.Apply<RecallingMemoryPower>(choiceContext, base.Owner.Creature, base.DynamicVars["RecallingMemoryPower"].BaseValue, base.Owner.Creature, this);
    }
}
