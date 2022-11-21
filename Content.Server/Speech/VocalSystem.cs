using Content.Server.Chat.Systems;
using Content.Server.Humanoid;
using Content.Server.Speech.Components;
using Content.Shared.Chat.Prototypes;
using Content.Shared.Humanoid;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server.Speech;

public sealed class VocalSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<VocalComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<VocalComponent, SexChangedEvent>(OnSexChanged);
        SubscribeLocalEvent<VocalComponent, EmoteEvent>(OnEmote);
    }

    private void OnStartup(EntityUid uid, VocalComponent component, ComponentStartup args)
    {
        LoadSounds(uid, component);
    }

    private void OnSexChanged(EntityUid uid, VocalComponent component, SexChangedEvent args)
    {
        LoadSounds(uid, component);
    }

    private void OnEmote(EntityUid uid, VocalComponent component, ref EmoteEvent args)
    {
        if (args.Handled)
            return;

        // snowflake case for wilhelm scream easter egg
        if (args.Emote.ID == component.ScreamId)
        {
            args.Handled = TryPlayScreamSound(uid, component);
            return;
        }

        // just play regular sound based on emote proto
        args.Handled = TryPlayEmoteSound(uid, component, args.Emote.ID);
    }

    private bool TryPlayEmoteSound(EntityUid uid, VocalComponent component, string emoteId)
    {
        var proto = component.EmoteSounds;
        if (proto == null || !proto.Sounds.TryGetValue(emoteId, out var sound))
            return false;

        var param = proto.Params ?? sound.Params;
        _audio.PlayPvs(sound, uid, param);
        return true;
    }

    private bool TryPlayScreamSound(EntityUid uid, VocalComponent component)
    {
        if (_random.Prob(component.WilhelmProbability))
        {
            _audio.PlayPvs(component.Wilhelm, uid, component.Wilhelm.Params);
            return true;
        }

        return TryPlayEmoteSound(uid, component, component.ScreamId);
    }

    private void LoadSounds(EntityUid uid, VocalComponent component, Sex? sex = null)
    {
        if (component.Sounds == null)
            return;

        sex ??= CompOrNull<HumanoidComponent>(uid)?.Sex ?? Sex.Unsexed;

        if (!component.Sounds.TryGetValue(sex.Value, out var protoId))
            return;
        if (!_proto.TryIndex(protoId, out EmoteSoundsPrototype? proto))
            return;

        component.EmoteSounds = proto;
    }
}
