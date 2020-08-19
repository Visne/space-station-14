using Content.Shared.Viewport;
using Robust.Server.GameObjects.Components.UserInterface;
using Robust.Shared.GameObjects;

namespace Content.Server.Camera
{
    [RegisterComponent]
    public class ViewportComponent : SharedViewportComponent
    {
        private BoundUserInterface _userInterface;

        public override void Initialize()
        {
            base.Initialize();
            _userInterface = Owner.GetComponent<ServerUserInterfaceComponent>().GetBoundUserInterface(ViewportUiKey.Key);
        }
    }
}
