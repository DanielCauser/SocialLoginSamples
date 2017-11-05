using System;
namespace GoogleNativeLogin.Models
{
    public class GoogleUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
}
