<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Admin_Homepage.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE.Admin_Homepage" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <asp:Panel ID="Panel1" runat="server" >
    
  
        <!-- Breadcrumb Section Begin -->
   
            <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>Welcome Admin, Manage Customers</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>
    <!-- Breadcrumb Form Section Begin -->
			<div class="container">
           
               <br />

            <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            &nbsp;
            <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
        <br />
      
                   <table style="width: 97%;">
            <tr>
                <td valign="top">
                    <br />
                     <div id="popup">
                    <asp:Table ID="Table1" runat="server" Height="17px">
                    </asp:Table>
                    </div>
                    <br />
                </td>
            </tr>
        </table>
                
    <br />

    <marquee scrolldelay="150" behavior="alternate">
          <img src="../images/c4.jpg" width="300" height="180" alt="" /> &nbsp
          <img src="../images/c5.JPG" width="300" height="180" alt="" /> &nbsp
         <img src="../images/c1.jpg" width="300" height="180" alt="" /> &nbsp
          </marquee>
		
	</div>


        
        
</asp:Panel>


</asp:Content>
