// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Apache.Arrow.Flatbuf
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

internal struct DictionaryEncoding : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_5_9(); }
  public static DictionaryEncoding GetRootAsDictionaryEncoding(ByteBuffer _bb) { return GetRootAsDictionaryEncoding(_bb, new DictionaryEncoding()); }
  public static DictionaryEncoding GetRootAsDictionaryEncoding(ByteBuffer _bb, DictionaryEncoding obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public DictionaryEncoding __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// The known dictionary id in the application where this data is used. In
  /// the file or streaming formats, the dictionary ids are found in the
  /// DictionaryBatch messages
  public long Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  /// The dictionary indices are constrained to be non-negative integers. If
  /// this field is null, the indices must be signed int32. To maximize
  /// cross-language compatibility and performance, implementations are
  /// recommended to prefer signed integer types over unsigned integer types
  /// and to avoid uint64 indices unless they are required by an application.
  public Int? IndexType { get { int o = __p.__offset(6); return o != 0 ? (Int?)(new Int()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// By default, dictionaries are not ordered, or the order does not have
  /// semantic meaning. In some statistical, applications, dictionary-encoding
  /// is used to represent ordered categorical data, and we provide a way to
  /// preserve that metadata here
  public bool IsOrdered { get { int o = __p.__offset(8); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public DictionaryKind DictionaryKind { get { int o = __p.__offset(10); return o != 0 ? (DictionaryKind)__p.bb.GetShort(o + __p.bb_pos) : DictionaryKind.DenseArray; } }

  public static Offset<DictionaryEncoding> CreateDictionaryEncoding(FlatBufferBuilder builder,
      long id = 0,
      Offset<Int> indexTypeOffset = default(Offset<Int>),
      bool isOrdered = false,
      DictionaryKind dictionaryKind = DictionaryKind.DenseArray) {
    builder.StartTable(4);
    DictionaryEncoding.AddId(builder, id);
    DictionaryEncoding.AddIndexType(builder, indexTypeOffset);
    DictionaryEncoding.AddDictionaryKind(builder, dictionaryKind);
    DictionaryEncoding.AddIsOrdered(builder, isOrdered);
    return DictionaryEncoding.EndDictionaryEncoding(builder);
  }

  public static void StartDictionaryEncoding(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddId(FlatBufferBuilder builder, long id) { builder.AddLong(0, id, 0); }
  public static void AddIndexType(FlatBufferBuilder builder, Offset<Int> indexTypeOffset) { builder.AddOffset(1, indexTypeOffset.Value, 0); }
  public static void AddIsOrdered(FlatBufferBuilder builder, bool isOrdered) { builder.AddBool(2, isOrdered, false); }
  public static void AddDictionaryKind(FlatBufferBuilder builder, DictionaryKind dictionaryKind) { builder.AddShort(3, (short)dictionaryKind, 0); }
  public static Offset<DictionaryEncoding> EndDictionaryEncoding(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<DictionaryEncoding>(o);
  }
}


static internal class DictionaryEncodingVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyField(tablePos, 4 /*Id*/, 8 /*long*/, 8, false)
      && verifier.VerifyTable(tablePos, 6 /*IndexType*/, IntVerify.Verify, false)
      && verifier.VerifyField(tablePos, 8 /*IsOrdered*/, 1 /*bool*/, 1, false)
      && verifier.VerifyField(tablePos, 10 /*DictionaryKind*/, 2 /*DictionaryKind*/, 2, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}
