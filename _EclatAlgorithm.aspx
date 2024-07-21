<%@ Page Title="" Language="C#" MasterPageFile="~/Member.Master" AutoEventWireup="true" CodeBehind="_EclatAlgorithm.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE._EclatAlgorithm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server" style="text-align: left">
    
    
            <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>YOUR BUYING PATTERNS (ECLAT ALGORITHM)</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>


   
			

            <table style="width: 397px">
                <tr>
                    <td>
                        <asp:Label ID="lblTime" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
			

        <br />
        <h2>
                                                  View Advertisements </h2>
                                                  <br />
             <marquee scrolldelay="150" direction="up"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></marquee>
            
        <table align="center" style="width: 97%;">
            <tr>
                <td align="center">
                   <table align="center" style="width: 97%;">
            <tr>
                <td align="center" valign="top">
                    <br />
                </td>
            </tr>
                       <tr>
                           <td align="center" valign="top">
                               <asp:Panel ID="Panel3" runat="server">
                                   <asp:Panel ID="Panel4" runat="server" Visible="False">
                                       <span class="nav-label">
                                       <table style="width:100%;">
                                           <tr>
                                               <td align="center">
                                                   <h2>
                                                       Your Transactions</h2>
                                               </td>
                                               <td>
                                                   &nbsp;</td>
                                               <td align="center">
                                                   <h2>
                                                       Distinct Items Purchased</h2>
                                               </td>
                                           </tr>
                                           <tr>
                                               <td align="center">
                                                   &nbsp;</td>
                                               <td>
                                                   &nbsp;</td>
                                               <td align="center">
                                                   &nbsp;</td>
                                           </tr>
                                           <tr>
                                               <td align="center">
                                                   <asp:Table ID="Table2" runat="server">
                                                   </asp:Table>
                                               </td>
                                               <td>
                                                   &nbsp;</td>
                                               <td align="center">
                                                   <asp:Table ID="Table1" runat="server">
                                                   </asp:Table>
                                               </td>
                                           </tr>
                                           <tr>
                                               <td align="center">
                                                   &nbsp;</td>
                                               <td>
                                                   &nbsp;</td>
                                               <td align="center">
                                                   &nbsp;</td>
                                           </tr>
                                       </table>
                                       </span>
                                   </asp:Panel>
                                   <asp:Panel ID="Panel5" runat="server" Visible="False">
                                       <span class="nav-label">
                                       <h2>
                                           Frequent Items Bought</h2>
                                       <asp:Table ID="Table3" runat="server">
                                       </asp:Table>
                                       </span>
                                   </asp:Panel>
                                   <br />
                                   <br />
                                   <table align="center" style="width:100%;">
                                       <tr>
                                           <td style="height: 20px">
                                               &nbsp;</td>
                                           <td align="center" style="height: 20px">
                                               <h2>
                                                   Correlations Between Frequently Bought Items (Results)</h2>
                                           </td>
                                           <td style="height: 20px">
                                               &nbsp;</td>
                                       </tr>
                                       <tr>
                                           <td style="height: 20px">
                                               &nbsp;</td>
                                           <td align="center" style="height: 20px">
                                               &nbsp;</td>
                                           <td style="height: 20px">
                                               &nbsp;</td>
                                       </tr>
                                       <tr>
                                           <td style="height: 20px">
                                           </td>
                                           <td align="center" style="height: 20px">
                                               <asp:Table ID="Table4" runat="server">
                                               </asp:Table>
                                           </td>
                                           <td style="height: 20px">
                                           </td>
                                       </tr>
                                   </table>
                                   <br />
                               </asp:Panel>
                           </td>
                       </tr>
                       <tr>
                           <td align="center" valign="top">
                               &nbsp;</td>
                       </tr>
                       <tr>
                           <td align="center" valign="top">
                               <asp:Table ID="Table5" runat="server">
                               </asp:Table>
                               <br />
                               <asp:Panel ID="Panel2" runat="server" Height="400px" Visible="False" 
            Width="721px">
            <br />
            <table style="width: 75%;">
                <tr>
                    <td style="width: 351px" valign="top">
                        <asp:ListBox ID="lv_Items" runat="server" Height="175px" Width="211px">
                        </asp:ListBox>
                    </td>
                    <td style="width: 151px">
                        <asp:ListBox ID="lv_Transactions" runat="server" Height="175px" Width="324px">
                        </asp:ListBox>
                    </td>
                    <td style="width: 151px">
                        <asp:ListBox ID="lv_TransactionsId" runat="server" Height="175px" Width="150px">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="ListBox1" runat="server" Height="161px" Width="211px">
                        </asp:ListBox>
                    </td>
                    <td>
                        <asp:ListBox ID="ListBox2" runat="server" Height="161px" Width="324px">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>
            <br />
        </asp:Panel></td>
                       </tr>
                       <tr>
                           <td align="center" valign="top">
                               &nbsp;</td>
                       </tr>
        </table>
                </td>
            </tr>
        </table>
    <br />
        

		

       
    
    
    </asp:Panel>

</asp:Content>
