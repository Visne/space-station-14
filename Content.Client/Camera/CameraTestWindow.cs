using Robust.Client.GameObjects;
using Robust.Client.Graphics.Drawing;
using Robust.Client.Interfaces.Console;
using Robust.Client.Interfaces.Graphics;
using Robust.Client.Interfaces.Placement;
using Robust.Client.Placement;
using Robust.Client.Player;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Interfaces.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Map;
using Robust.Shared.Maths;
using Robust.Shared.Prototypes;

namespace Content.Client.Camera
{
    internal class CameraCommand : IConsoleCommand
    {
        public string Command => "camera";
        public string Help => "Show camera";
        public string Description => "Show camera";

        public bool Execute(IDebugConsole console, params string[] args)
        {
            var window = new CameraTestWindow();

            window.OpenCentered();
            return true;
        }
    }

    public class CameraTestWindow : SS14Window
    {
        private IPlacementManager _placementManager = new PlacementManager();
        private IPrototypeManager _prototypeManager = new PrototypeManager();

        public CameraTestWindow()
        {
            var clyde = IoCManager.Resolve<IClyde>();

            var vp = clyde.CreateViewport((400, 400), "CameraTest");
            //vp.Eye = eyeComponent.Eye;

            Contents.AddChild(new CameraControl(vp));
        }

        private sealed class CameraControl : Control
        {
            private readonly IClydeViewport _viewport;

            public CameraControl(IClydeViewport viewport)
            {
                _viewport = viewport;
            }

            protected override Vector2 CalculateMinimumSize()
            {
                return _viewport.Size / UIScale * 2;
            }

            protected override void Draw(DrawingHandleScreen handle)
            {
                _viewport.Render();
                handle.DrawTextureRect(_viewport.RenderTarget.Texture, UIBox2.FromDimensions(Vector2.Zero, _viewport.Size * 2));
            }
        }
    }
}
