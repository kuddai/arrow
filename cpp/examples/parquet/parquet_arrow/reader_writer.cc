// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements. See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership. The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied. See the License for the
// specific language governing permissions and limitations
// under the License.

#include <arrow/api.h>
#include <arrow/io/api.h>
#include <parquet/arrow/reader.h>
#include <parquet/arrow/writer.h>
#include <parquet/stream_writer.h>
#include <parquet/exception.h>

#include <iostream>
#include <string>

using namespace parquet;

// #0 Build dummy data to pass around
// To have some input data, we first create an Arrow Table that holds
// some data.
// std::shared_ptr<arrow::Table> generate_table() {
//   arrow::Int64Builder i64builder;
//   PARQUET_THROW_NOT_OK(i64builder.AppendValues({1, 2, 3, 4, 5}));
//   std::shared_ptr<arrow::Array> i64array;
//   PARQUET_THROW_NOT_OK(i64builder.Finish(&i64array));
// 
//   arrow::StringBuilder strbuilder;
//   PARQUET_THROW_NOT_OK(strbuilder.Append("some"));
//   PARQUET_THROW_NOT_OK(strbuilder.Append("string"));
//   PARQUET_THROW_NOT_OK(strbuilder.Append("content"));
//   PARQUET_THROW_NOT_OK(strbuilder.Append("in"));
//   PARQUET_THROW_NOT_OK(strbuilder.Append("rows"));
//   std::shared_ptr<arrow::Array> strarray;
//   PARQUET_THROW_NOT_OK(strbuilder.Finish(&strarray));
// 
//   std::shared_ptr<arrow::Schema> schema = arrow::schema(
//       {arrow::field("int", arrow::int64()), arrow::field("str", arrow::utf8())});
// 
//   return arrow::Table::Make(schema, {i64array, strarray});
// }
// 
// // #1 Write out the data as a Parquet file
// void write_parquet_file(const arrow::Table& table) {
//   std::shared_ptr<arrow::io::FileOutputStream> outfile;
//   PARQUET_ASSIGN_OR_THROW(
//       outfile, arrow::io::FileOutputStream::Open("parquet-arrow-example.parquet"));
//   // The last argument to the function call is the size of the RowGroup in
//   // the parquet file. Normally you would choose this to be rather large but
//   // for the example, we use a small value to have multiple RowGroups.
//   PARQUET_THROW_NOT_OK(
//       parquet::arrow::WriteTable(table, arrow::default_memory_pool(), outfile, 3));
// }
void write_parquet_file() {
  std::shared_ptr<::arrow::io::FileOutputStream> outfile;
  PARQUET_ASSIGN_OR_THROW(
      outfile, ::arrow::io::FileOutputStream::Open("parquet-arrow-example.parquet"));

  std::shared_ptr<parquet::schema::GroupNode> schema;
  using TestType = ByteArrayType;
  //using c_type = typename TestType::c_type;

  // Repetition::type repetition = Repetition::OPTIONAL;
  Repetition::type repetition = Repetition::REQUIRED;
  int num_columns = 1;
  std::vector<schema::NodePtr> fields;

  for (int i = 0; i < num_columns; ++i) {
    std::string name = "column_" + std::to_string(i);
    fields.push_back(
        schema::PrimitiveNode::Make(name, repetition, Type::BYTE_ARRAY));
  }
  schema::NodePtr node = schema::GroupNode::Make("schema", Repetition::REQUIRED, fields);
  auto gnode = std::static_pointer_cast<schema::GroupNode>(node);

  WriterProperties::Builder builder;
  builder.disable_dictionary();
  builder.compression(Compression::GZIP);
  //builder.encoding

   // Set up builder with required compression type etc.
   // Define schema.
   // ...

  auto file_writer = parquet::ParquetFileWriter::Open(outfile, gnode, builder.build());

  // Loop over some data structure which provides the required
  // fields to be written and write each row.
  //for (const auto& a : getArticles())
  //{
  //   os << a.name() << a.price() << a.quantity() << parquet::EndRow;
  //}
  RowGroupWriter* row_group_writer;


  for (int row = 0; row < 1; ++row) {
    std::cerr << "Writing row " << row << std::endl;
    std::vector<std::string> arr;
    std::vector<ByteArray> values;
    row_group_writer = file_writer->AppendRowGroup();
    for (int col = 0; col < 1; ++col) {
      for (int i = 0; i < 3; ++i) {
        arr.push_back("t_" + std::to_string(i) + "_" + std::to_string(row));
      }

      for (int i = 0; i < 3; ++i) {
        const std::string& s = arr[i];
        values.push_back(ByteArray(static_cast<uint32_t>(s.size()), reinterpret_cast<const uint8_t*>(s.data())));
        std::cerr << "Writing column " << col << " row " << row << " value " << std::string_view(values.back()) << std::endl;
      }
      std::cerr << "Writing column " << col << " row " << row << std::endl;
      auto column_writer =
          static_cast<TypedColumnWriter<TestType>*>(row_group_writer->NextColumn());
      std::cerr << "Writing batch column " << col << std::endl;
      for (size_t i = 0; i < values.size(); ++i) {
        std::cerr << "kuddai mark 1 Writing column " << col << " row " << row << " value " << std::string_view(values[i]) << std::endl;
      }
      column_writer->WriteBatch(values.size(), nullptr, nullptr, values.data());
      std::cerr << "Writing column close " << col << std::endl;
      for (size_t i = 0; i < values.size(); ++i) {
        std::cerr << "kuddai mark 2 Writing column " << col << " row " << row << " value " << std::string_view(values[i]) << std::endl;
      }
      column_writer->Close();
      std::cerr << "Writing column close after " << col << std::endl;
    }
    std::cerr << "Writing row_group close " << std::endl;
    row_group_writer->Close();
  }
  std::cerr << "Writing file_writer close " << std::endl;
  file_writer->Close();
}

// #2: Fully read in the file
// void read_whole_file() {
//   std::cout << "Reading parquet-arrow-example.parquet at once" << std::endl;
//   std::shared_ptr<arrow::io::ReadableFile> infile;
//   PARQUET_ASSIGN_OR_THROW(infile,
//                           arrow::io::ReadableFile::Open("parquet-arrow-example.parquet",
//                                                         arrow::default_memory_pool()));
// 
//   std::unique_ptr<parquet::arrow::FileReader> reader;
//   PARQUET_THROW_NOT_OK(
//       parquet::arrow::OpenFile(infile, arrow::default_memory_pool(), &reader));
//   std::shared_ptr<arrow::Table> table;
//   PARQUET_THROW_NOT_OK(reader->ReadTable(&table));
//   std::cout << "Loaded " << table->num_rows() << " rows in " << table->num_columns()
//             << " columns." << std::endl;
// }
// 
// // #3: Read only a single RowGroup of the parquet file
// void read_single_rowgroup() {
//   std::cout << "Reading first RowGroup of parquet-arrow-example.parquet" << std::endl;
//   std::shared_ptr<arrow::io::ReadableFile> infile;
//   PARQUET_ASSIGN_OR_THROW(infile,
//                           arrow::io::ReadableFile::Open("parquet-arrow-example.parquet",
//                                                         arrow::default_memory_pool()));
// 
//   std::unique_ptr<parquet::arrow::FileReader> reader;
//   PARQUET_THROW_NOT_OK(
//       parquet::arrow::OpenFile(infile, arrow::default_memory_pool(), &reader));
//   std::shared_ptr<arrow::Table> table;
//   PARQUET_THROW_NOT_OK(reader->RowGroup(0)->ReadTable(&table));
//   std::cout << "Loaded " << table->num_rows() << " rows in " << table->num_columns()
//             << " columns." << std::endl;
// }
// 
// // #4: Read only a single column of the whole parquet file
// void read_single_column() {
//   std::cout << "Reading first column of parquet-arrow-example.parquet" << std::endl;
//   std::shared_ptr<arrow::io::ReadableFile> infile;
//   PARQUET_ASSIGN_OR_THROW(infile,
//                           arrow::io::ReadableFile::Open("parquet-arrow-example.parquet",
//                                                         arrow::default_memory_pool()));
// 
//   std::unique_ptr<parquet::arrow::FileReader> reader;
//   PARQUET_THROW_NOT_OK(
//       parquet::arrow::OpenFile(infile, arrow::default_memory_pool(), &reader));
//   std::shared_ptr<arrow::ChunkedArray> array;
//   PARQUET_THROW_NOT_OK(reader->ReadColumn(0, &array));
//   PARQUET_THROW_NOT_OK(arrow::PrettyPrint(*array, 4, &std::cout));
//   std::cout << std::endl;
// }

// #5: Read only a single column of a RowGroup (this is known as ColumnChunk)
//     from the Parquet file.
// void read_single_column_chunk() {
//   std::cout << "Reading first ColumnChunk of the first RowGroup of "
//                "parquet-arrow-example.parquet"
//             << std::endl;
//   std::shared_ptr<arrow::io::ReadableFile> infile;
//   PARQUET_ASSIGN_OR_THROW(infile,
//                           arrow::io::ReadableFile::Open("parquet-arrow-example.parquet",
//                                                         arrow::default_memory_pool()));
// 
//   std::unique_ptr<parquet::arrow::FileReader> reader;
//   PARQUET_THROW_NOT_OK(
//       parquet::arrow::OpenFile(infile, arrow::default_memory_pool(), &reader));
//   std::shared_ptr<arrow::ChunkedArray> array;
//   PARQUET_THROW_NOT_OK(reader->RowGroup(0)->Column(0)->Read(&array));
//   PARQUET_THROW_NOT_OK(arrow::PrettyPrint(*array, 4, &std::cout));
//   std::cout << std::endl;
// }

int main(int argc, char** argv) {
  //std::shared_ptr<arrow::Table> table = generate_table();
  // write_parquet_file(*table);
  write_parquet_file();
  // read_whole_file();
  // read_single_rowgroup();
  // read_single_column();
  // read_single_column_chunk();
}
