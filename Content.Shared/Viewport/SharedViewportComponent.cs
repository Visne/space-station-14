using System;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;

namespace Content.Shared.Viewport
{
    public class SharedViewportComponent : Component
    {
        public override string Name => "Viewport";
    }

    [Serializable, NetSerializable]
    public enum ViewportUiKey
    {
        Key
    }
}
