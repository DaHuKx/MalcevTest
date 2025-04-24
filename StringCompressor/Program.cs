using StringCompressor;

var compress1 = "aaaabcccdddeeeeeeeeeeeeeeef";
var compress2 = "abcd";
var compress3 = "aaabbc4ddd ";

var decompress1 = "a2b3e4f10c4";
var decompress2 = "1a2e3f4b";
var decompress3 = "a4b5c0d2";
var decompress4 = "abcdef1";

Console.WriteLine($"Compress 1: {compress1} - {Compressor.Compress(compress1)}");
Console.WriteLine($"Compress 2: {compress2} - {Compressor.Compress(compress2)}");
Console.WriteLine($"Compress 3: {compress3} - {Compressor.Compress(compress3)}");
Console.WriteLine();
Console.WriteLine($"Decompress 1: {decompress1} - {Compressor.Decompress(decompress1)}");
Console.WriteLine($"Decompress 2: {decompress2} - {Compressor.Decompress(decompress2)}");
Console.WriteLine($"Decompress 3: {decompress3} - {Compressor.Decompress(decompress3)}");
Console.WriteLine($"Decompress 4: {decompress4} - {Compressor.Decompress(decompress4)}");