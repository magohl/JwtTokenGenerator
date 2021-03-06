﻿using CommandLine;
using Microsoft.IdentityModel.Tokens;

namespace JwtTokenGenerator
{
    public class Options
    {
        [Option('k', "key", Required = true, HelpText = "Ascii key use to generate the symmetric security key. [Minimum 16 chars]")]
        public string Key { get; set; }

        [Option('i', "issuer", Required = false, HelpText = "Issuer")]
        public string Issuer { get; set; }

        [Option('a', "audience", Required = false, HelpText = "Audience")]
        public string Audience { get; set; }

        [Option('s', "subject", Required = false, HelpText = "Subject")]
        public string Subject { get; set; }

        [Option('e', "expires", Required = false, Default = 5, HelpText = "Token expiry. Use TimeUnit to set the unit. Default is 5 seconds.")]
        public double Expires { get; set; }

        [Option('t', "timeunit", Required = false, Default=TimeUnit.Seconds, HelpText = "Token expiry time unit. Seconds, Minutes, Hours, Days")]
        public TimeUnit TimeUnit {get;set;}

        [Option('g', "algorithm", Required = false, Default=SecurityAlgorithms.HmacSha256, HelpText = "SecurityAlgorithm. Eg. HS256, HS512 etc.")]
        public string SecurityAlgorithm {get;set;}

        public void Validate()
        {
            //A better validator is needed as we now allow different algorithms
            // if (Key.Length<16)
            // {
            //     throw new ArgumentValidationException($"Key must be at least 16 characters long!");
            // }
        }
    }
}
