using System;
using System.Security.Cryptography; // For RandomNumberGenerator
// c++ code: https://github.com/ViralTaco/mkpass/blob/main/mkpass.cxx

namespace MkPass;
static class App {
  const string version = "mkpass 2.0.1";
  const string license = """
    Copyright (c) 2022,2023 viraltaco_ (Original Work in C++) MIT License.
    Copyright (c) 2025 viraltaco_ (C# Port) All rights reserved.
    """;
  const string usage = """
    mkpass generates 'random' passwords.
    usage:
    mkpass [OPT] symbols length

    symbols         (off|on) if on use symbols. Default: on
    length          (0 to 2^63) length/size in bytes. Default: 32

    OPT (one of):
    --help         Prints this help message.
    --license      Prints the license.
    --version      Prints the version.

    Examples:
    mkpass         Prints a 32 characters long password with symbols.
    mkpass on 32   Prints a 32 characters long password with symbols.
    mkpass off 1   Prints one alpha-numeric character.
    mkpass --help  Prints everything you just read.
    """;

  const string charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
  const string symbols = "!\\#$%&'( )*+,-./:;<=>?@[]^_{|}~\"";
  const string alphabet = charset + symbols;

  public static void Main(string[] args) {
    var idx = alphabet.Length - 1;
    var len = 32u;

    if (args.Length > 0) {
      var opt = args[0];
      if (opt.StartsWith('-')) {
        Console.WriteLine(version);
        Console.Write(opt switch { "--license" => license, "--version" => "", _ => usage });
        return;
      } 
      if (opt == "off") idx = charset.Length - 1;
      len = uint.Parse((args.Length > 1) ? args[1] : opt);
    }

    using (var rng = RandomNumberGenerator.Create()) {
      var buf = new byte[len];
      rng.GetBytes(buf); // Fill the buffer with random bytes
      foreach (var b in buf) Console.Write(alphabet[b % idx]);
    }
  } // Main()
} // class App
