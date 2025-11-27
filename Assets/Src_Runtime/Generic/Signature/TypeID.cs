using System;
using System.Runtime.InteropServices;

#pragma warning disable CS0660
#pragma warning disable CS0661

namespace GJ {

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct TypeID : IEquatable<TypeID>, IComparable<TypeID> {

        public static TypeID Invalid = new TypeID(0);

        [FieldOffset(0)]
        [NonSerialized]
        public ulong value;

        [FieldOffset(6)]
        public ushort typeMajor;

        [FieldOffset(2)]
        public uint typeMinor;

        [FieldOffset(0)]
        public ushort typePatch;

        public TypeID(ulong value) {
            this = default;
            this.value = value;
        }

        public TypeID(ushort typeMajor, uint typeMinor, ushort typePatch) {
            this = default;
            this.typeMajor = typeMajor;
            this.typeMinor = typeMinor;
            this.typePatch = typePatch;
        }

        bool IEquatable<TypeID>.Equals(TypeID other) {
            return value == other.value;
        }

        int IComparable<TypeID>.CompareTo(TypeID other) {
            return value.CompareTo(other.value);
        }

        public string GetString() {
            return $"{typeMajor}-{typeMinor}-{typePatch}";
        }

        public override string ToString() {
            return GetString();
        }

        public static bool operator ==(TypeID left, TypeID right) {
            return left.value == right.value;
        }

        public static bool operator !=(TypeID left, TypeID right) {
            return left.value != right.value;
        }
    }
}