using Microsoft.AspNetCore.Identity;

namespace PracaInzynierskaDietetyka.Data
{
    public class XRole : IdentityRole
    {
        public XRole() : base() { }

        public DateTime CreatedDate { get; set; }
    }
}