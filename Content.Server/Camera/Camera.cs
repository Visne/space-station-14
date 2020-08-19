#nullable enable
using System;
using Robust.Server.GameObjects;
using Robust.Server.Interfaces.Console;
using Robust.Server.Interfaces.Player;
using Robust.Shared.Interfaces.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Map;
using Robust.Shared.Maths;

namespace Content.Server.Camera
{
    public class Camera
    {
        internal class CameraCommand : IClientCommand
        {
            public string Command => "camera";
            public string Help => "Show camera";
            public string Description => "Show camera";

            public void Execute(IConsoleShell shell, IPlayerSession? player, string[] args)
            {
                var mapId = player!.AttachedEntity!.Transform.MapID;
                var cameraPos = new MapCoordinates(Vector2.Zero, mapId);

                var cameraEnt = IoCManager.Resolve<IEntityManager>().SpawnEntity(null, cameraPos);

                var viewportComponent = cameraEnt.AddComponent<ViewportComponent>();
                var serverEyeComponent = cameraEnt.AddComponent<ServerEyeComponent>();

                Console.WriteLine("test test test");

            }
        }
    }
}
