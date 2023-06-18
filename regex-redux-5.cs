// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Adapted from regex-redux C# .NET Core #5 program
// http://benchmarksgame.alioth.debian.org/u64q/program.php?test=regexredux&lang=csharpcore&id=5
// aka (as of 2017-09-01) rev 1.3 of https://alioth.debian.org/scm/viewvc.php/benchmarksgame/bench/regexredux/regexredux.csharp-5.csharp?root=benchmarksgame&view=log
// Best-scoring C# .NET Core version as of 2017-09-01

/* The Computer Language Benchmarks Game
   http://benchmarksgame.alioth.debian.org/
 
   Regex-Redux by Josh Goldfoot
*/

using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
//using BenchmarkDotNet.Attributes;
//using MicroBenchmarks;

namespace BenchmarksGame
{
    //[BenchmarkCategory(Categories.Runtime, Categories.BenchmarksGame, Categories.JIT, Categories.Regex, Categories.NoWASM)]
    public partial class RegexRedux_5
    {
        private static string _sequences = "";

        public static void Main(string[] args)
        {  

            using (var inputStream = new FileStream( @"C:\proj\rr\rr_input_5000000", FileMode.Open))
            using (var input = new StreamReader(inputStream))
            {
                _sequences = input.ReadToEnd();
            }

            _ = args[0] switch
            {
                "gen" => RunBench5_Generated(),
                "reg" => RunBench5_Compiled(RegexOptions.Compiled),
                _ => throw new Exception()
            };
        }

        [GeneratedRegex(@">.*\n|\n")]
        public static partial Regex ReplaceRegex();
        [GeneratedRegex(@"tHa[Nt]")]
        public static partial Regex MagicRe1();
        [GeneratedRegex(@"aND|caN|Ha[DS]|WaS")]
        public static partial Regex MagicRe2();
        [GeneratedRegex(@"a[NSt]|BY")]
        public static partial Regex MagicRe3();
        [GeneratedRegex(@"<[^>]*>")]
        public static partial Regex MagicRe4();
        [GeneratedRegex(@"\|[^|][^|]*\|")]
        public static partial Regex MagicRe5();
        [GeneratedRegex(@"[cgt]gggtaaa|tttaccc[acg]")]
        public static partial Regex Re2();
        [GeneratedRegex(@"a[act]ggtaaa|tttacc[agt]t")]
        public static partial Regex Re3();
        [GeneratedRegex(@"agggt[cgt]aa|tt[acg]accct")]
        public static partial Regex Re7();
        [GeneratedRegex(@"aggg[acg]aaa|ttt[cgt]ccct")]
        public static partial Regex Re6();
        [GeneratedRegex(@"ag[act]gtaaa|tttac[agt]ct")]
        public static partial Regex Re4();
        [GeneratedRegex(@"agg[act]taaa|ttta[agt]cct")]
        public static partial Regex Re5();
        [GeneratedRegex(@"agggtaaa|tttaccct")]
        public static partial Regex Re1();
        [GeneratedRegex(@"agggtaa[cgt]|[acg]ttaccct")]
        public static partial Regex Re9();
        [GeneratedRegex(@"agggta[cgt]a|t[acg]taccct")]
        public static partial Regex Re8();

        public static int RunBench5_Generated()
        {
            Console.WriteLine("generated");
            var sequences = _sequences;
            var initialLength = sequences.Length;
            sequences = ReplaceRegex().Replace(sequences, "");

            var magicTask = Task.Run(() =>
            {
                var newseq = MagicRe1().Replace(sequences, "<4>");
                newseq = MagicRe2().Replace(newseq, "<3>");
                newseq = MagicRe3().Replace(newseq, "<2>");
                newseq = MagicRe4().Replace(newseq, "|");
                newseq = MagicRe5().Replace(newseq, "-");
                return newseq.Length;
            });



            Console.Out.WriteLineAsync("\n" + initialLength + "\n" + sequences.Length);
            Console.Out.WriteLineAsync(magicTask.Result.ToString());

            return magicTask.Result;
        }

        public static int RunBench5_Compiled(RegexOptions options)
        {
            Console.WriteLine("compiled");
            var sequences = _sequences;
            var initialLength = sequences.Length;
            sequences = Regex.Replace(sequences, ">.*\n|\n", "", options);

            var magicTask = Task.Run(() =>
                {
                    var newseq = Regex.Replace(sequences, "tHa[Nt]", "<4>", options);
                    newseq = Regex.Replace(newseq, "aND|caN|Ha[DS]|WaS", "<3>", options);
                    newseq = Regex.Replace(newseq, "a[NSt]|BY", "<2>", options);
                    newseq = Regex.Replace(newseq, "<[^>]*>", "|", options);
                    newseq = Regex.Replace(newseq, "\\|[^|][^|]*\\|", "-", options);
                    return newseq.Length;
            });



            Console.Out.WriteLineAsync("\n" + initialLength + "\n" + sequences.Length);
            Console.Out.WriteLineAsync(magicTask.Result.ToString());

            return magicTask.Result;
        }

        //public static string Hash(string s)
        //{
        //    //Convert the string into an array of bytes.
        //    byte[] messageBytes = Encoding.UTF8.GetBytes(s);

        //    //Create the hash value from the array of bytes.
        //    byte[] hashValue = SHA256.HashData(messageBytes);

        //    //Display the hash value to the console.
        //    return Convert.ToHexString(hashValue);
        //}
    }
}
