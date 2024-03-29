// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Apache.Arrow.Flatbuf
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// Same as Utf8, but with 64-bit offsets, allowing to represent
/// extremely large data values.
internal struct LargeUtf8 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_5_9(); }
  public static LargeUtf8 GetRootAsLargeUtf8(ByteBuffer _bb) { return GetRootAsLargeUtf8(_bb, new LargeUtf8()); }
  public static LargeUtf8 GetRootAsLargeUtf8(ByteBuffer _bb, LargeUtf8 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LargeUtf8 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartLargeUtf8(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<LargeUtf8> EndLargeUtf8(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<LargeUtf8>(o);
  }
}


static internal class LargeUtf8Verify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}
