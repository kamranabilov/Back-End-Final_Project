#pragma checksum "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f0542e1bae8f590b5cc746f00b16789fa5e774e7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdminPanel_Views_Dashboard_Index), @"mvc.1.0.view", @"/Areas/AdminPanel/Views/Dashboard/Index.cshtml")]
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
#line 1 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\_ViewImports.cshtml"
using Back_End_Final_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\_ViewImports.cshtml"
using Back_End_Final_Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f0542e1bae8f590b5cc746f00b16789fa5e774e7", @"/Areas/AdminPanel/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7951285ae88d309ff35dd9a3eb15788c1f379d08", @"/Areas/AdminPanel/Views/_ViewImports.cshtml")]
    public class Areas_AdminPanel_Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ContactUs>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""main-panel"">   
    <div class=""table-responsive pt-3"">
        <table class=""table table-success"">
            <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Email
                    </th>
                    <th>
                       Subject
                    </th>
                    <th>
                        Message
                    </th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 25 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr style=\"background-color:blue\" >\r\n                        <td >\r\n                            ");
#nullable restore
#line 29 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml"
                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 32 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml"
                       Write(item.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 35 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml"
                       Write(item.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 38 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml"
                       Write(item.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 41 "E:\3.Asp.Net.Core\Back-End Final Project\Back-End Final Project\Back-End Final Project\Areas\AdminPanel\Views\Dashboard\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ContactUs>> Html { get; private set; }
    }
}
#pragma warning restore 1591
