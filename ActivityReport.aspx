<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityReport.aspx.cs" Inherits="Timesheetmanagement.ActivityReport1" %>

<!DOCTYPE html>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Name="jquery" />
            <asp:ScriptReference Name="jquery.ui.combined" />
        </Scripts>
    </asp:ScriptManager>
       <div>
        <table class="repeaterborder" style="background-color: gray; width: 100%">
            <tr>
                <td class="repeaterheaderlabel">
                    Select Report Type :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="DrpReports">
                        <asp:ListItem Text="---- Select ----" Value="0">

                        </asp:ListItem>
                        <asp:ListItem Text="Efforts Report" Value="EffortsReport"></asp:ListItem>
                        <asp:ListItem Text="Efforts Summary Report" Value="EffortsSummary"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                            
                           <span class="repeaterheaderlabel">From Date :</span> <asp:TextBox runat="server" ID="TxtFromDate" CssClass="repeateritemlabel" Enabled="true" ></asp:TextBox>
                          
                           <cc1:CalendarExtender ID="Ajfromdate" Animated="true" Format="dd/MM/yyyy" TargetControlID="TxtFromDate" CssClass="black" runat="server"></cc1:CalendarExtender>
                            
                            <asp:RequiredFieldValidator ID="Reqfromdate" runat="server" ControlToValidate="TxtFromDate" ForeColor="Red" ErrorMessage="*" ></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <span class="repeaterheaderlabel">To Date :</span> <asp:TextBox runat="server" ID="TxtToDate" CssClass="repeateritemlabel"></asp:TextBox>
                            <cc1:CalendarExtender ID="Ajtodate" Animated="true" Format="dd/MM/yyyy" TargetControlID="TxtToDate" CssClass="black" runat="server"></cc1:CalendarExtender>
                             <asp:RequiredFieldValidator ID="Reqtodate" runat="server" ControlToValidate="TxtToDate" ForeColor="Red" ErrorMessage="*" ></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="BtnViewReport" Text="View Report" OnClick="BtnViewReport_Click" CssClass="repeaterbuttons" />
                        </td>
                <td>
                    <asp:Button ID="ExportExcel" Text="ExportData" runat="server" OnClick="ExportExcel_Click" />
                </td>
            </tr>
        </table>
        <div style="overflow: scroll; float: left; margin-top: 20px; width: 99%;">
            <asp:GridView ID="gvReport" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                GridLines="Both" HorizontalAlign="Left">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
