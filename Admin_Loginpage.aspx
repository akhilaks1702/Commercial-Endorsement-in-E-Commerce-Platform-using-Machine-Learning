<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Admin_Loginpage.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE.Admin_Loginpage" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

               
<!-- Breadcrumb Section Begin -->
    <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>Admin Login Form</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>
    <!-- Breadcrumb Form Section Begin -->
			<!-- Register Section Begin -->
            <div class="container">

                <table style="width: 34%;">
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left; width: 127px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            <strong><span style="font-size: small">Admin Id</span></strong></td>
                        <td style="font-size: small; text-align: left; width: 127px">
                            <span style="font-size: small">
                            <asp:TextBox ID="tb_uname" runat="server" CssClass="text" Width="200px"></asp:TextBox>
                            </span>
                        </td>
                        <td style="font-size: small; text-align: left">
                            <span style="font-size: small">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_uname"
                                CssClass="text" ErrorMessage="*" ToolTip="field required" 
                                ValidationGroup="Adminlogin"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left; width: 127px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            <strong><span style="font-size: small">Password</span></strong></td>
                        <td style="font-size: small; text-align: left; width: 127px">
                            <span style="font-size: small">
                            <asp:TextBox ID="tb_pass" runat="server" CssClass="text" TextMode="Password" 
                                Width="200px"></asp:TextBox>
                            </span>
                        </td>
                        <td style="font-size: small; text-align: left">
                            <span style="font-size: small">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_pass"
                                CssClass="text" ErrorMessage="*" ToolTip="field required" 
                                ValidationGroup="Adminlogin"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left; width: 127px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: right; ">
                           <asp:ImageButton ID="btn_submit" runat="server" ImageUrl="~/images/CAUVGTUR.jpg" 
                                    onclick="btn_submit_Click" ValidationGroup="Adminlogin" /></td>
                        <td style="font-size: small; text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left; width: 127px">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        <td style="font-size: small; text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-size: small; text-align: left; width: 85px">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: right; ">
                            &nbsp;</td>
                        <td style="font-size: small; text-align: left">
                            &nbsp;</td>
                    </tr>
                </table>
                
               
                
                <br />
               
               <marquee scrolldelay="150" behavior="alternate">
          <img src="../images/c4.jpg" width="300" height="180" alt="" /> &nbsp
          <img src="../images/c5.JPG" width="300" height="180" alt="" /> &nbsp
         <img src="../images/c1.jpg" width="300" height="180" alt="" /> &nbsp
          </marquee>
		<br />
	 </div>
        
</asp:Content>
