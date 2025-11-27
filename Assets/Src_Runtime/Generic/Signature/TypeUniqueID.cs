// TypeUniqueID = TypeID typeID + int specID
using System;
using System.Runtime.InteropServices;

#pragma warning disable CS0660
#pragma warning disable CS0661

namespace GJ {

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct TypeUniqueID : IEquatable<TypeUniqueID>, IComparable<TypeUniqueID> {

        [FieldOffset(0)]
        [NonSerialized]
        public decimal value;

        [FieldOffset(8)]
        public TypeID typeID;

        [FieldOffset(4)]
        public EntityType entityType;

        [FieldOffset(0)]
        public int specID;

        public TypeUniqueID(decimal value) {
            this = default;
            this.value = value;
        }

        public TypeUniqueID(TypeID typeID, EntityType entityType, int specID) {
            this = default;
            this.typeID = typeID;
            this.entityType = entityType;
            this.specID = specID;
        }

        bool IEquatable<TypeUniqueID>.Equals(TypeUniqueID other) {
            return value == other.value;
        }

        int IComparable<TypeUniqueID>.CompareTo(TypeUniqueID other) {
            return value.CompareTo(other.value);
        }

        public string GetString() {
            return $"{typeID}^{specID}";
        }

        public override string ToString() {
            return GetString();
        }

        public static bool operator ==(TypeUniqueID left, TypeUniqueID right) {
            return left.value == right.value;
        }

        public static bool operator !=(TypeUniqueID left, TypeUniqueID right) {
            return left.value != right.value;
        }
    }
}