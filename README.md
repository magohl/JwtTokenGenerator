# JwtTokenGenerator
A simple .net core based commandline JWT sha256 token generator.


```
JwtTokenGenerator 1.0.0
  -k, --key         Required. Ascii key use to generate the symmetric security key. [HS256 which means
  -i, --issuer      Issuer
  -a, --audience    Audience
  -e, --expires     (Default: 5) Token expiry. Use TimeUnit to set the unit. Default is 5 seconds.
  -t, --timeunit    (Default: Seconds) Token expiry time unit. Seconds, Minutes, Hours, Days
  --help            Display this help screen.
  --version         Display version information.
-----------------------------------------------------------
```
