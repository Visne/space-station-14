using System;
using System.Collections.Generic;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using YamlDotNet.RepresentationModel;

namespace Content.Shared.VendingMachines
{
    [Serializable, NetSerializable, Prototype("VendingMachineAdvertisements")]
    public class VendingMachineAdvertisementsPrototype : IPrototype, IIndexedPrototype
    {
        private string _id;
        //private string _name;
        private List<string> _advertisements;

        public string ID => _id;
        //public string Name => _name;
        public List<string> Advertisements => _advertisements;

        public void LoadFrom(YamlMappingNode mapping)
        {
            var serializer = YamlObjectSerializer.NewReader(mapping);

            serializer.DataField(ref _id, "id", string.Empty);
            //serializer.DataField(ref _name, "name", string.Empty);
            serializer.DataField(ref _advertisements, "advertisements", new List<string>());
        }
    }
}
