<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Admin_Items.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE.Admin_Items" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
   

  
            <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>Manage Products</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>

           <br />
                                  
                                    <table style="width: 64%;">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                             <fieldset><legend style="color: #006699"><b>N</b><b>ew Items</b></legend>
                                                <table style="width: 78%;">
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left; width: 204px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>Category</b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="DropDownList3_SelectedIndexChanged" Width="205px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                                                ControlToValidate="DropDownList3" ErrorMessage="*" Operator="NotEqual" 
                                                                ToolTip="Select Category" ValidationGroup="Items" ValueToCompare="- All -"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>SubCategory</b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="DropDownList2_SelectedIndexChanged" Width="205px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                                ControlToValidate="DropDownList2" ErrorMessage="*" Operator="NotEqual" 
                                                                ToolTip="select subcategory" ValidationGroup="Items" 
                                                                ValueToCompare="- All -"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>Item </b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:TextBox ID="txt_ItemName" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                ControlToValidate="txt_ItemName" ErrorMessage="*" ToolTip="field required" 
                                                                ValidationGroup="Items"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>Item Details</b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:TextBox ID="txt_ItemDetails" runat="server" Width="200px" Height="50px" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                                ControlToValidate="txt_ItemDetails" ErrorMessage="*" ToolTip="field required" 
                                                                ValidationGroup="Items"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>Cost</b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:TextBox ID="txt_Cost" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                                ControlToValidate="txt_Cost" ErrorMessage="*" ToolTip="filed required" 
                                                                ValidationGroup="Items"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>Quantity</b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:TextBox ID="txt_quantity" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                                ControlToValidate="txt_quantity" ErrorMessage="*" ToolTip="field required" 
                                                                ValidationGroup="Items"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>Item Image</b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="205px" />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                                ControlToValidate="FileUpload1" ErrorMessage="*" ToolTip="filed required" 
                                                                ValidationGroup="Items"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                                ControlToValidate="FileUpload1" ErrorMessage="*" ToolTip="select image" 
                                                                ValidationExpression="^([0-9a-zA-Z_\-~ :\\])+(.jpg|.JPG|.jpeg|.JPEG|.bmp|.BMP|.gif|.GIF|.png|.PNG)$" 
                                                                ValidationGroup="Items"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                                                                ForeColor="#006699" onclick="LinkButton1_Click" Visible="False">Change 
                                                            ItemPhoto</asp:LinkButton>
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            <b>Other Details</b></td>
                                                        <td style="text-align: left; width: 204px">
                                                            <asp:TextBox ID="txtOtherDetails" runat="server" TextMode="MultiLine" 
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                                ControlToValidate="txtOtherDetails" ErrorMessage="*" ToolTip="field required" 
                                                                ValidationGroup="Items"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            &nbsp;</td>
                                                        <td style="text-align: right; width: 204px">
                                                            &nbsp;</td>
                                                        <td style="text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; width: 123px">
                                                            &nbsp;</td>
                                                        <td align="right">
                                                            <asp:Button ID="btn_Items" runat="server" onclick="btn_Items_Click" 
                                                                Text="Submit" ValidationGroup="Items" />
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
                                
                                    <table style="width: 91%;">
                                      
                                        <tr>
                                            <td>
                                            <div id="popup">
                                            <div style="height:400px; width:auto; overflow:auto">
                                                <asp:Table ID="Table3" runat="server">
                                                </asp:Table>
                                                </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />


		

       
      
    
    </asp:Panel>
</asp:Content>
