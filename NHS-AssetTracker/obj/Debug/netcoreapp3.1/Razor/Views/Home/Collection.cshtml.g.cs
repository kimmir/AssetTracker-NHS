#pragma checksum "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Home\Collection.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "854b7f482e8c7470c73fe3f117a6bbd1c1f33fad"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Collection), @"mvc.1.0.view", @"/Views/Home/Collection.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\_ViewImports.cshtml"
using NHS_AssetTracker;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\_ViewImports.cshtml"
using NHS_AssetTracker.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Home\Collection.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"854b7f482e8c7470c73fe3f117a6bbd1c1f33fad", @"/Views/Home/Collection.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55d1bc8b2e79134805876d28e775692cd9a3c671", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Collection : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Home\Collection.cshtml"
  
    ViewData["Title"] = "Collection";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 9 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Home\Collection.cshtml"
 if (SignInManager.IsSignedIn(User)) //29.02.20 - Kim: Updated to prevent users manually loading a page and viewing content without being logged in
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"container\">\r\n        <div class=\"row\">\r\n            <div class=\"col-md-12\">\r\n                <h2 class=\"mt-2 text-center\">");
#nullable restore
#line 14 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Home\Collection.cshtml"
                                        Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                <hr>\r\n                <br>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-md-12\">\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 24 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Home\Collection.cshtml"
}
else
{

}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<ApplicationUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<ApplicationUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
