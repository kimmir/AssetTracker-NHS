#pragma checksum "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Account\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f2740e34f88a40bc002480a892ea1e510fef8a2b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Register), @"mvc.1.0.view", @"/Views/Account/Register.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f2740e34f88a40bc002480a892ea1e510fef8a2b", @"/Views/Account/Register.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55d1bc8b2e79134805876d28e775692cd9a3c671", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Register : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-signin"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\i7\Source\Repos\nhs-asset-tracker14\NHS-AssetTracker\Views\Account\Register.cshtml"
  
    ViewData["Title"] = "Register";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <div class=""container"">
        <div class=""row"">
            <div class=""col-md-12"">
                <h2 class=""mt-2 text-center"">Register</h2>
                <hr>
            </div>
            <div class=""col-md-4""></div>
            <div class=""col-md-4"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f2740e34f88a40bc002480a892ea1e510fef8a2b4176", async() => {
                WriteLiteral("\r\n                    <br>\r\n                    <h1 class=\"h3 mb-3 font-weight-normal\">Enter your details to register</h1>\r\n                    <div class=\"form-group\">\r\n                        <label for=\"inputEmail\"");
                BeginWriteAttribute("class", " class=\"", 580, "\"", 588, 0);
                EndWriteAttribute();
                WriteLiteral(">Email address</label>\r\n                        <input type=\"email\" id=\"inputEmail\" class=\"form-control\" placeholder=\"Email address\"");
                BeginWriteAttribute("required", " required=\"", 721, "\"", 732, 0);
                EndWriteAttribute();
                BeginWriteAttribute("autofocus", " autofocus=\"", 733, "\"", 745, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label for=\"inputPassword\"");
                BeginWriteAttribute("class", " class=\"", 873, "\"", 881, 0);
                EndWriteAttribute();
                WriteLiteral(">Password</label>\r\n                        <input type=\"password\" id=\"inputPassword\" class=\"form-control\" placeholder=\"Password\"");
                BeginWriteAttribute("required", " required=\"", 1010, "\"", 1021, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label for=\"inputPassword\"");
                BeginWriteAttribute("class", " class=\"", 1149, "\"", 1157, 0);
                EndWriteAttribute();
                WriteLiteral(">Enter invitation code</label>\r\n                        <input type=\"password\" id=\"inputPassword\" class=\"form-control\" placeholder=\"Code\"");
                BeginWriteAttribute("required", " required=\"", 1295, "\"", 1306, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label for=\"inputPassword\"");
                BeginWriteAttribute("class", " class=\"", 1434, "\"", 1442, 0);
                EndWriteAttribute();
                WriteLiteral(">Enter captcha code below</label>\r\n                        <input type=\"password\" id=\"inputPassword\" class=\"form-control\" placeholder=\"asd2~2ASc\"");
                BeginWriteAttribute("required", " required=\"", 1588, "\"", 1599, 0);
                EndWriteAttribute();
                BeginWriteAttribute("disabled", " disabled=\"", 1600, "\"", 1611, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                        <input type=\"text\" id=\"inputPassword\" class=\"form-control\" placeholder=\"captcha code\"");
                BeginWriteAttribute("required", " required=\"", 1724, "\"", 1735, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <button class=\"btn btn-lg btn-primary btn-block\" type=\"submit\">Sign in</button>\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n");
        }
        #pragma warning restore 1998
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
