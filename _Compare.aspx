<%@ Page Title="" Language="C#" MasterPageFile="~/Member.Master" AutoEventWireup="true" CodeBehind="_Compare.aspx.cs" Inherits="DATAMINING_ASSOCIATIONRULE._Compare" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">

    
            <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h3>Comparative Analysis (Apriori vs ECLAT)</h3>
                  </div>
               </div>
            </div>
         </div>
      </section>

     
   
        <div class="container">           
<br />
                            <div>
						   		<span>
                                       <asp:Table ID="tableCompare" runat="server" HorizontalAlign="Center" 
                                    Width="100%">
                                       </asp:Table>  </span>
						  </div>

  				                 <asp:Panel ID="panelUpdatePassword" runat="server">

<br />
<br />
  <div class="article">
          <h2><span>Graph</span> Representation (Algorithm Vs Efficiency)!!!</h2>
          <hr />
    
    	
<div style="float:left;width:340px;">
			<div class="box">
				<div class="registration_left">
   <%-- <a href="#"><div class="reg_fb"><i>Select Chart Type</i><div class="clear"></div></div></a>--%>
		 <div class="registration_form">
				<p>
					<asp:DropDownList ID="ddlChartType" runat="server" AutoPostBack="False" 
                        Visible="False">
					</asp:DropDownList>
				</p>
			</div>

			<div class="box">
				<p>
					<asp:RadioButtonList ID="rblValueCount" runat="server" AutoPostBack="False" Visible="False" 
                        >
						<asp:ListItem Value="10">10 Values</asp:ListItem>
						<asp:ListItem Value="20">20 Values</asp:ListItem>
						<asp:ListItem Value="50">50 Values</asp:ListItem>
						<asp:ListItem Value="100">100 Values</asp:ListItem>
						<asp:ListItem Value="500" Selected="True">500 Values</asp:ListItem>
					</asp:RadioButtonList>
				</p>
			</div>
		</div>

		<div class="box">
			<p>
				<asp:CheckBox ID="cbUse3D" runat="server" AutoPostBack="False" 
                    Text="Use 3D Chart" Visible="False" />
			</p>
			<p>
				<asp:RadioButtonList ID="rblInclinationAngle" runat="server" 
                    AutoPostBack="False" Visible="False">
					<asp:ListItem Value="-90">-90°</asp:ListItem>
					<asp:ListItem Value="-50">-50°</asp:ListItem>
					<asp:ListItem Value="-20">-20°</asp:ListItem>
					<asp:ListItem Value="0">0°</asp:ListItem>
					<asp:ListItem Selected="True" Value="20">20°</asp:ListItem>
					<asp:ListItem Value="50">50°</asp:ListItem>
					<asp:ListItem Value="90">90°</asp:ListItem>
				</asp:RadioButtonList>
			</p>
		</div>
		

	</div>

  <div>
      <table style="width: 100%;">
          <tr>
              <td>
                  &nbsp;<asp:Button ID="btnShow" runat="server" onclick="btnShow_Click" Text="Show" 
                      ValidationGroup="a" Width="125px" Visible="False" />
                  &nbsp;</td>
          </tr>
          </table>
  
  </div>
		
      
    </div><div class="clear"></div>

    <asp:Chart ID="cTestChart" runat="server" Height="400px" Width="600px" 
            Visible="False">
			<Series>
				<asp:Series Name="Testing">
				</asp:Series>
			</Series>
			<ChartAreas>
				<asp:ChartArea Name="ChartArea1">
					<Area3DStyle />
				</asp:ChartArea>
			</ChartAreas>
		</asp:Chart>

       

        </div>
        </asp:Panel>

        
    </div>
    </asp:Panel>




</asp:Content>
