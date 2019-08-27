<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Timesheetmanagement.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:::: Time Sheet Management ::::</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; height: 600px; text-align: center">
                <tr>
                    <td style="width: 20%">&nbsp;
                    </td>
                    <td style="width: 60%">
                        <table style="width: 100%">
                            <tr>
                                <td align="center">
                                    <table style="background-color:lightblue">
                                        <tr>
                                            <td colspan="2">Login Panel
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>User Id :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TxtUserId"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="ReqUserid" ErrorMessage="*" ControlToValidate="TxtUserId" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Password :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TxtPassword" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="Reqpassword" ErrorMessage="*" ControlToValidate="TxtPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="BtnLogin" Text="Login" OnClick="BtnLogin_Click" />
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="BtnCancel" Text="Cancel" OnClick="BtnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td style="width: 20%">&nbsp;
                    </td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>
