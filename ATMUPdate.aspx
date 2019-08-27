<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ATMUPdate.aspx.cs" Inherits="Timesheetmanagement.ATMUPdate" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<script runat="server">

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Calender.aspx");
    }
</script>--%>
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
            <fieldset class="fldSet">
                <legend class="DivClass"><b>Update Activity Status</b></legend>
                <div></div>
                <div class="DivClass">
                    <span><b>Welcome:-
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>|<asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                    </b></span>

                    <table class="DivClass">
                        <tr>
                            <td class="tdClass">Select LOB</td>
                            <td class="tdClass">

                                <asp:DropDownList ID="ddlResourceName0" runat="server" CssClass="dropdown"></asp:DropDownList>
                            </td>
                            <td class="tdClass">&nbsp;Select Application:-
                            </td>
                            <td class="tdClass">

                                <asp:DropDownList ID="ddlResourceName" runat="server" CssClass="dropdown"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>

                            <td class="tdClass">&nbsp;Select Scheduler:-</td>
                            <td class="tdClass">
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="dropdown"></asp:DropDownList>
                            </td>
                            <td class="tdClass">Select Status:-</td>
                            <td class="tdClass">
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="dropdown"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdClass">&nbsp;RCA</td>
                            <td class="tdClass">
                                <asp:TextBox ID="txtComment0" runat="server" TextMode="MultiLine"></asp:TextBox>

                                <br />
                            </td>
                            <td class="tdClass">CA</td>
                            <td class="tdClass">
                                <br />
                                <asp:TextBox ID="txtComment1" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="tdClass">PA
                            </td>
                            <td class="tdClass">
                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </td>
                            <td class="tdClass">Remark</td>
                            <td class="tdClass">
                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="tdClass" colspan="4">
                                <asp:Button ID="Button1" Text="Add Details" runat="server" CssClass="button" OnClick="btnApply_Click" />

                            </td>

                        </tr>
                        <tr>
                            <td colspan="3"></td>

                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="Dgrd" runat="server">
                                </asp:GridView>
                            </td>

                        </tr>
                        <tr>
                            <td class="tdClass">
                                <asp:Button ID="btnApply" Text="Submit" runat="server" CssClass="button" OnClick="btnApply_Click" />
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
