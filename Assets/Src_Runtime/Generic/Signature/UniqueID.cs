using System;
using System.Runtime.InteropServices;

#pragma warning disable CS0660
#pragma warning disable CS0661

namespace GJ {

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct UniqueID : IEquatable<UniqueID>, IComparable<UniqueID> {

        public static UniqueID Invalid => new UniqueID(0);

        [FieldOffset(0)]
        public ulong value;

        [FieldOffset(4)]
        public EntityType entityType;

        [FieldOffset(0)]
        public int entityID;

        public UniqueID(ulong value) {
            this = default;
            this.value = value;
        }

        public UniqueID(EntityType entityType, int entityID) {
            this = default;
            this.entityType = entityType;
            this.entityID = entityID;
        }

        bool IEquatable<UniqueID>.Equals(UniqueID other) {
            return value == other.value;
        }

        int IComparable<UniqueID>.CompareTo(UniqueID other) {
            return value.CompareTo(other.value);
        }

        public static bool operator ==(UniqueID left, UniqueID right) {
            return left.value == right.value;
        }

        public static bool operator !=(UniqueID left, UniqueID right) {
            return left.value != right.value;
        }

        public string GetString() {
            return $"{entityType}-{entityID}";
        }

        public override string ToString() {
            return GetString();
        }
    }
}