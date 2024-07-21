<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminViewComments.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE.AdminViewComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">
   <!-- Breadcrumb Section Begin -->
    <div class="breacrumb-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="breadcrumb-text">
                        <h2><span>View</span> Products Comments</h2>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumb Form Section Begin -->

   
   <!-- Register Section Begin -->
    
        <div class="container">
    <br />       
         <marquee scrolldelay="150" behavior="alternate">
          <img src="../images/103.cms.jpeg" width="300" height="180" alt="" /> &nbsp
          <img src="../images/108.cms.jpg" width="300" height="180" alt="" /> &nbsp
         <img src="../images/109.cms.jpeg" width="300" height="180" alt="" /> &nbsp
          </marquee>
          <br />
        <table style="width: 60%;" align="center">
            <tr>
                <td>
                    </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Table ID="Table2" runat="server">
                    </asp:Table>




                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 90%;" align="center">
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
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        
        </div>
    
    </asp:Panel>

</asp:Content>
