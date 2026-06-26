using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using STS2RitsuLib;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class SepiaGift() : WhiteFlameCardTemplate(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    private const int MaxCombats = 5;
    private static bool _isSubscribed;
    private static readonly Dictionary<Player, List<TrackedCard>> _trackedCards = new();

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var cardPool = base.Owner.Character.CardPool.GetUnlockedCards(
            base.Owner.UnlockState,
            base.Owner.RunState.CardMultiplayerConstraint);

        EnsureSubscribed();

        for (int i = 0; i < 3; i++)
        {
            var options = CardFactory.GetDistinctForCombat(
                base.Owner, cardPool, 3,
                base.Owner.RunState.Rng.CombatCardGeneration).ToList();

            if (base.IsUpgraded)
            {
                CardCmd.Upgrade(options, CardPreviewStyle.None);
            }

            var chosen = await CardSelectCmd.FromChooseACardScreen(
                choiceContext, options, base.Owner, canSkip: true);

            if (chosen != null)
            {
                await CardPileCmd.AddGeneratedCardToCombat(chosen, PileType.Deck, base.Owner);
                TrackCard(chosen, base.Owner);
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }

    private static void TrackCard(CardModel card, Player owner)
    {
        if (!_trackedCards.TryGetValue(owner, out var list))
        {
            list = new List<TrackedCard>();
            _trackedCards[owner] = list;
        }
        list.Add(new TrackedCard(card, MaxCombats));
    }

    private static void EnsureSubscribed()
    {
        if (_isSubscribed) return;
        _isSubscribed = true;
        RitsuLibFramework.SubscribeLifecycle<CombatEndedEvent>(evt => OnCombatEnded(evt).GetAwaiter().GetResult());
    }

    private static async Task OnCombatEnded(CombatEndedEvent evt)
    {
        var toRemove = new List<Player>();

        foreach (var (player, entries) in _trackedCards)
        {
            for (int i = entries.Count - 1; i >= 0; i--)
            {
                entries[i].RemainingCombats--;
                if (entries[i].RemainingCombats <= 0
                    && entries[i].Card.Pile?.Type == PileType.Deck)
                {
                    await CardPileCmd.RemoveFromDeck(entries[i].Card);
                    entries.RemoveAt(i);
                }
            }

            if (entries.Count == 0)
                toRemove.Add(player);
        }

        foreach (var player in toRemove)
            _trackedCards.Remove(player);
    }

    private sealed class TrackedCard(CardModel card, int remainingCombats)
    {
        public CardModel Card = card;
        public int RemainingCombats = remainingCombats;
    }
}
