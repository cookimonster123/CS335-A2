//using Microsoft.AspNetCore.Authentication;
//using A2.Data;
//using Microsoft.Extensions.Options;
//using System.Text.Encodings.Web;

//public class A2AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
//{
//    private readonly IA2Repo _repository;


//    public A2AuthHandler(
//        IA2Repo repository,
//        IOptionsMonitor<AuthenticationSchemeOptions> options,
//        ILoggerFactory logger,
//        UrlEncoder encoder,
//        ISystemClock clock)
//        : base(options, logger, encoder, clock)

//    {
//        _repository = repository;
//    }



//}