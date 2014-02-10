using System;

namespace pmp.sdk.Intefaces
{
    public interface ICollectionDoc
    {
        Guid Guid { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }

        void Links(string relType);
        object Search(string urn);
        void Items();
        void Save();
    }
}
