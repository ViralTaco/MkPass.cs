using System;
using System.Security.Cryptography; // For RandomNumberGenerator
// c++ code: https://github.com/ViralTaco/mkpass/blob/main/mkpass.cxx

namespace MkPass;
static class App {
  const string kVersion = "mkpass 2.2.0";
  const string kLicense = """
    Copyright (c) 2022,2023 viraltaco_ (Original Work in C++) MIT License.
    Copyright (c) 2025 viraltaco_ (C# Port) All rights reserved.
    """;
  const string kUsage = """
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

  const string kAlphNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
  const string kSymbols = "!\\#$%&'( )*+,-./:;<=>?@[]^_{|}~\"";
  const string kCharset = kAlphNum + kSymbols;

  public static void Main(string[] args) {
    var idx = kCharset.Length - 1;
    var len = 32L;

    if (args.Length > 0) {
      var opt = args[0];
      if (opt.StartsWith('-')) {
        Console.WriteLine(kVersion);
        Console.Write(opt switch { "--license" => kLicense, "--version" => "", _ => kUsage });
        return;
      } else if (opt.StartsWith('o')) {
        if (opt == "off") idx = kAlphNum.Length - 1;
        if (args.Length > 1) len = long.Parse(args[1]);
      } else {
        len = long.Parse(opt);
      }
    }
    
    while (len-- > 0)
      Console.Write(kCharset[RandomNumberGenerator.GetInt32(idx)]);
  }
}