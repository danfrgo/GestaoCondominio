#pragma checksum "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c244d760a1b0ee0354f7be200813789ec7ccf4f7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Utilizadores_Index), @"mvc.1.0.view", @"/Views/Utilizadores/Index.cshtml")]
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
#line 1 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\_ViewImports.cshtml"
using GestaoCondominios;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\_ViewImports.cshtml"
using GestaoCondominios.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
using GestaoCondominios.BLL.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c244d760a1b0ee0354f7be200813789ec7ccf4f7", @"/Views/Utilizadores/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d8cb1666a53a725b23b61a1890eae841e1d7151", @"/Views/_ViewImports.cshtml")]
    public class Views_Utilizadores_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GestaoCondominios.BLL.Models.Utilizador>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn-floating blue darken-4 tooltip"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Utilizadores", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GerirUtilizadorees", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-position", new global::Microsoft.AspNetCore.Html.HtmlString("right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-tooltip", new global::Microsoft.AspNetCore.Html.HtmlString("Gerir utilizador"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
  
    ViewData["Title"] = "Moradores registados";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""tabelas"">

    <div class=""collection with-header"">
        <div class=""collection-header grey darken-1"">
            <div class=""row"">
                <div class=""col s10"">
                    <h6 class=""white-text"">Moradores registados</h6>
                </div>

                <div class=""col s2"">

                </div>
            </div>
        </div>

        <div class=""collection-item"">

            <table class=""striped highlight"">
                <thead>
                    <tr>
                        <th>
                            Nome
                        </th>

                        <th>
                            Codigo Postal
                        </th>

                        <th>
                            Email
                        </th>

                        <th>
                            Telefone
                        </th>

                        <th>
                            Status
                       ");
            WriteLiteral(" </th>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n");
#nullable restore
#line 52 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>\r\n                                ");
#nullable restore
#line 56 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 59 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.CodigoPostal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 62 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 65 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n\r\n");
#nullable restore
#line 68 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                             if (item.Status == StatusConta.Analisando)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>\r\n                                    <span class=\"new badge purple darken-3\" data-badge-caption=\"\"");
            BeginWriteAttribute("id", " id=\"", 2204, "\"", 2217, 1);
#nullable restore
#line 71 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
WriteAttributeValue("", 2209, item.Id, 2209, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 71 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                                                                                                           Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                </td>\r\n");
            WriteLiteral("                                <td");
            BeginWriteAttribute("class", " class=\"", 2346, "\"", 2362, 1);
#nullable restore
#line 74 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
WriteAttributeValue("", 2354, item.Id, 2354, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <a class=\"btn-floating blue darken-4 btnAprovar tooltip\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2458, "\"", 2515, 6);
            WriteAttributeValue("", 2468, "AprovarUtilizador(\'", 2468, 19, true);
#nullable restore
#line 75 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
WriteAttributeValue("", 2487, item.Id, 2487, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2495, "\',", 2495, 2, true);
            WriteAttributeValue(" ", 2497, "\'", 2498, 2, true);
#nullable restore
#line 75 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
WriteAttributeValue("", 2499, item.UserName, 2499, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2513, "\')", 2513, 2, true);
            EndWriteAttribute();
            WriteLiteral(" data-position=\"right\" data-tooltip=\"Aprovar utilizador\"><i class=\"material-icons\">thumb_up</i></a>\r\n                                    <a class=\"btn-floating red darken-4 btnReprovar tooltip\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2709, "\"", 2767, 6);
            WriteAttributeValue("", 2719, "ReprovarUtilizador(\'", 2719, 20, true);
#nullable restore
#line 76 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
WriteAttributeValue("", 2739, item.Id, 2739, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2747, "\',", 2747, 2, true);
            WriteAttributeValue(" ", 2749, "\'", 2750, 2, true);
#nullable restore
#line 76 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
WriteAttributeValue("", 2751, item.UserName, 2751, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2765, "\')", 2765, 2, true);
            EndWriteAttribute();
            WriteLiteral(" data-position=\"right\" data-tooltip=\"Reprovar utilizador\"><i class=\"material-icons\">thumb_down</i></a>\r\n\r\n                                </td>\r\n");
#nullable restore
#line 79 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"

                            }
                            else if (item.Status == StatusConta.Aprovado)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>\r\n                                    <span class=\"new badge green darken-3\" data-badge-caption=\"\">");
#nullable restore
#line 84 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                                                                                            Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                </td>\r\n");
            WriteLiteral("                                <td>\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c244d760a1b0ee0354f7be200813789ec7ccf4f713437", async() => {
                WriteLiteral("<i class=\"material-icons\">group</i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 88 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                                                                                                                                                  WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 88 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                                                                                                                                                                            WriteLiteral(item.UserName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-name", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </td>\r\n");
#nullable restore
#line 90 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"

                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>\r\n                                    <span class=\"new badge orange darken-3\" data-badge-caption=\"\">");
#nullable restore
#line 95 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                                                                                             Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                </td>\r\n");
#nullable restore
#line 97 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tr>\r\n");
#nullable restore
#line 99 "C:\Users\Danie\source\repos\GestaoCondominios\GestaoCondominios\Views\Utilizadores\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GestaoCondominios.BLL.Models.Utilizador>> Html { get; private set; }
    }
}
#pragma warning restore 1591
