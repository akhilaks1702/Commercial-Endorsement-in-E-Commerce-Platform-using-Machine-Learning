<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE.Categories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:Panel ID="Panel1" runat="server">
    
            <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>Manage Product Categories</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>
          
 <asp:Panel ID="Panel3" runat="server">
                                    <br />
                                 
                                     
                                    <table style="width: 51%;">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <fieldset><legend style="color: #006699"><b>New Item Category</b></legend>
                                                <table style="width: 85%;">
                                                    <tr>
                                                        <td style="text-align: left; width: 90px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left; width: 201px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 90px">
                                                            <b>Category</b></td>
                                                        <td style="text-align: left; width: 201px">
                                                            <asp:TextBox ID="txt_categoryname" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                ControlToValidate="txt_categoryname" ErrorMessage="*" ToolTip="field required" 
                                                                ValidationGroup="category"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 90px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left; width: 201px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 90px">
                                                            &nbsp;</td>
                                                        <td style="text-align: right; width: 201px">
                                                            <asp:Button ID="btn_newcategories" runat="server" Text="Submit" 
                                                                ValidationGroup="category" onclick="btn_newcategories_Click" />
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                                <br />
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                    
                                    <br />
                                   
                                    <table style="width: 89%;">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Table ID="Table1" runat="server">
                                                </asp:Table>
                                            </td>
                                        </tr>
                                    </table>

                                    <table style="width: 86%;">
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:ImageButton ID="ImageButton1" runat="server" 
                                                    ImageUrl="~/images/prevlabel.gif" onclick="ImageButton1_Click" Height="80px"  Width="200px" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="ImageButton2" runat="server" 
                                                    ImageUrl="~/images/nextlabel.gif" onclick="ImageButton2_Click" Height="80px"  Width="200px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="ImageButton3" runat="server" 
                                                    ImageUrl="~/images/closelabel.gif" onclick="ImageButton3_Click" Height="80px"  Width="200px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    
                                </asp:Panel>
            <br />
            <br />
        <br />


		
     
    </asp:Panel>

</asp:Content>
