<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Admin_CustomerTransactions.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE.Admin_CustomerTransactions" Title="Untitled Page" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="Panel1" runat="server">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">

            <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>Manage Customer Orders</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>
    <!-- Breadcrumb Form Section Begin -->
            
            <div class="container">
           
               <br />
			
         
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Panel ID="PanelOr2" runat="server" Width="326px">
                                <table style="width:100%;">
                                    <tr>
                                        <td align="left" width="115">
                                            <b>Select Status</b></td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDListO_Status" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="DDListO_Status_SelectedIndexChanged" 
                                                ValidationGroup="Or" Width="200px">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Dispatched</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td ID="Td1">
                        <div id="popup">
                            <asp:Table ID="TableOr" runat="server" BorderStyle="Double" 
                               >
                            </asp:Table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <br />
                            <asp:Panel ID="PnlOrBtns" runat="server" Visible="False" Width="400px">
                                <table style="width:100%;">
                                    <tr>
                                        <td align="left">
                                            <asp:LinkButton ID="LBtnOPrev" runat="server" onclick="LBtnOPrev_Click">&lt;&lt;Previous</asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="LblMsg_order" runat="server" Font-Bold="True" 
                                                Font-Names="Courier New"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:LinkButton ID="LBtnNext" runat="server" onclick="LBtnNext_Click">Next&gt;&gt;</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
		
	</div>


        
            </asp:View>
            <asp:View ID="View2" runat="server">
          <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>View Transaction Details</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>
    <!-- Breadcrumb Form Section Begin -->
     <div class="container">
            <br />

                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="left">
                            <asp:LinkButton ID="LBtn_Back" runat="server" onclick="LBtn_Back_Click">&lt;&lt;Back</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="font-size: small; color: #FF0000">
                            <b>** Please Mouse over on the Item to know the Quantity Available</b></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="center" style="font-size: small; color: #FF0000">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="center">
                            <div ID="popup">
                                <asp:Table ID="TableOrD" runat="server">
                                </asp:Table>
                            </div>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <br />

		
	</div>

    
            </asp:View>
        </asp:MultiView>
   
        <br />
   
    </asp:Panel>

</asp:Content>
