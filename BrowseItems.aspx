<%@ Page Title="" Language="C#" MasterPageFile="~/Member.Master" AutoEventWireup="true" CodeBehind="BrowseItems.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE.BrowseItems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">

 <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>Browse Products (Categorywise and Subcategorywise)</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>
     
    
             <table style="width: 90%;">
            <tr>
                <td>
                    <table style="width: 92%;">
                        <tr>
                            <td style="text-align: left; width: 115px">
                                &nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 115px">
                                <b>Item Category</b></td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" 
                                    AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 115px">
                                &nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width: 80%;">
                        <tr>
                            <td style="text-align: left; width: 182px">
                                &nbsp;</td>
                            <td style="text-align: left; width: 201px">
                                &nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 182px">
                                <b>Item SubCategory</b></td>
                            <td style="text-align: left; width: 201px">
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="200px" 
                                    AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 182px">
                                &nbsp;</td>
                            <td style="text-align: left; width: 201px">
                                &nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 90%;">
            <tr>
                <td>
                    <asp:Table ID="Table1" runat="server">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <table style="width: 86%;">
            <tr>
                <td style="text-align: left">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/images/prevlabel.gif" onclick="ImageButton1_Click" Height="80px"  Width="200px"/>
                </td>
                <td>
                    <asp:Label ID="LblMsg" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/images/nextlabel.gif" onclick="ImageButton2_Click" Height="80px"  Width="200px"/>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImageButton3" runat="server" 
                        ImageUrl="~/images/closelabel.gif" onclick="ImageButton3_Click" Height="80px"  Width="200px"/>
                </td>
            </tr>
        </table>
        <br />
     
        <br />


		
     
    </asp:Panel>
</asp:Content>
