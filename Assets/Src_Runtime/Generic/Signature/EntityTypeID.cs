using System;
using System.Runtime.InteropServices;

#pragma warning disable CS0660
#pragma warning disable CS0661

namespace GJ {

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct EntityTypeID : IEquatable<EntityTypeID>, IComparable<EntityTypeID> {

        [FieldOffset(0)]
        [NonSerialized]
        public decimal value;

        [FieldOffset(8)]
        public EntityType entityType;

        [FieldOffset(0)]
        public TypeID typeID;

        public EntityTypeID(decimal value) {
            this = default;
            this.value = value;
        }

        public EntityTypeID(EntityType entityType, TypeID typeID) {
            this = default;
            this.entityType = entityType;
            this.typeID = typeID;
        }

        bool IEquatable<EntityTypeID>.Equals(EntityTypeID other) {
            return value == other.value;
        }

        int IComparable<EntityTypeID>.CompareTo(EntityTypeID other) {
            return value.CompareTo(other.value);
        }

        public string GetString() {
            return $"{entityType}-{typeID.GetString()}";
        }

        public override string ToString() {
            return GetString();
        }

        public static bool operator ==(EntityTypeID left, EntityTypeID right) {
            return left.value == right.value;
        }

        public static bool operator !=(EntityTypeID left, EntityTypeID right) {
            return left.value != right.value;
        }
    }
}