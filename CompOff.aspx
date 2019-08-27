<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompOff.aspx.cs" Inherits="Timesheetmanagement.CompOff" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style>
    .DivClass {
        background-color: snow;
        color: darkmagenta;
        margin: 20px 0 20px 0;
        padding: 20px;
        align-self: center;
        align-items: center;
    }

    .tdClass {
        margin: 20px 0 20px 0;
        padding: 20px;
        align-self: center;
        align-items: center;
    }

    .cal {
        background-color: lightblue;
    }

    .fldSet {
        border-color: darkmagenta;
    }

    .button {
        background-color: #4CAF50; /* Green */
        border: none;
        color: white;
        padding: 15px 32px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        align-self: center;
    }

    .dropdown {
        position: relative;
        background-color: snow;
        color: darkmagenta;
        min-width: 160px;
        box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
        padding: 12px 16px;
        z-index: 1;
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
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
            <fieldset class="fldSet">
                <legend class="DivClass"><b>Comp Off</b></legend>
                <div></div>
                <div class="DivClass">
                    <span><b>Welcome:-
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                    </b></span>

                    <table class="DivClass">
                        <tr>
                            <td class="tdClass">Resource Name:-
                            </td>
                            <td class="tdClass">
                                <asp:DropDownList ID="ddlResourceName" runat="server" CssClass="dropdown" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdClass">CompOff Date:-
                            </td>
                            <td class="tdClass">
                                <asp:TextBox ID="txtCompOffDate" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="Ajfromdate" Animated="true" Format="MM/dd/yyyy" TargetControlID="txtCompOffDate" CssClass="cal" runat="server"></cc1:CalendarExtender>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter CompOff Date" ForeColor="Red" ControlToValidate="txtCompOffDate">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       <%--  <tr>
                            <td class="tdClass">To Date:-
                            </td>
                            <td class="tdClass">
                                <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" Animated="true" Format="MM/dd/yyyy" TargetControlID="txtToDate" CssClass="cal" runat="server"></cc1:CalendarExtender>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter From Date" ForeColor="Red" ControlToValidate="txtToDate">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="tdClass">Leave Type
                            </td>
                            <td class="tdClass">
                                <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="dropdown"></asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdClass">Comment
                            </td>
                            <td class="tdClass">
                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRequeststatus" runat="server" Text=""></asp:Label>
                            </td>
                             <td>
                                
                               <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtCompOffDate" ControlToValidate="txtToDate" ErrorMessage="To date should be date after or Equal to From Date" ForeColor="Red" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                               --%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="tdClass">
                                <asp:Button ID="btnApply" Text="Add" runat="server" CssClass="button" OnClick="btnApply_Click" />
                            </td>
                            <td class="tdClass">
                                <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="button" OnClick="btnBack_Click" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
