﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommandLine;
using Microsoft.IdentityModel.Tokens;

namespace JwtTokenGenerator
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var parser = new Parser(s=>s.CaseInsensitiveEnumValues=true);
                return parser.ParseArguments<Options>(args)
                    .MapResult(
                        (Options opts) =>
                        {
                            opts.Validate();
                            string tokenRaw = GetToken(options: opts);
                            Console.WriteLine($"SHA256 JWT key generated: {tokenRaw}");
                            return 0;
                        },
                        errors => 1);
            }
            catch (ArgumentValidationException e)
            {
                Console.WriteLine("Invalid arguments detected: {0}", e.Message);
                return 1;
            }          
        }

        private static string GetToken(Options options)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var expiryTimeSpan = new TimeSpan();
            var tokenDescriptor = new SecurityTokenDescriptor();
            var key = Encoding.ASCII.GetBytes(options.Key);

            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), options.SecurityAlgorithm);

            if (options.Subject != null)
                tokenDescriptor.Subject =  new System.Security.Claims.ClaimsIdentity(new List<Claim>{new Claim("sub", options.Subject)});

            if (options.Issuer != null)
                tokenDescriptor.Issuer = options.Issuer;

            if (options.Audience != null)
                tokenDescriptor.Audience = options.Audience;

            switch (options.TimeUnit)
            {
                case TimeUnit.Seconds:
                    expiryTimeSpan = TimeSpan.FromSeconds(options.Expires);
                    break;
                case TimeUnit.Minutes:
                    expiryTimeSpan = TimeSpan.FromMinutes(options.Expires);
                    break;
                case TimeUnit.Hours:
                    expiryTimeSpan = TimeSpan.FromHours(options.Expires);
                    break;
                case TimeUnit.Days:
                    expiryTimeSpan = TimeSpan.FromDays(options.Expires);
                    break;
            }

            tokenDescriptor.Expires = DateTime.Now.Add(expiryTimeSpan);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenRaw = tokenHandler.WriteToken(token);
            return tokenRaw;
        }
    }
}
