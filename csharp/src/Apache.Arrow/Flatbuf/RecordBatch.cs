// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Apache.Arrow.Flatbuf
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// A data header describing the shared memory layout of a "record" or "row"
/// batch. Some systems call this a "row batch" internally and others a "record
/// batch".
internal struct RecordBatch : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_5_9(); }
  public static RecordBatch GetRootAsRecordBatch(ByteBuffer _bb) { return GetRootAsRecordBatch(_bb, new RecordBatch()); }
  public static RecordBatch GetRootAsRecordBatch(ByteBuffer _bb, RecordBatch obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RecordBatch __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// number of records / rows. The arrays in the batch should all have this
  /// length
  public long Length { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  /// Nodes correspond to the pre-ordered flattened logical schema
  public FieldNode? Nodes(int j) { int o = __p.__offset(6); return o != 0 ? (FieldNode?)(new FieldNode()).__assign(__p.__vector(o) + j * 16, __p.bb) : null; }
  public int NodesLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
  /// Buffers correspond to the pre-ordered flattened buffer tree
  ///
  /// The number of buffers appended to this list depends on the schema. For
  /// example, most primitive arrays will have 2 buffers, 1 for the validity
  /// bitmap and 1 for the values. For struct arrays, there will only be a
  /// single buffer for the validity (nulls) bitmap
  public Buffer? Buffers(int j) { int o = __p.__offset(8); return o != 0 ? (Buffer?)(new Buffer()).__assign(__p.__vector(o) + j * 16, __p.bb) : null; }
  public int BuffersLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
  /// Optional compression of the message body
  public BodyCompression? Compression { get { int o = __p.__offset(10); return o != 0 ? (BodyCompression?)(new BodyCompression()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<RecordBatch> CreateRecordBatch(FlatBufferBuilder builder,
      long length = 0,
      VectorOffset nodesOffset = default(VectorOffset),
      VectorOffset buffersOffset = default(VectorOffset),
      Offset<BodyCompression> compressionOffset = default(Offset<BodyCompression>)) {
    builder.StartTable(4);
    RecordBatch.AddLength(builder, length);
    RecordBatch.AddCompression(builder, compressionOffset);
    RecordBatch.AddBuffers(builder, buffersOffset);
    RecordBatch.AddNodes(builder, nodesOffset);
    return RecordBatch.EndRecordBatch(builder);
  }

  public static void StartRecordBatch(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddLength(FlatBufferBuilder builder, long length) { builder.AddLong(0, length, 0); }
  public static void AddNodes(FlatBufferBuilder builder, VectorOffset nodesOffset) { builder.AddOffset(1, nodesOffset.Value, 0); }
  public static void StartNodesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(16, numElems, 8); }
  public static void AddBuffers(FlatBufferBuilder builder, VectorOffset buffersOffset) { builder.AddOffset(2, buffersOffset.Value, 0); }
  public static void StartBuffersVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(16, numElems, 8); }
  public static void AddCompression(FlatBufferBuilder builder, Offset<BodyCompression> compressionOffset) { builder.AddOffset(3, compressionOffset.Value, 0); }
  public static Offset<RecordBatch> EndRecordBatch(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<RecordBatch>(o);
  }
}


static internal class RecordBatchVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyField(tablePos, 4 /*Length*/, 8 /*long*/, 8, false)
      && verifier.VerifyVectorOfData(tablePos, 6 /*Nodes*/, 16 /*FieldNode*/, false)
      && verifier.VerifyVectorOfData(tablePos, 8 /*Buffers*/, 16 /*Buffer*/, false)
      && verifier.VerifyTable(tablePos, 10 /*Compression*/, BodyCompressionVerify.Verify, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}
