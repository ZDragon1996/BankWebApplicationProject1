#pragma checksum "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fcfd00cda42b9e37b652d46ec256a870f294b8c1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Accounts_TransactionList), @"mvc.1.0.view", @"/Views/Accounts/TransactionList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Accounts/TransactionList.cshtml", typeof(AspNetCore.Views_Accounts_TransactionList))]
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
#line 1 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\_ViewImports.cshtml"
using BankingWebApplication;

#line default
#line hidden
#line 2 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\_ViewImports.cshtml"
using BankingWebApplication.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fcfd00cda42b9e37b652d46ec256a870f294b8c1", @"/Views/Accounts/TransactionList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62a793f8994a5dc7b8377c9ce3733b88aed08cea", @"/Views/_ViewImports.cshtml")]
    public class Views_Accounts_TransactionList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DAL.Entities.Transaction>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(46, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(89, 40, true);
            WriteLiteral("\r\n<h2>Transaction List</h2>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(129, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f43fad30730e4898a1a4f6201d08a852", async() => {
                BeginContext(151, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(167, 94, true);
            WriteLiteral("\r\n</p>\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(262, 49, false);
#line 17 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
           Write(Html.DisplayNameFor(model => model.TransactionNo));

#line default
#line hidden
            EndContext();
            BeginContext(311, 57, true);
            WriteLiteral("\r\n            </th>\r\n\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(369, 45, false);
#line 21 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
           Write(Html.DisplayNameFor(model => model.Accountno));

#line default
#line hidden
            EndContext();
            BeginContext(414, 57, true);
            WriteLiteral("\r\n            </th>\r\n\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(472, 51, false);
#line 25 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
           Write(Html.DisplayNameFor(model => model.TransactionInfo));

#line default
#line hidden
            EndContext();
            BeginContext(523, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(579, 40, false);
#line 28 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
           Write(Html.DisplayNameFor(model => model.Time));

#line default
#line hidden
            EndContext();
            BeginContext(619, 86, true);
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 34 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(754, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(815, 44, false);
#line 38 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
               Write(Html.DisplayFor(model => item.TransactionNo));

#line default
#line hidden
            EndContext();
            BeginContext(859, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(927, 44, false);
#line 41 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
               Write(Html.DisplayFor(modelItem => item.Accountno));

#line default
#line hidden
            EndContext();
            BeginContext(971, 69, true);
            WriteLiteral("\r\n                </td>\r\n\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1041, 50, false);
#line 45 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
               Write(Html.DisplayFor(modelItem => item.TransactionInfo));

#line default
#line hidden
            EndContext();
            BeginContext(1091, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1159, 39, false);
#line 48 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
               Write(Html.DisplayFor(modelItem => item.Time));

#line default
#line hidden
            EndContext();
            BeginContext(1198, 48, true);
            WriteLiteral("\r\n                </td>\r\n\r\n\r\n            </tr>\r\n");
            EndContext();
#line 53 "C:\Users\z8410\Desktop\Banking\BankingWebApplication\BankingWebApplication\Views\Accounts\TransactionList.cshtml"
        }

#line default
#line hidden
            BeginContext(1257, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DAL.Entities.Transaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591